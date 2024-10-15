using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    [Header("---------- Stats ----------")]
    public GameObject gameOverUI;

    [SerializeField] private int currentHP, currentShield;
    [SerializeField] private float invulnerableTime;
    [SerializeField] private float shieldRecoveryTime;
    private float shieldRecoveryTimer;
    private bool isInvulnerable;
    private bool isDead;

    void InitStats()
    {
        currentHP = player.health;
        IngameController.instance.SetHP(currentHP, player.health);
        currentShield = player.shield;
        IngameController.instance.SetShield(currentShield, player.shield);

        shieldRecoveryTimer = 0;
        isInvulnerable = false;
        isDead = false;
    }

    void MyPlayerStats()
    {
        if (isInvulnerable)
        {
            shieldRecoveryTimer = 0;
        }

        if (currentShield < player.shield)
        {
            shieldRecoveryTimer += Time.deltaTime;
            if (shieldRecoveryTimer >= shieldRecoveryTime)
            {
                currentShield++;
                IngameController.instance.SetShield(currentShield, player.shield);
                shieldRecoveryTimer = 0;
            }
        }

        //if (currentHP <= 0 && !isDead)
        //{
        //    isDead = true;
        //    Time.timeScale = 0f;
        //    gameOverUI.SetActive(true);
        //}

    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            print("bo dang bat tu roi");
            return;
        }

        //currentHP -= damage;

        currentShield -= damage;
        if (currentShield < 0)
        {
            currentHP += currentShield;
            currentShield = 0;
        }

        anim.SetTrigger("IsHurt");
        IngameController.instance.SetHP(currentHP, player.health);
        IngameController.instance.SetShield(currentShield, player.shield);
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