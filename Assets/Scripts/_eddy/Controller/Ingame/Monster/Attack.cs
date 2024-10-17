using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Attack ----------")]
    public List<GameObject> bulletPool;
    public GameObject bullet;
    public int amoutBulletsToPool;

    public Transform attackPoint;
    public float attacksPerSec;
    public float minimumDistanceAttack;
    public float bulletForce;

    [SerializeField] private int meleeDamage = 1;
    private float lastTimeAttack;

    void InitForAttack()
    {
        Transform pool = GameObject.Find("BulletPool").transform;
        bulletPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amoutBulletsToPool; i++)
        {
            temp = Instantiate(bullet, pool);
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    protected virtual void MonsterAttack()
    {
        Vector2 direction = target.transform.position - attackPoint.position;

        //target.TakeDamage(damageToGive);  can chien

        if (distance < minimumDistanceAttack && Time.time >= lastTimeAttack + 1 / attacksPerSec)
        {
            lastTimeAttack = Time.time;
            ShootBullet(direction);
        }
    }

    void ShootBullet(Vector2 direction)
    {
        GameObject bullet = GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = attackPoint.position;
            float rotateValue = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            //bullet.transform.rotation = Quaternion.Euler(0, 0, rotateValue + 90);
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotateValue + (int)bullet.GetComponent<BulletController>().spriteDirection);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * bulletForce);
        }

    }

    private GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    void OnCollisionEnter2DAttack(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("quai gay dame");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(meleeDamage);
        }
    }

}
