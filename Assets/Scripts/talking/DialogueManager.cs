using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    public GameObject panel;
    private Queue<string> sentences;
    public GameObject player;

    public Text nameText;
    public Text dialogueText;

    #region load npc img
    private Sprite sampleNPC;
    private Sprite gray;
    private Sprite cinnamon;
    private Sprite story;
    private Sprite river;
    private Sprite sword;
    #endregion
    public Image imgNpc;
    public bool isPaused = false;

    private string[] charName;

    // Start is called before the first frame update
    void Start()
    {
        loadNPCImg();
        sentences = new Queue<string>();
        Dialogue d = new Dialogue();
        charName = d.getCharName();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startDialogue(Dialogue dialogues)
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        sentences.Clear();
        foreach (string sentence in dialogues.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dislayNextSentence();
        if (panel != null)
        {
            panel.SetActive(true);
            player.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void dislayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string[] words = sentence.Split(':');

        nameText.text = getNameChar(words[0]);
        dialogueText.text = words[1];

        string img = charName.FirstOrDefault(x => x.ToLower().Trim().Equals(words[0].ToLower().Trim()));
        setImage(img);
    }

    public void endDialogue()
    {
        isPaused = !isPaused;
        Time.timeScale = 1f;
        panel.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void loadNPCImg()
    {
        sampleNPC = Resources.Load<Sprite>("Image/NPC/sample npc");
        story = Resources.Load<Sprite>("Image/NPC/story");
        gray = Resources.Load<Sprite>("Image/NPC/gray");
        cinnamon = Resources.Load<Sprite>("Image/NPC/cinnamon");
        sword = Resources.Load<Sprite>("Image/NPC/sword");
        river = Resources.Load<Sprite>("Image/NPC/river");
    }

    private void setImage(string name)
    {
        name = name == null ? "" : name.Trim().ToLower();
        imgNpc.sprite = null;
        if (name.Equals("g"))
        {
            imgNpc.sprite = gray;
        }
        else if (name.Equals("c"))
        {
            imgNpc.sprite = cinnamon;
        }
        else if (name.Equals("b"))
        {
            imgNpc.sprite = sword;
        }
        else if (name.Equals("s"))
        {
            imgNpc.sprite = story;
        }
        else if (name.Equals("r"))
        {
            imgNpc.sprite = river;
        }
        else
        {
            imgNpc.sprite = sampleNPC;
        }
    }

    private string getNameChar(string name)
    {
        name = name.Trim().ToLower();
        if (name.Equals("g"))
        {
            return "Gray";
        }
        else if (name.Equals("c"))
        {
            return "Cinnamon";
        }
        else if (name.Equals("b"))
        {
            return "Broken sword";
        }
        else if (name.Equals("s"))
        {
            return "Story teller";
        }
        else if (name.Equals("r"))
        {
            return "The riverkeeper";
        }
        else
        {
            return "Other";
        }
    }
}
