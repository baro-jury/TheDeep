using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Health ----------")]
    public int curHealth;
    public int numOfHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject gameOverUI;
    public bool isDead;
    public GameObject bossBar;

    [SerializeField] private float invincibleTime;
    private bool isInvincible = false;

    public bool isCurColliding = false;

    void InitForHealth()
    {
        if (true) return;

        invincibleTime = 1.5f;
        curHealth = 5;
        numOfHearts = 5;
    }

    void MyPlayerHealth()
    {
        if (true) return;

        if (curHealth > numOfHearts)
        {
            curHealth = numOfHearts;
        }

        for (int i = 0; i < IngameController.instance.hearts.Length; i++)
        {
            if (i < curHealth)
            {
                IngameController.instance.hearts[i].sprite = fullHeart;
            }
            else
            {
                IngameController.instance.hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                IngameController.instance.hearts[i].enabled = true;
            }
            else
            {
                IngameController.instance.hearts[i].enabled = false;
            }
        }

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

    public void hurtPlayer(int damageToGive)
    {
        if (true) return;

        curHealth -= damageToGive;
        if (curHealth <= 0 && !isDead)
        {
            /*gameObject.SetActive(false);*/
            isDead = true;
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
            if (bossBar != null)
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