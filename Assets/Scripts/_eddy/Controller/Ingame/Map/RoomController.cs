using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    [HideInInspector] public RoomController[] neighbors = { null, null, null, null };
    [HideInInspector] public List<GateController> availableGates;

    [HideInInspector] public bool isFirstRoom;
    [HideInInspector] public bool isLastRoom;
    [HideInInspector] public bool isActivated;
    [HideInInspector] public bool isClear;

    public Tilemap ground;
    public GateController[] gates;

    void Awake()
    {
        availableGates = new List<GateController>();
        isFirstRoom = false;
        isLastRoom = false;
    }

    void Start()
    {
        isActivated = false;
        isClear = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isClear = !isClear;
        }
        ActivateRoom();
        ClearRoom();
    }

    void ActivateRoom()
    {
        if (isFirstRoom || isLastRoom || !isActivated) return;

        foreach (var gate in availableGates)
        {
            gate.gateCollider.isTrigger = false;
            gate.gateRenderer.maskInteraction = SpriteMaskInteraction.None;
        }

    }

    void ClearRoom()
    {
        if (!isActivated) return;

        if (!isClear) return;

        foreach (var gate in availableGates)
        {
            gate.gateCollider.isTrigger = true;
            gate.gateRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
}
