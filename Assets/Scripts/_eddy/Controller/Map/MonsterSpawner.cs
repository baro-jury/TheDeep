using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    public RoomController room;
    public List<GameObject> monsterList;

    private List<Vector3> availableSpawnPositions = new List<Vector3>();
    private bool canSpawn;
    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        //player = GameObject.FindWithTag("Player");

        InitializeSpawnPositions();
    }

    void Update()
    {
        //if (player == null)
        //{
        //    player = GameObject.FindWithTag("Player");
        //}
        SpawnMonsters();
        KillMonsters();
    }

    void InitializeSpawnPositions()
    {
        availableSpawnPositions.Clear();
        BoundsInt bounds = room.ground.cellBounds;
        TileBase[] allTiles = room.ground.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int localPlace = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                    Vector3 place = room.ground.CellToWorld(localPlace);
                    availableSpawnPositions.Add(place);
                }
            }
        }
    }

    void SpawnMonsters()
    {
        if (!room.isStartRoom && room.isActivated)
        {
            if (!canSpawn) return;

            //int numOfMonster = Random.Range(5, 7);
            int numOfMonster = 1;
            for (int i = 0; i < numOfMonster; i++)
            {
                if (availableSpawnPositions.Count > 0)
                {
                    int randomIndex = Random.Range(0, availableSpawnPositions.Count);
                    Vector3 spawnPosition = availableSpawnPositions[randomIndex] + new Vector3(0.5f, 0.5f, 0);
                    Instantiate(monsterList[Random.Range(0, monsterList.Count)], spawnPosition, Quaternion.identity, transform);

                    availableSpawnPositions.Remove(availableSpawnPositions[randomIndex]);
                }
                else
                {
                    Debug.Log("No available positions to spawn the enemy.");
                }

                if (i == (numOfMonster - 1)) canSpawn = false;
            }
        }
    }

    void KillMonsters()
    {
        if (!canSpawn && transform.childCount == 0)
        {
            room.isClear = true;
        }
    }
}
