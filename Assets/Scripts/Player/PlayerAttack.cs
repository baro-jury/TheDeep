using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float FireRate = 1;
    public float nextTimeToFire = 0.25f;
    public float bulletForce = 3;

    private PlayerInputActions playerInputActions;
    private InputAction attackInputAction;
    private Animator anim;
    private List<GameObject> listBullet;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        InitForAttack();

    }

    void Update()
    {
        MyPlayerAttack();
    }

    private void OnEnable()
    {
        attackInputAction = playerInputActions.Player.Attack;
        attackInputAction.Enable();
        //attackInputAction.performed += Attack;
    }

    private void OnDisable()
    {
        attackInputAction.Disable();
    }

    void InitForAttack()
    {
        listBullet = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            listBullet.Add(bullet);
        }
    }

    void MyPlayerAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (Vector3)target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        firePoint.transform.rotation = rotation;

        if (attackInputAction.IsPressed())
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                anim.SetTrigger("swordAttack");
                GameObject bullet = getBullet();
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = firePoint.transform.rotation;
                bullet.SetActive(true);

            }
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
