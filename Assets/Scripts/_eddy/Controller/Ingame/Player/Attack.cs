using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Attack ----------")]
    public List<GameObject> bulletPool;
    public GameObject bullet;
    public int amoutBulletsToPool;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float range = 0.6f;
    public float damage = 5;
    public float attacksPerSec = 3f;
    private float lastTimeAttack = 0f;
    public float FireRate;
    public float bulletForce = 20f;

    void InitForAttack()
    {
        Transform pool = GameObject.Find("BulletPool").transform;
        bulletPool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amoutBulletsToPool; i++)
        {
            temp = Instantiate(bullet, pool);
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    void MyPlayerAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = target - (Vector2)transform.position;
        float rotateValue = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(new Vector3(0, 0, rotateValue));
    }

    void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= lastTimeAttack + 1 / attacksPerSec)
        {
            anim.SetTrigger("IsAttacking");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MonsterController>().Hurt();
            }

            lastTimeAttack = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
