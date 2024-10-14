using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Attack ----------")]
    public List<GameObject> bulletPool;
    public GameObject bullet;
    public int amoutBulletsToPool;

    public Transform shootPoint;
    public float FireRate;
    float nexTimeToFire = 0;
    public float minimumDistanceAttack;
    public float Force;
    Vector2 Direction;

    [SerializeField] private int meleeDamage = 1;

    void InitForAttack()
    {
        Transform pool = GameObject.Find("BulletPool").transform;
        bulletPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amoutBulletsToPool; i++)
        {
            temp = Instantiate(bullet, pool);
            temp.GetComponent<HurtPlayer>().player = target;
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    protected virtual void MonsterAttack()
    {
        //target.TakeDamage(damageToGive);

        Direction = target.transform.position - shootPoint.position;

        if (distance < minimumDistanceFollow && Time.time > nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1 / FireRate;
            ShootBullet();
        }
    }

    GameObject GetPooledBullet()
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

    void ShootBullet()
    {
        GameObject bullet = GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            float rotateValue = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotateValue + 90);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
        }

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
