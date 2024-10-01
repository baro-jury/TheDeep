using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Follow ----------")]
    public float speed;
    private float distance;
    Vector2 movement;
    public bool facingRight = true;
    public float minimumDistanceFollow;
    bool moveable = true;

    private Monster monster;
    private Rigidbody2D rb2D;
    private Animator anim;

    [HideInInspector] public PlayerController target;

    void Awake()
    {
        monster = GetComponent<Monster>();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InitForAttack();
        InitForHealth();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        moveable = true;
        if (moveable == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            Vector2 direction = target.transform.position - transform.position;
            facingRight = direction.x > 0;
            direction.Normalize();
            if (distance < minimumDistanceFollow)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }

            if (movement.x > 0 && !facingRight)
                Flip();
            else if (movement.x < 0 && facingRight)
                Flip();
        }

        MonsterAttack();
        MonsterHealth();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (GetCurHealth() <= 1)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            moveable = false;
        }

        OnCollisionEnter2DAttack(collision);
        OnCollisionEnter2DHealth(collision);
    }
}
