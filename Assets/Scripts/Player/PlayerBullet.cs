using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") 
            || collision.gameObject.CompareTag("wall")
            || collision.gameObject.CompareTag("CloseDoor"))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            //Invoke("DestroyGameObject", 10f);
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * 15 * Time.deltaTime, Space.World);
    }

    void DestroyGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
