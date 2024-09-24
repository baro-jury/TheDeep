using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    public RoomController[] neighbors = { null, null, null, null };
    private GateController[] availableGates = { null, null, null, null };
    [HideInInspector] public bool isClear;

    public GateController[] gates;
    public Tilemap ground;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
