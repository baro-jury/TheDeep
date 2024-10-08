using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Attack ----------")]
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float range = 0.6f;
    public float damage = 5;

    public float attackPerSecs = 3f;
    private float nextAttackTime = 0f;

    public GameObject bulletPrefab;
    public float FireRate;

    public float bulletForce = 20f;

    private List<GameObject> listBullet;

    void InitForAttack()
    {
        listBullet = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            listBullet.Add(bullet);
        }
    }

    void MyPlayerAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (Vector3)target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        attackPoint.transform.rotation = rotation;
    }

    void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextAttackTime)
        {
            anim.SetTrigger("IsAttacking");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<MonsterController>().Hurt();
            }

            nextAttackTime = Time.time + 1f / attackPerSecs;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
