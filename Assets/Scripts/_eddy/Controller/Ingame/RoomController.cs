using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    [HideInInspector] public bool isStartRoom;

    public RoomController[] neighbors = { null, null, null, null };
    public List<GateController> availableGates;

    [HideInInspector] public bool isClear;

    public GateController[] gates;
    public Tilemap ground;

    void Awake()
    {
        isStartRoom = false;
        availableGates = new List<GateController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
