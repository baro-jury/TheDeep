using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
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

    void Update()
    {
        MonsterAttack();
        MonsterHealth();

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        MonsterMove();
    }

    void UpdateAnimation()
    {
        anim.SetBool("isMove", rb2D.velocity != Vector2.zero);
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
