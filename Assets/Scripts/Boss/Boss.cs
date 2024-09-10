using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

    private int maxHealth = 20;
    private int curHealth;
    [SerializeField] private int damgeToTake = 1;
    public Animator animator;
/*    public GameObject gameOverUI;
*/
    [SerializeField]private Rigidbody2D rb;

    public HealthBar healthBar;
    void Start()
    {
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurHealth()
    {
        return curHealth;
    }

    public void TakingDamage(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
        if (curHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        animator.Play("Boss_Death");
/*        gameOverUI.SetActive(true);
*/        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }
    public void MainMenuOver()
    {
        SceneManager.LoadScene("Menu");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakingDamage(damgeToTake);
            Debug.Log("ban trung boss");
        }
    }

    private IEnumerator WaitAndDoSomething(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
}
