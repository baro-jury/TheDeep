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
        if (playerData.isInitialized)
        {
            currentHP = playerData.health;
            currentShield = playerData.shield;
        }
        else
        {
            currentHP = player.health;
            currentShield = player.shield;
            UpdatePlayerData(player.velocity, currentHP, currentShield, player.mana);
            playerData.isInitialized = true;
        }
        IngameController.instance.SetShield(currentShield, player.shield);
        IngameController.instance.SetHP(currentHP, player.health);

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
                UpdatePlayerData(player.velocity, currentHP, currentShield, player.mana);
                shieldRecoveryTimer = 0;
            }
        }

        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            IngameController.instance.Gameover();
        }

    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            print("bo dang bat tu roi");
            return;
        }

        currentShield -= damage;
        if (currentShield < 0)
        {
            currentHP += currentShield;
            currentShield = 0;
        }

        anim.SetTrigger("IsHurt");
        IngameController.instance.SetHP(currentHP, player.health);
        IngameController.instance.SetShield(currentShield, player.shield);
        UpdatePlayerData(player.velocity, currentHP, currentShield, player.mana);
        StartCoroutine(Invulnerable());
    }

    IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }
}