using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateController : MonoBehaviour
{
    [HideInInspector] public TilemapRenderer renderer;
    [HideInInspector] public TilemapCollider2D collider;

    public RoomController room;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<TilemapRenderer>();
        collider = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
