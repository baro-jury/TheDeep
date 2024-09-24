using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapResizer : MonoBehaviour
{
    public static TilemapResizer instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Awake()
    {
        MakeInstance();
    }

    public void TrimTilemap(Tilemap tilemap)
    {
        if (tilemap == null) return;

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        BoundsInt newBounds = new BoundsInt();
        bool hasStarted = false;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    if (!hasStarted)
                    {
                        newBounds.xMin = x;
                        newBounds.yMin = y;
                        newBounds.xMax = x;
                        newBounds.yMax = y;
                        hasStarted = true;
                    }
                    else
                    {
                        if (x < newBounds.xMin) newBounds.xMin = x;
                        if (y < newBounds.yMin) newBounds.yMin = y;
                        if (x > newBounds.xMax) newBounds.xMax = x;
                        if (y > newBounds.yMax) newBounds.yMax = y;
                    }
                }
            }
        }

        newBounds.size = new Vector3Int(newBounds.size.x + 1, newBounds.size.y + 1, 1);

        TileBase[] newAllTiles = tilemap.GetTilesBlock(newBounds);
        tilemap.ClearAllTiles();
        tilemap.SetTilesBlock(newBounds, newAllTiles);

        print(tilemap.name + ": " + tilemap.size);
    }
}
