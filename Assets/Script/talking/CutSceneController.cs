using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public string levelOfScene;
    public GameObject panel;
    private Queue<string> sentences;
    //public GameObject player;

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
    public GameObject fadeImage;
    public float fadeTime = 1f;
    public AudioSource audioSource;

    private string[] charName;

    public TextAsset jsonTalking;
    private List<Dialogue> listDialogue;
    public bool isPaused = false;

    Dialogue d;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("Image/NPC/cutscene");
        listDialogue = JsonConvert.DeserializeObject<List<Dialogue>>(jsonData.text);

        loadNPCImg();
        sentences = new Queue<string>();
        d = listDialogue.FirstOrDefault(x => x.level.ToLower()
        .Equals(levelOfScene.Trim().ToLower()));
        if (d == null)
        {
            d = listDialogue.FirstOrDefault(x => x.level.ToLower().Equals("demo"));
        }
        charName = d.getCharName();
        startDialogue(d);
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
            //player.GetComponent<CharacterMovement>().enabled = false;
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
        StartCoroutine(FadeOutCoroutine());
        panel.SetActive(false);
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        loadNextScene(levelOfScene);
        //player.GetComponent<CharacterMovement>().enabled = true;
    }

    private void loadNextScene(string levelOfScene)
    {
        if (levelOfScene.Equals("introlduction"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        else if (name.Equals("dev"))
        {
            return "The developer who make this";
        }
        else
        {
            return "Other";
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        fadeImage.SetActive(true);
        Color color = fadeImage.GetComponent<SpriteRenderer>().color;
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeTime);
            fadeImage.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }
}
