using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public bool facingRight = true;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Vertical", movement.x);
        animator.SetFloat("Horizontal", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            bool success = TryMove(movement);

            if (!success)
            {
                success = TryMove(new Vector2(movement.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, movement.y));
            }
            animator.speed = moveSpeed;
        }
        else
        {
            animator.speed = 0;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                        direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                        movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                        castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                        moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
            if (count == 0)
            {
                if (movement.x > 0 && !facingRight)
                    Flip();
                else if (movement.x < 0 && facingRight)
                    Flip();
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
