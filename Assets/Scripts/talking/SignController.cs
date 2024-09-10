using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public TextAsset jsonTalking;
    private List<Dialogue> listDialogue;

    Dialogue d;
    public DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        //TextAsset jsonData = Resources.Load<TextAsset>("Image/NPC/testTalking");
        //listDialogue = JsonConvert.DeserializeObject<List<Dialogue>>(jsonData.text);
        
        //d = listDialogue.FirstOrDefault(x=> x.level.ToLower().Trim().Equals("demo"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void saySomeThing()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("Image/NPC/testTalking");
        listDialogue = JsonConvert.DeserializeObject<List<Dialogue>>(jsonData.text);

        d = listDialogue.FirstOrDefault(x => x.level.ToLower().Trim().Equals("demo"));
        dialogueManager.startDialogue(d);
    }
}
