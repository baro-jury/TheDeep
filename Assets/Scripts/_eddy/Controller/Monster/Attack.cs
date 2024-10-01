using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Attack ----------")]
    public GameObject bullet;
    public float FireRate;
    float nexTimeToFire = 0;
    public float minimumDistanceAttack;
    public Transform ShootPoint;
    public float Force;
    Vector2 Direction;
    public List<GameObject> bullets;
    public int countBullets;

    [SerializeField] private float waitToHurt = 1f;
    private bool isCollided;
    [SerializeField] private int damageToGive = 1;


    void InitForAttack()
    {
        for (int i = 0; i < countBullets; i++)
        {
            GameObject b = Instantiate(bullet);
            b.GetComponent<HurtPlayer>().player = target;
            b.SetActive(false);
            bullets.Add(b);
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

    GameObject GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].activeSelf == false)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.position = ShootPoint.position;

                return bullets[i];
            }
        }
        return null;
    }
    void ShootBullet()
    {
        //GameObject bulletIns = Instantiate(GetBullet(), ShootPoint.position, Quaternion.identity);
        GameObject bulletIns = GetBullet();
        bulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    void OnCollisionEnter2DAttack(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("-1");
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("-1");
            other.gameObject.GetComponent<PlayerController>().hurtPlayer(damageToGive);

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
