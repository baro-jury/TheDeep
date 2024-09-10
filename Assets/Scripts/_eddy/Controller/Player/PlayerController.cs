using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction moveInputAction;
    private InputAction attackInputAction;

    private Vector2 moveDirection;

    [SerializeField]
    private float moveSpeed = 5f;
    public Rigidbody2D rb2D;
    public Animator anim;
    Vector2 movement;
    public bool facingRight = true;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

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
    }
}
