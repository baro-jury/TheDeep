using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{

    public Transform player;
    public bool isFliped = false;

    public GameObject attackPoint;
    // Start is called before the first frame update
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFliped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFliped = false;
        } else if(transform.position.x < player.position.x && !isFliped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f, 0f);
            isFliped = true;
        }
    }
}
