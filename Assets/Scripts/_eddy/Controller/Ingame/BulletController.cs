using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("Wall") 
            || collision.gameObject.CompareTag("Gate"))
        {
            //if (collision.gameObject.CompareTag("Player"))
            //{
            //    Debug.Log("trung dan");
            //}
            gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
