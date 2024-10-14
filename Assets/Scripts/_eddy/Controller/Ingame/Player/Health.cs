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
        //if (currentHP <= 0 && !isDead)
        //{
        //    isDead = true;
        //    Time.timeScale = 0f;
        //    gameOverUI.SetActive(true);
        //    if (bossBar != null)
        //    {
        //        bossBar.SetActive(false);
        //    }
        //}

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

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            print("bo dang bat tu roi");
            return;
        }

        currentHP -= damage;
        IngameController.instance.SetHP(currentHP, player.health);
        StartCoroutine(Invulnerable());
    }

    IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
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