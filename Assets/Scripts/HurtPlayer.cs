using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private float waitToHurt = 1f;
    private bool isCollided;
    [SerializeField] private int damageToGive = 1;

    void Update()
    {
        if (isCollided)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt < 0f)
            {
                player.TakeDamage(damageToGive);
                waitToHurt = 1.5f;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("-1");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damageToGive);
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
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damageToGive);

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
