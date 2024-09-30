using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    [HideInInspector] public RoomController[] neighbors = { null, null, null, null };
    [HideInInspector] public List<GateController> availableGates;

    [HideInInspector] public bool isStartRoom;
    [HideInInspector] public bool isActivated;
    [HideInInspector] public bool isClear;

    public Tilemap ground;
    public GateController[] gates;

    void Awake()
    {
        isStartRoom = false;
        availableGates = new List<GateController>();
    }

    void Start()
    {
        isActivated = false;
        isClear = false;
    }

    // Update is called once per frame
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
        if (!isStartRoom && isActivated)
        {
            foreach (var gate in availableGates)
            {
                gate.collider.isTrigger = false;
                gate.renderer.maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

    void ClearRoom()
    {
        if (isActivated && isClear)
        {
            foreach (var gate in availableGates)
            {
                gate.collider.isTrigger = true;
                gate.renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            }
        }
    }
}
