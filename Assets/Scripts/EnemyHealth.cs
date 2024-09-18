using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [HideInInspector] public int curHealth;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public int getCurHealth()
    {
        return curHealth;
    }

    public void Hurt()
    {
        Debug.Log(name + " Hurted");
        curHealth = curHealth - 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Hurt();
        }
    }
}
