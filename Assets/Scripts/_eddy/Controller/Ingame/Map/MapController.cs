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
    public int numberOfRoom;
    public List<GameObject> roomList;

    private RoomController roomNor;
    private RoadController roadH, roadV;

    private GameObject currentRoom;
    private int absDistanceRoomX, absDistanceRoomY;

    void Start()
    {
        roomList = new List<GameObject>();

        roomNor = roomNormal.GetComponent<RoomController>();
        roadH = roadHorizontal.GetComponent<RoadController>();
        roadV = roadVertical.GetComponent<RoadController>();

        //TilemapResizer.instance.TrimTilemap(roomNor.ground);
        //TilemapResizer.instance.TrimTilemap(roadH.ground);
        //TilemapResizer.instance.TrimTilemap(roadV.ground);

        absDistanceRoomX = roomNor.ground.size.x + roadH.ground.size.x;
        absDistanceRoomY = roomNor.ground.size.y + roadV.ground.size.y;

        GenerateMap(transform.position);
        OpenGates(roomList);
    }

    void GenerateMap(Vector2 position)
    {
        if (roomList.Count >= numberOfRoom) return;

        currentRoom = Instantiate(roomNormal, position, Quaternion.identity, transform);
        currentRoom.GetComponent<RoomController>().isFirstRoom = true;
        roomList.Add(currentRoom);

        GenerateRoom(currentRoom);
    }

    void GenerateRoom(GameObject fromRoom)
    {
        RoomController fromRoomCtrl = fromRoom.GetComponent<RoomController>();
        if (fromRoomCtrl == null) return;

        List<int> availableDirections = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (fromRoomCtrl.neighbors[i] == null)
            {
                availableDirections.Add(i);
            }
        }
        if (availableDirections.Count == 0) return;
        int direction = availableDirections[Random.Range(0, availableDirections.Count)];

        Vector2 pos = fromRoom.transform.position;
        switch (direction)
        {
            case MapController.TOP:
                pos += Vector2.up * absDistanceRoomY;
                break;
            case MapController.RIGHT:
                pos += Vector2.right * absDistanceRoomX;
                break;
            case MapController.BOTTOM:
                pos += Vector2.down * absDistanceRoomY;
                break;
            case MapController.LEFT:
                pos += Vector2.left * absDistanceRoomX;
                break;
        }

        currentRoom = Instantiate(roomNormal, pos, Quaternion.identity, transform);
        roomList.Add(currentRoom);
        if (roomList.Count == numberOfRoom)
        {
            currentRoom.GetComponent<RoomController>().isLastRoom = true;
        }
        CheckNeighbors(currentRoom);

        if (roomList.Count < numberOfRoom)
        {
            GenerateRoom(currentRoom);
        }
    }

    void CheckNeighbors(GameObject checkingRoom)
    {
        RoomController checkingRoomCtrl = checkingRoom.GetComponent<RoomController>();
        if (checkingRoomCtrl == null) return;

        foreach (var room in roomList)
        {
            if (room == checkingRoom) continue;
            RoomController roomCtrl = room.GetComponent<RoomController>();
            if (roomCtrl == null) continue;

            Vector2 distance = room.transform.position - checkingRoom.transform.position;
            float absDistanceX = Mathf.Abs(distance.x);
            float absDistanceY = Mathf.Abs(distance.y);

            if (absDistanceX == absDistanceRoomX && absDistanceY == 0)
            {
                LinkRooms(checkingRoom, checkingRoomCtrl, roomCtrl, distance.x, true);
                //if (checkingRoomCtrl.isLastRoom) break;
            }
            else if (absDistanceY == absDistanceRoomY && absDistanceX == 0)
            {
                LinkRooms(checkingRoom, checkingRoomCtrl, roomCtrl, distance.y, false);
                //if (checkingRoomCtrl.isLastRoom) break;
            }
        }
    }

    void LinkRooms(GameObject checkingRoom, RoomController checkingRoomCtrl, RoomController roomCtrl, float distance, bool isHorizontal)
    {
        int checkingRoomDir = isHorizontal ? (distance > 0 ? MapController.RIGHT : MapController.LEFT) : (distance > 0 ? MapController.TOP : MapController.BOTTOM);
        if (checkingRoomCtrl.neighbors[checkingRoomDir] != null) return;
        checkingRoomCtrl.neighbors[checkingRoomDir] = roomCtrl;

        int roomDir = isHorizontal ? (distance < 0 ? MapController.RIGHT : MapController.LEFT) : (distance < 0 ? MapController.TOP : MapController.BOTTOM);
        roomCtrl.neighbors[roomDir] = checkingRoomCtrl;

        if (roomCtrl.isFirstRoom && roomCtrl.neighbors.Count(neighbor => neighbor != null) >= 2) return;

        checkingRoomCtrl.availableGates.Add(checkingRoomCtrl.gates[checkingRoomDir]);
        roomCtrl.availableGates.Add(roomCtrl.gates[roomDir]);

        Vector2 position = (Vector2)checkingRoom.transform.position + (isHorizontal ? new Vector2((distance > 0 ? 26 : -20), 0) : new Vector2(0, (distance > 0 ? 20 : -15)));
        Instantiate(isHorizontal ? roadHorizontal : roadVertical, position, Quaternion.identity, transform);
    }

    void OpenGates(List<GameObject> roomsInMap)
    {
        foreach (var room in roomsInMap)
        {
            RoomController roomCtrl = room.GetComponent<RoomController>();
            if (roomCtrl == null) continue;

            foreach (var gate in roomCtrl.availableGates)
            {
                gate.gateCollider.isTrigger = true;
                gate.gateRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            }
        }
    }

}
