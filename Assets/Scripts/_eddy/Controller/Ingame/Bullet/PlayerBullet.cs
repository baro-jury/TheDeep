using UnityEngine;

public class PlayerBullet : BulletController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HideBullet();
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * 15 * Time.deltaTime, Space.World);
    }

}
