using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public GameObject wallRayObj;

    private Vector2 moveDirection;

    void MyPlayerMove()
    {
        //if (!IsBlocked())
        //{
        //    rb2D.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);
        //}
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
