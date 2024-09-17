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
        //attackInputAction.performed += Attack;
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
        //anim.SetFloat("xVelocity", player.Rb.velocity.x);
        //anim.SetFloat("yVelocity", player.Rb.velocity.y);
        //anim.SetBool("IsGrounded", isGrounded);
        //anim.SetBool("IsFlying", isFlying);

        anim.SetFloat("Vertical", moveDirection.x);
        anim.SetFloat("Horizontal", moveDirection.y);
        anim.SetFloat("Speed", moveDirection.sqrMagnitude);

        anim.speed = moveDirection == Vector2.zero ? 0 : player.moveVelocity;
    }

}