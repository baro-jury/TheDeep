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

    protected virtual void UpdateAnimation()
    {
        anim.SetFloat("xVelocity", rb2D.velocity.x);
        anim.SetFloat("yVelocity", rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DMovement(collision);
        OnCollisionEnter2DAttack(collision);
        OnCollisionEnter2DHealth(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DMovement(collision);
    }
}
