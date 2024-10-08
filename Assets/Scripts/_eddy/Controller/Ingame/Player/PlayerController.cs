using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerInputActions playerInputActions;
    private InputAction moveInputAction;
    private InputAction attackInputAction;
    private Rigidbody2D rb2D;
    private Animator anim;

    void Awake()
    {
        player = GetComponent<Player>();
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        CameraController.instance.player = player.transform;

        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InitForAttack();
        InitForHealth();
    }

    private void OnEnable()
    {
        moveInputAction = playerInputActions.Player.Move;
        moveInputAction.Enable();

        attackInputAction = playerInputActions.Player.Attack;
        attackInputAction.Enable();
        attackInputAction.performed += Attack;
    }

    private void OnDisable()
    {
        moveInputAction.Disable();
        attackInputAction.Disable();
    }

    void Update()
    {
        InputResponse();

        MyPlayerAttack();
        MyPlayerHealth();

        UpdateAnimation();

        float force = 1000f;
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            print("trai");
            rb2D.AddForce(Vector2.left * force);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            print("tren");
            rb2D.AddForce(Vector2.up * force);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            print("phai");
            rb2D.AddForce(Vector2.right * force);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            print("duoi");
            rb2D.AddForce(Vector2.down * force);
        }
    }

    void FixedUpdate()
    {
        MyPlayerMove();
    }

    void InputResponse()
    {
        moveDirection = moveInputAction.ReadValue<Vector2>();
    }

    void UpdateAnimation()
    {
        //anim.SetFloat("velocity", rb2D.velocity.magnitude);
        anim.SetFloat("xVelocity", rb2D.velocity.x);
        anim.SetFloat("yVelocity", rb2D.velocity.y);
        //anim.SetBool("IsGrounded", isGrounded);
        //anim.SetBool("IsFlying", isFlying);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DMovement(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DMovement(collision);
    }

}