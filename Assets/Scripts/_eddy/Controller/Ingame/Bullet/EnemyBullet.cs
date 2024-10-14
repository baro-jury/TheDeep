using UnityEngine;

public class EnemyBullet : BulletController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            HideBullet();
        }
    }
}
