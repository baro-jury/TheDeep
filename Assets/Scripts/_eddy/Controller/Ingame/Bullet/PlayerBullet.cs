using UnityEngine;

public class PlayerBullet : BulletController
{
    public override void Trigger2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("Wall")
            || collision.gameObject.CompareTag("Gate"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * 15 * Time.deltaTime, Space.World);
    }

}
