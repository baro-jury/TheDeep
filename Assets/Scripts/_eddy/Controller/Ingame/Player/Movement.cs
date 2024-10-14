using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Movement ----------")]
    private Vector2 moveDirection;

    void MyPlayerMove()
    {
        //if (!IsBlocked())
        //{
        //    rb2D.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);
        //}
        rb2D.velocity = new Vector2(player.velocity * moveDirection.x, player.velocity * moveDirection.y);

        if (moveDirection.x > 0)
        {
            Vector3 scale = transform.localScale;
            if (scale.x < 0) scale.x *= -1f;
            transform.localScale = scale;
        }
        else if (moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            if (scale.x > 0) scale.x *= -1f;
            transform.localScale = scale;
        }

        #region sizeMap
        //if (transform.position.x < -28.5f || transform.position.x > 28.5f)
        //{
        //    transform.position = new Vector2(transform.position.x > 28.5f ? 28.5f : -28.5f, transform.position.y);
        //}
        //if (transform.position.y > 8)
        //{
        //    transform.position = new Vector2(transform.position.x, 8);
        //} 
        #endregion
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
