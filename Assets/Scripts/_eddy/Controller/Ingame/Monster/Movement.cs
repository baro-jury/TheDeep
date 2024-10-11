using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Movement ----------")]
    public float speed;
    private float distance;
    public float minimumDistanceFollow;
    bool moveable = true;

    protected virtual void MonsterMove()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        moveable = true;
        if (moveable == true)
        {
            Vector2 moveVector = target.transform.position - transform.position;
            Vector2 velocity = moveVector.normalized * monster.velocity;
            if (moveVector.magnitude < velocity.magnitude * Time.fixedDeltaTime)
            {
                rb2D.velocity = Vector2.zero;
            }
            else
            {
                rb2D.velocity = distance < minimumDistanceFollow ? velocity : Vector2.zero;
            }

        }

        Flip(rb2D.velocity.x);
    }

    void Flip(float xVel)
    {
        Vector3 scale = transform.localScale;
        if (xVel * scale.x < 0) scale.x *= -1f;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2DMovement(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveable = false;
        }
    }

    private void OnTriggerEnter2DMovement(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (GetCurHealth() <= 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
