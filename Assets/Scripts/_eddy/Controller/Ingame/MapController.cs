using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public const ushort TOP = 0;
    public const ushort RIGHT = 1;
    public const ushort BOTTOM = 2;
    public const ushort LEFT = 3;

    [Header("Object")]
    public GameObject roomNormal;
    public GameObject roadHorizontal, roadVertical;

    [Header("Map")]
    public List<GameObject> roomList;

    private RoomController roomNor;
    private RoadController roadH, roadV;
    private GameObject currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        roomNor = roomNormal.GetComponent<RoomController>();
        roadH = roadHorizontal.GetComponent<RoadController>();
        roadV = roadVertical.GetComponent<RoadController>();
        TilemapResizer.instance.TrimTilemap(roomNor.ground);
        TilemapResizer.instance.TrimTilemap(roadH.ground);
        TilemapResizer.instance.TrimTilemap(roadV.ground);

        GenerateMap(transform.position);
    }

    void GenerateMap(Vector2 position)
    {
        var room = Instantiate(roomNormal, position, Quaternion.identity, transform);
        roomList.Add(room);
        currentRoom = room;

        if (roomList.Count < 9)
        {
            Vector2 currentPos = currentRoom.transform.position;
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case MapController.TOP:
                    GenerateMap(new Vector2(currentPos.x, currentPos.y + roomNor.ground.size.y + roadV.ground.size.y));
                    break;
                case MapController.RIGHT:
                    GenerateMap(new Vector2(currentPos.x + roomNor.ground.size.x + roadH.ground.size.x, currentPos.y));
                    break;
                case MapController.BOTTOM:
                    GenerateMap(new Vector2(currentPos.x, currentPos.y - roomNor.ground.size.y - roadV.ground.size.y));
                    break;
                case MapController.LEFT:
                    GenerateMap(new Vector2(currentPos.x - roomNor.ground.size.x - roadH.ground.size.x, currentPos.y));
                    break;
            }
        }
    }

}
