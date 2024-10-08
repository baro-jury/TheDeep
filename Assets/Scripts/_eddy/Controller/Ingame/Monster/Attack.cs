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
    
    [SerializeField] private float waitToHurt = 1f;
    private bool isCollided;
    [SerializeField] private int damageToGive = 1;

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

    void MonsterAttack()
    {
        if (isCollided)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt < 0f)
            {
                target.hurtPlayer(damageToGive);
                waitToHurt = 1.5f;
            }
        }

        Direction = target.transform.position - transform.position;

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
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
            
        }
        
    }

    void OnCollisionEnter2DAttack(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().hurtPlayer(damageToGive);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isCollided = false;
            waitToHurt = 1.5f;
        }

    }
    private void OnTriggerEnter2DAttack(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("-1 quai");
            collision.gameObject.GetComponent<PlayerController>().hurtPlayer(damageToGive);

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitToHurt = 1.5f;
            isCollided = false;
        }
    }

}
