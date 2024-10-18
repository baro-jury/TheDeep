using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController : MonoBehaviour
{
    [Header("---------- Stats ----------")]
    public int maxHealth = 5;
    [HideInInspector] public int curHealth;

    void InitStats()
    {
        curHealth = maxHealth;
    }

    void MonsterStats()
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
