using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderCloseDoor : MonoBehaviour
{
    public List<GameObject> closeDoor;
    public GameObject bossAndUi;
    public Boss bossHealth;
    public AudioSource audio;
    private bool isPlayed;
    public GameObject bossBar;
    // Start is called before the first frame update
    void Start()
    {
        isPlayed = false;
        audio.loop = true;
        bossBar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.getCurHealth() <= 0)
        {
            bossBar.SetActive(false);
            audio.Pause();
            for (int i = 0; i < closeDoor.Count; i++)
            {
                closeDoor[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossAndUi.SetActive(true);
            for (int i = 0; i < closeDoor.Count; i++)
            {
                closeDoor[i].SetActive(true);
            }
            if (!isPlayed)
            {
                audio.Play();
                bossBar.SetActive(true);
                isPlayed = true;
            }
            Debug.Log("va cham");
        }
    }
}
