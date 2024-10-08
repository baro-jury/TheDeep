using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Health ----------")]
    public int maxHealth = 3;
    [HideInInspector] public int curHealth;

    void InitForHealth()
    {
        curHealth = maxHealth;
    }

    void MonsterHealth()
    {
        if (curHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetCurHealth()
    {
        return curHealth;
    }

    public void Hurt()
    {
        Debug.Log(name + " Hurted");
        curHealth--;
    }

    private void OnCollisionEnter2DHealth(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Hurt();
        }
    }
}
