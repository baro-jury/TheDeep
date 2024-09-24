using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    [HideInInspector]
    public Transform player;

    public float minX, maxX, minY, maxY;

    void Awake()
    {
        MakeInstance();
    }
    void Update()
    {
        if (player != null)
        {
            Vector3 tempPos = transform.position;
            tempPos.x = player.position.x;
            tempPos.y = player.position.y;
            //if (temp.x < minX)
            //{
            //    temp.x = minX;
            //}
            //else if (temp.x > maxX)
            //{
            //    temp.x = maxX;
            //}
            //temp.y = player.position.y;
            //if (temp.y < minY)
            //{
            //    temp.y = minY;
            //}
            //else if (temp.y > maxY)
            //{
            //    temp.y = maxY;
            //}
            transform.position = tempPos;
        }
    }
}
