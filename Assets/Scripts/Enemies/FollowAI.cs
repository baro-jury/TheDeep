using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    Vector2 movement;
    public bool facingRight = true;
    Animator animator;
    public Rigidbody2D rb;
    public float minimumDistance;
    bool moveAble = true;
    public EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
            moveAble = true;
        if(moveAble == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            if (distance < minimumDistance)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }

            if (movement.x > 0 && !facingRight)
                Flip();
            else if (movement.x < 0 && facingRight)
                Flip();
        }
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
            if (enemyHealth.getCurHealth() <= 1)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            moveAble = false;
        }
    }
}
