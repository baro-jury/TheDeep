using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Health ----------")]
    public int currentHP;
    public int numOfHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject gameOverUI;
    public bool isDead;
    public GameObject bossBar;

    [SerializeField] private float invulnerableTime;
    private bool isInvulnerable = false;

    public bool isCurColliding = false;

    void InitForHealth()
    {
        invulnerableTime = 1.5f;
        currentHP = player.health;
        IngameController.instance.SetHP(currentHP, player.health);
        numOfHearts = 5;
    }

    void MyPlayerHealth()
    {
        //if (currentHP > numOfHearts)
        //{
        //    currentHP = numOfHearts;
        //}

        //for (int i = 0; i < IngameController.instance.hearts.Length; i++)
        //{
        //    if (i < currentHP)
        //    {
        //        IngameController.instance.hearts[i].sprite = fullHeart;
        //    }
        //    else
        //    {
        //        IngameController.instance.hearts[i].sprite = emptyHeart;
        //    }
        //    if (i < numOfHearts)
        //    {
        //        IngameController.instance.hearts[i].enabled = true;
        //    }
        //    else
        //    {
        //        IngameController.instance.hearts[i].enabled = false;
        //    }
        //}

    }

    public void Invincible()
    {
        if (isInvulnerable == true)
        {
            invulnerableTime -= Time.deltaTime;

            if (invulnerableTime < 0)
            {
                isInvulnerable = false;
                invulnerableTime = 1.5f;
            }
        }
    }

    public void hurtPlayer(int damageToGive)
    {
        if (true) return;

        currentHP -= damageToGive;
        if (currentHP <= 0 && !isDead)
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