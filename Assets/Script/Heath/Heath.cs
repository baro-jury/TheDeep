using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Heath : MonoBehaviour
{
    public int curHealth;
    public int numOfHearts;
    public Animator playerDie;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject gameOverUI;
    public bool isDead;
    public GameObject bossBar;

    [SerializeField] private float invincibleTime;
    private bool isInvincible = false;

    public bool isCurColliding = false;

    private void Start()
    {
        invincibleTime = 1.5f;
        curHealth = 5;
        numOfHearts = 5;
    }
    void Update()
    {
        if (curHealth > numOfHearts)
        {
            curHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        
        /*if (isCurColliding && isInvincible == false)
        {
            Debug.Log("-1 life");
            curHealth -= 1;
            isInvincible = true;
            isCurColliding = false;
        }
        Invincible();*/

    }

    public void Invincible()
    {
        if (isInvincible == true)
        {
            invincibleTime -= Time.deltaTime;

            if (invincibleTime < 0)
            {
                isInvincible = false;
                invincibleTime = 1.5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("trung dan");
            isCurColliding = true;
        }
        if (curHealth < 1)
        {
            Debug.Log("You die");
            return;
        }*/
    }

    public void hurtPlayer(int damageToGive)
    {
        curHealth -= damageToGive;
        if(curHealth <= 0 && !isDead)
        {
            /*gameObject.SetActive(false);*/
            isDead = true;
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
            if(bossBar != null)
            {
                bossBar.SetActive(false);
            }
        }
    }
    public void GameOverRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene_2");
    }
    public void MainMenuOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
