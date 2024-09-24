using System.Collections;
using System.Collections.Generic;
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
        CleanTilemap(roomNor.ground);
        //CleanTilemap(roadH.ground);
        //CleanTilemap(roadV.ground);
        //print("room: " + roomNor.ground.size);
        //print("roadH: " + roadH.ground.size);
        //print("roadV: " + roadV.ground.size);
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

    void CleanTilemap(Tilemap tilemap)
    {
        if (tilemap == null) return;

        BoundsInt bounds = tilemap.cellBounds;
        print(bounds);
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null)
                {
                    // Set the tile at this position to null, effectively "removing" it
                    Vector3Int position = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                    tilemap.SetTile(position, null);
                }
            }
        }
    }
}
