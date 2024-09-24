using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
    private int distanceRoomX, distanceRoomY;

    // Start is called before the first frame update
    void Start()
    {
        roomNor = roomNormal.GetComponent<RoomController>();
        roadH = roadHorizontal.GetComponent<RoadController>();
        roadV = roadVertical.GetComponent<RoadController>();

        //TilemapResizer.instance.TrimTilemap(roomNor.ground);
        //TilemapResizer.instance.TrimTilemap(roadH.ground);
        //TilemapResizer.instance.TrimTilemap(roadV.ground);

        distanceRoomX = roomNor.ground.size.x + roadH.ground.size.x;
        distanceRoomY = roomNor.ground.size.y + roadV.ground.size.y;

        GenerateMap(transform.position);
    }

    void GenerateMap(Vector2 position)
    {
        var room = Instantiate(roomNormal, position, Quaternion.identity, transform);
        roomList.Add(room);
        currentRoom = room;

        //if (roomList.Count < 9)
        //{
        //    Vector2 currentPos = currentRoom.transform.position;
        //    int direction = Random.Range(0, 4);
        //    switch (direction)
        //    {
        //        case MapController.TOP:
        //            GenerateMap(new Vector2(currentPos.x, currentPos.y + roomNor.ground.size.y + roadV.ground.size.y));
        //            break;
        //        case MapController.RIGHT:
        //            GenerateMap(new Vector2(currentPos.x + roomNor.ground.size.x + roadH.ground.size.x, currentPos.y));
        //            break;
        //        case MapController.BOTTOM:
        //            GenerateMap(new Vector2(currentPos.x, currentPos.y - roomNor.ground.size.y - roadV.ground.size.y));
        //            break;
        //        case MapController.LEFT:
        //            GenerateMap(new Vector2(currentPos.x - roomNor.ground.size.x - roadH.ground.size.x, currentPos.y));
        //            break;
        //    }
        //}

        GenerateRoom(currentRoom);
    }

    void GenerateRoom(GameObject fromRoom)
    {
        RoomController fromRoomCtrl = fromRoom.GetComponent<RoomController>();
        int direction = Random.Range(0, 4);
        while (fromRoomCtrl.neighbors[direction] != null)
        {
            direction = Random.Range(0, 4);
        }

        Vector2 currentPos = currentRoom.transform.position;
        switch (direction)
        {
            case MapController.TOP:
                currentPos.y += distanceRoomY;
                break;
            case MapController.RIGHT:
                currentPos.x += distanceRoomX;
                break;
            case MapController.BOTTOM:
                currentPos.y -= distanceRoomY;
                break;
            case MapController.LEFT:
                currentPos.x -= distanceRoomX;
                break;
        }

        var room = Instantiate(roomNormal, currentPos, Quaternion.identity, transform);
        roomList.Add(room);
        currentRoom = room;
        CheckNeighbor(currentRoom);

        if (roomList.Count < 9)
        {
            GenerateRoom(currentRoom);
        }
    }

    void CheckNeighbor(GameObject checkingRoom)
    {
        var checkingRoomCtrl = checkingRoom.GetComponent<RoomController>();

        int checkingRoomDir, roomDir;
        foreach (GameObject room in roomList)
        {
            float distance = Vector2.Distance(room.transform.position, checkingRoom.transform.position);
            if (distance != distanceRoomX && distance != distanceRoomY) continue;
            else if (distance == distanceRoomX)
            {
                float distanceX = room.transform.position.x - checkingRoom.transform.position.x;

                checkingRoomDir = distanceX > 0 ? MapController.RIGHT : MapController.LEFT;
                if (checkingRoomCtrl.neighbors[checkingRoomDir] != null) continue;
                checkingRoomCtrl.neighbors[checkingRoomDir] = room.GetComponent<RoomController>();

                roomDir = distanceX < 0 ? MapController.RIGHT : MapController.LEFT;
                room.GetComponent<RoomController>().neighbors[roomDir] = checkingRoomCtrl;

                Instantiate(roadHorizontal,
                    new Vector2(distanceX > 0 ? checkingRoom.transform.position.x + 26 : checkingRoom.transform.position.x - 20,
                        checkingRoom.transform.position.y),
                    Quaternion.identity, transform);
            }
            else if (distance == distanceRoomY)
            {
                float distanceY = room.transform.position.y - checkingRoom.transform.position.y;

                checkingRoomDir = distanceY > 0 ? MapController.TOP : MapController.BOTTOM;
                if (checkingRoomCtrl.neighbors[checkingRoomDir] != null) continue;
                checkingRoomCtrl.neighbors[checkingRoomDir] = room.GetComponent<RoomController>();

                roomDir = distanceY < 0 ? MapController.TOP : MapController.BOTTOM;
                room.GetComponent<RoomController>().neighbors[roomDir] = checkingRoomCtrl;

                Instantiate(roadVertical,
                    new Vector2(checkingRoom.transform.position.x,
                        distanceY > 0 ? checkingRoom.transform.position.y + 20 : checkingRoom.transform.position.y - 15),
                    Quaternion.identity, transform);
            }

            //float distanceX = room.transform.position.x - checkingRoom.transform.position.x;
            //if (Mathf.Abs(distanceX) == distanceRoomX)
            //{
            //    checkingRoomDir = distanceX > 0 ? MapController.RIGHT : MapController.LEFT;
            //    if (checkingRoomCtrl.neighbors[checkingRoomDir] != null) continue;
            //    checkingRoomCtrl.neighbors[checkingRoomDir] = room.GetComponent<RoomController>();

            //    roomDir = distanceX < 0 ? MapController.RIGHT : MapController.LEFT;
            //    room.GetComponent<RoomController>().neighbors[roomDir] = checkingRoomCtrl;

            //    continue;
            //}

            //float distanceY = room.transform.position.y - checkingRoom.transform.position.y;
            //if (Mathf.Abs(distanceY) == distanceRoomY)
            //{
            //    checkingRoomDir = distanceY > 0 ? MapController.TOP : MapController.BOTTOM;
            //    if (checkingRoomCtrl.neighbors[checkingRoomDir] != null) continue;
            //    checkingRoomCtrl.neighbors[checkingRoomDir] = room.GetComponent<RoomController>();

            //    roomDir = distanceY < 0 ? MapController.TOP : MapController.BOTTOM;
            //    room.GetComponent<RoomController>().neighbors[roomDir] = checkingRoomCtrl;

            //    continue;
            //}
        }
    }

}
