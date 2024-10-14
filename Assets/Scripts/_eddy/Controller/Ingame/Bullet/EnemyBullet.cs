using UnityEngine;

public class EnemyBullet : BulletController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("dan gay dame");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bulletDamage);
            HideBullet();
        }
    }
}
