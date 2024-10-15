using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Movement ----------")]
    private Vector2 moveDirection;

    void MyPlayerMove()
    {
        rb2D.velocity = new Vector2(player.velocity * moveDirection.x, player.velocity * moveDirection.y);

        Vector3 scale = transform.localScale;
        if (moveDirection.x * scale.x < 0) scale.x *= -1f;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2DMovement(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 moveDirection = (transform.position - collision.transform.position).normalized;
            float pushForce = 2000f;
            rb2D.AddForce(moveDirection * pushForce);
        }
    }

}
