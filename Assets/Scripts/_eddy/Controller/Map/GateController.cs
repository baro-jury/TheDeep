using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateController : MonoBehaviour
{
    [HideInInspector] public TilemapRenderer renderer;
    [HideInInspector] public CompositeCollider2D collider;

    public RoomController room;

    void Awake()
    {
        renderer = GetComponent<TilemapRenderer>();
        collider = GetComponent<CompositeCollider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        room.isActivated = true;
    }
}
