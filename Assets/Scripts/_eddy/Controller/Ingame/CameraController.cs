using Cinemachine;
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

    public CinemachineBrain cinemachine;
    [HideInInspector] public Transform player;

    void Awake()
    {
        MakeInstance();

        cinemachine = gameObject.GetComponent<CinemachineBrain>();
    }

    void Start()
    {
        
    }

}
