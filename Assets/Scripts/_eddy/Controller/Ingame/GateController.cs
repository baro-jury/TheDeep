using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateController : MonoBehaviour
{
    public TilemapRenderer Renderer { get; set; }
    public TilemapCollider2D Collider2D { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<TilemapRenderer>();
        Collider2D = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
