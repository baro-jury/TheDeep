using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("wall") 
            || collision.gameObject.CompareTag("CloseDoor"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("trung dan");
            }
            gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.zero);
        }
    }
}
