using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    private int randomSpot;
    private float waitTime;

    void Start()
    {
        waitTime = startWaitTime;
        if (moveSpots.Length > 0)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpots.Length > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
