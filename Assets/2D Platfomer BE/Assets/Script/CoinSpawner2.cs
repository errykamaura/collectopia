using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CoinSpawner2 : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap platformTilemap;
    public float coinOffsetY = 1.5f;

    // Coin manual tersebar dari awal hingga akhir
    public List<Vector3> extraCoinPositions = new List<Vector3>()
    {
        // Bagian awal (minimal)
        new Vector3(6, 3, 0),

        // Tengah level
        new Vector3(14, 5, 0),
        new Vector3(15, 5.5f, 0),
        new Vector3(16, 5, 0),

        // Zigzag ringan
        new Vector3(21, 5, 0),
        new Vector3(22, 5.5f, 0),
        new Vector3(23, 5, 0),

        // Akhir level
        new Vector3(28, 6, 0),
        new Vector3(29, 6, 0),
        new Vector3(30, 6, 0),
    };

    void Start()
    {
        // Bersihkan coin lama
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        SpawnCoinsAbovePlatformTiles();
        SpawnExtraCoins();

        // Set total coin ke CoinManager
        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            CoinManager cm = gm.GetComponent<CoinManager>();
            if (cm != null)
            {
                cm.totalCoins = transform.childCount;
            }
        }
        else
        {
            Debug.LogWarning("GameManager tidak ditemukan di scene.");
        }
    }

    void SpawnCoinsAbovePlatformTiles()
    {
        if (platformTilemap == null)
        {
            Debug.LogWarning("Tilemap platform belum diassign!");
            return;
        }

        BoundsInt bounds = platformTilemap.cellBounds;
        int counter = 0;

        foreach (Vector3Int cellPos in bounds.allPositionsWithin)
        {
            TileBase tile = platformTilemap.GetTile(cellPos);
            if (tile != null)
            {
                Vector3Int tileAbove = new Vector3Int(cellPos.x, cellPos.y + 1, cellPos.z);
                TileBase aboveTile = platformTilemap.GetTile(tileAbove);

                if (aboveTile == null)
                {
                    // Spawn setiap 6 tile agar tidak menumpuk
                    if (counter % 6 == 0)
                    {
                        Vector3 worldPos = platformTilemap.CellToWorld(cellPos) + new Vector3(0.5f, coinOffsetY, 0f);
                        worldPos.z = 0f;
                        Instantiate(coinPrefab, worldPos, Quaternion.identity, this.transform);
                    }
                    counter++;
                }
            }
        }
    }

    void SpawnExtraCoins()
    {
        foreach (Vector3 pos in extraCoinPositions)
        {
            Vector3 worldPos = new Vector3(pos.x, pos.y, 0f);
            Instantiate(coinPrefab, worldPos, Quaternion.identity, this.transform);
        }
    }
}
