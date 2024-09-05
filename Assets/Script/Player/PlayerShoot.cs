using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float FireRate;
    public float nexTimeToFire = 10;
    public Animator animator;
    public InputAction fire;
    public PlayerInputAction action;
    private bool isCollided = false;

    public Collider2D playerCollider;
    private List<GameObject> listBullet;


    public float bulletForce = 20f;
    Vector2 Direction;

    private void Start()
    {
        listBullet = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            listBullet.Add(bullet);
        }
    }

    private void Awake()
    {
        action = new PlayerInputAction();
    }
    private void OnEnable()
    {
        fire = action.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // Direction = target - (Vector2)transform.position;

        /* if (Input.GetButtonDown("Fire1") && Time.time > nexTimeToFire)
         {
             nexTimeToFire = Time.time + 1/FireRate;
             Shoot();
         }*/

        Vector3 direction = (Vector3)target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        firePoint.transform.rotation = rotation;

        // if (Input.GetButtonDown("Fire1"))
        // {
        //    GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        //     bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10;
        // }
    }
    void Shoot()
    {

    }
    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time > nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1 / FireRate;
            animator.SetTrigger("swordAttack");
            GameObject bullet = getBullet();
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            //Debug.Log("Fire: " + bullet.transform.position);
            bullet.SetActive(true);

            //bullet.GetComponent<Rigidbody2D>().AddForce(Direction * bulletForce, ForceMode2D.Impulse);
            //bullet.GetComponent<Rigidbody2D>().velocity = (transform.right * bulletForce);
        }
    }

    public GameObject getBullet()
    {
        foreach (var o in listBullet)
        {
            if (!o.activeSelf)
            {
                o.SetActive(true);
                return o;
            }
        }
        return null;
    }

    public Vector3 ConvertVector(Vector3 dir)
    {
        float directionX = 0;
        float directionY = 0;
        if (dir.x == 0 && dir.y != 0)
        {
            directionY = dir.y / Mathf.Abs(dir.y);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.y == 0 && dir.x != 0)
        {
            directionX = dir.x / Mathf.Abs(dir.x);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.x != 0 && dir.y != 0)
        {
            if (dir.x < 0 && dir.y < 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y > 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            if (dir.x < 0 && dir.y > 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y < 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            return new Vector3(directionX, directionY, 0);
        }
        return Vector3.zero;
    }
    public Vector3 MiniumVector(Vector3 dir)
    {
        Vector3 vectorEquation = ConvertVector(dir);
        float equation = dir.y / dir.x;
        Vector3 goToDirection = new Vector3(0, 0, 0);
        if (vectorEquation.x >= 1)
        {
            goToDirection = new Vector3(0.5f, 0.5f * equation);
        }
        if (vectorEquation.x <= -1)
        {
            goToDirection = new Vector3(-0.5f, -0.5f * equation);
        }
        if (vectorEquation.y >= 1)
        {
            goToDirection = new Vector3(0.5f / equation, 0.5f);
        }
        if (vectorEquation.y <= -1)
        {
            goToDirection = new Vector3(-0.5f / equation, -0.5f);
        }
        //Debug.Log("ConvertVector: " + goToDirection);
        return goToDirection;
    }

}
