using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollider : MonoBehaviour
{
    private bool isRoomDone;
    public Room roomParent;
    private List<GameObject> numOfCloseDoor = new List<GameObject>();
    public GenMonster genMonster;  

    // Start is called before the first frame update
    void Start()
    {
        isRoomDone = false;
        roomParent = GetComponentInParent<Room>();
        foreach (Transform child in roomParent.transform)
        {
            if (child.tag == "CloseDoor")
            {
                numOfCloseDoor.Add(child.gameObject);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        checkRoomDone();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && isRoomDone == false)
        {
            for(int i = 0; i < genMonster.enemies.Count; i++)
            {
                genMonster.enemies[i].gameObject.SetActive(true);
            }
            
            for (int i = 0; i < numOfCloseDoor.Count; i++)
            {  
                numOfCloseDoor[i].GetComponent<Collider2D>().isTrigger = false;
                numOfCloseDoor[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void checkRoomDone()
    {
        if(genMonster.enemies.Count == 0)
        {
            isRoomDone = true;
            for (int i = 0; i < numOfCloseDoor.Count; i++)
            {
                numOfCloseDoor[i].GetComponent<Collider2D>().isTrigger = true;
                numOfCloseDoor[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }
}
