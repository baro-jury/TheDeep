using UnityEngine;

public class EnemyBullet : BulletController
{
    public override void Trigger2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("Wall")
            || collision.gameObject.CompareTag("Gate"))
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
