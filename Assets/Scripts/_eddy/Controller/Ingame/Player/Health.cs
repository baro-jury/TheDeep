using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Health ----------")]
    public GameObject gameOverUI;
    public GameObject bossBar;

    [SerializeField] private int currentHP;
    private bool isDead;
    [SerializeField] private float invulnerableTime;
    private bool isInvulnerable = false;

    void InitForHealth()
    {
        isDead = false;
        invulnerableTime = 1.5f;
        currentHP = player.health;
        IngameController.instance.SetHP(currentHP, player.health);
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