using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateController : MonoBehaviour
{
    [HideInInspector] public TilemapRenderer gateRenderer;
    [HideInInspector] public CompositeCollider2D gateCollider;

    public RoomController room;

    void Awake()
    {
        gateRenderer = GetComponent<TilemapRenderer>();
        gateCollider = GetComponent<CompositeCollider2D>();
    }

    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        room.isActivated = true;
    }
}
