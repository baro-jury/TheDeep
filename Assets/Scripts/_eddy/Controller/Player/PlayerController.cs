using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction moveInputAction;
    private InputAction attackInputAction;
    private Rigidbody2D rb2D;
    private Animator anim;
    private Vector2 moveDirection;

    [SerializeField]
    private float moveSpeed = 5f;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        moveInputAction = playerInputActions.Player.Move;
        moveInputAction.Enable();

        attackInputAction = playerInputActions.Player.Attack;
        attackInputAction.Enable();
        //attackInputAction.performed += Attack;
    }

    private void OnDisable()
    {
        moveInputAction.Disable();
        attackInputAction.Disable();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        MoveMyPlayer();
    }

    void MoveMyPlayer()
    {
        moveDirection = moveInputAction.ReadValue<Vector2>();

        _Run();
        _Attack();
        _SetAnimator();

        #region sizeMap
        //if (transform.position.x < -28.5f || transform.position.x > 28.5f)
        //{
        //    transform.position = new Vector2(transform.position.x > 28.5f ? 28.5f : -28.5f, transform.position.y);
        //}
        //if (transform.position.y > 8)
        //{
        //    transform.position = new Vector2(transform.position.x, 8);
        //} 
        #endregion

        //Debug.Log(player.MoveVelocity * leftRightDirection + " --- " + player.JumpVelocity * downUpDirection);
        //Debug.Log("Van toc: " + player.Rb.velocity.x + " --- " + player.Rb.velocity.y);
        //Debug.Log("gravity scale = " + player.Rb.gravityScale);
    }

    void _Run()
    {
        rb2D.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);

        if (moveDirection.x > 0)
        {
            Vector3 scale = transform.localScale;
            if (scale.x < 0) scale.x *= -1f;
            transform.localScale = scale;
        }
        else if (moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            if (scale.x > 0) scale.x *= -1f;
            transform.localScale = scale;
        }

    }

    void _Attack()
    {
        if (attackInputAction.IsPressed())
        {
            //int numAttack = Random.Range(1, 4);
            //Debug.Log("attackkkk " + numAttack);
            //anim.SetTrigger("IsAttacking" + numAttack);

            //bool inRange = focusingEnemy == null ? false :
            //Vector3.Distance(transform.position, focusingEnemy.transform.position) < attackRange;
            //if (inRange)
            //{
            //    print("damage = " + player.DataPoint.CoreATK);
            //    focusingEnemy.TakeDamage(player.DataPoint.CoreATK);
            //}
        }
    }

    void _SetAnimator()
    {
        //anim.SetFloat("xVelocity", player.Rb.velocity.x);
        //anim.SetFloat("yVelocity", player.Rb.velocity.y);
        //anim.SetBool("IsGrounded", isGrounded);
        //anim.SetBool("IsFlying", isFlying);

        anim.SetFloat("Vertical", moveDirection.x);
        anim.SetFloat("Horizontal", moveDirection.y);
        anim.SetFloat("Speed", moveDirection.sqrMagnitude);

        anim.speed = moveDirection == Vector2.zero ? 0 : moveSpeed;

    }

    // When go to deadzone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Deadzone"))
        //{
        //    playerHP -= 10;
        //    Debug.Log("player's HP: " + playerHP);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Ban bi ban trung roi");
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Vector3 moveDirection = (transform.position - collision.transform.position).normalized;
            Vector3 targetPosition = transform.position + moveDirection * 1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 2f);
            if (hit.collider != null)
            {
                Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, 0f);
                targetPosition = hitPoint - moveDirection * 0.5f; // Điều chỉnh vị trí đích để tránh va chạm với tường.
            }
            StartCoroutine(MoveToPosition(targetPosition));
            Debug.Log("Ban bi danh trung roi");
            collision.transform.position = collision.transform.position + moveDirection * -2f;

        }
    }
    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveToPosition(transform.position));
    }
}
