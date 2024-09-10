using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private Heath healthMan;
    [SerializeField] private float waitToHurt = 1f;
    private bool isCollided;
    [SerializeField] private int damageToGive = 1;
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<Heath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollided)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt < 0f)
            {
                healthMan.hurtPlayer(damageToGive);
                waitToHurt = 1.5f;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("-1");
            other.gameObject.GetComponent<Heath>().hurtPlayer(damageToGive);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = false;
            waitToHurt = 1.5f;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("-1");
            other.gameObject.GetComponent<Heath>().hurtPlayer(damageToGive);

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         
            isCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitToHurt = 1.5f;
            isCollided = false;
        }
    }
}
