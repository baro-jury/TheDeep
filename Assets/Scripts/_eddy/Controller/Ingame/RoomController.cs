using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public static ushort TOP = 0;
    public static ushort RIGHT = 1;
    public static ushort BOTTOM = 2;
    public static ushort LEFT = 3;

    public GateController[] gates;

    private RoadController[] neighbors = { null, null, null, null };
    private GateController[] availableGates = { null, null, null, null };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
