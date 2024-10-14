using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int bulletDamage = 1;
    public float maxTimeExist = 10f;
    private float timeExist;

    void Start()
    {
        timeExist = 0;
    }

    void Update()
    {
        timeExist += Time.deltaTime;
        if (timeExist >= maxTimeExist)
        {
            HideBullet();
        }
    }

    protected void HideBullet()
    {
        gameObject.SetActive(false);
        timeExist = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")
            || collision.gameObject.CompareTag("Gate"))
        {
            HideBullet();
        }
    }
}
