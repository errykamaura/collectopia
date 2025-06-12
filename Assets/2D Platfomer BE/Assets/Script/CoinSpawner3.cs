using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CoinSpawner3 : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap platformTilemap;
    public float coinOffsetY = 1.5f;

    // Posisi manual berdasarkan desain level 3
    public List<Vector3> extraCoinPositions = new List<Vector3>()
    {
        // Awal level
        new Vector3(2, 1, 0),
        new Vector3(3, 1, 0),
        new Vector3(4, 1, 0),

        // Platform tengah 1
        new Vector3(9, 2, 0),

        // Lonjakan platform
        new Vector3(13, 3, 0),
        new Vector3(14, 3, 0),

        // Platform lebar 1
        new Vector3(18, 4, 0),
        new Vector3(19, 4, 0),
        new Vector3(20, 4, 0),

        // Tebing tinggi (vertical)
        new Vector3(22, 2, 0),
        new Vector3(22, 3, 0),
        new Vector3(22, 4, 0),

        // Jalur menurun
        new Vector3(27, 3, 0),
        new Vector3(29, 3, 0),

        // Blok akhir kiri
        new Vector3(33, 2, 0),
        new Vector3(34, 2, 0),
        new Vector3(35, 2, 0),

        // Blok akhir besar
        new Vector3(38, 2, 0),
        new Vector3(39, 2, 0),
        new Vector3(40, 2, 0),
        new Vector3(38.5f, 2.5f, 0),
        new Vector3(39.5f, 2.5f, 0),
    };

    void Start()
    {
        // Bersihkan coin lama
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        SpawnCoinsAbovePlatformTiles(); // optional kalau tilemap mendukung
        SpawnExtraCoins();

        // Set total coin ke CoinManager2
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
            Debug.Log("Tilemap platform tidak diset, skip spawn otomatis.");
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
