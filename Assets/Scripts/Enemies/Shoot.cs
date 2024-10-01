using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float FireRate;
    float nexTimeToFire = 0;
    public float minimumDistance;
    private float distance;
    public GameObject player;
    public Transform ShootPoint;
    public float Force;
    Vector2 Direction;
    public List<GameObject> bullets;
    public int countBullets;
    
    void Start()
    {
        for (int i = 0; i < countBullets; i++)
        {
            GameObject b = Instantiate(bullet);
            b.SetActive(false);
            bullets.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = player.transform.position;
        Direction = target - (Vector2)transform.position;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < minimumDistance && Time.time > nexTimeToFire)
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

    
}
