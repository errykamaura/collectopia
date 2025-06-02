using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap platformTilemap; // Tilemap untuk tanah/platform
    public float coinOffsetY = 1.5f; // Biar koin tidak nempel tile

    // Koin melayang dengan berbagai pola
    public List<Vector3> extraCoinPositions = new List<Vector3>()
    {
        // Pola lengkung
        new Vector3(5, 4, 0),
        new Vector3(6, 5, 0),
        new Vector3(7, 6, 0),
        new Vector3(8, 5, 0),
        new Vector3(9, 4, 0),

        // Garis datar tinggi
        new Vector3(14, 6, 0),
        new Vector3(15, 6, 0),
        new Vector3(16, 6, 0),

        // Zigzag
        new Vector3(20, 4, 0),
        new Vector3(21, 5, 0),
        new Vector3(22, 6, 0),
        new Vector3(23, 5, 0),
        new Vector3(24, 4, 0),

        // Tinggi di atas
        new Vector3(28, 7, 0),
        new Vector3(29, 7, 0),
        new Vector3(30, 7, 0),
    };

    void Start()
    {
        // Bersihkan coin lama agar tidak dobel saat play ulang
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn coin
        SpawnCoinsAbovePlatformTiles();
        SpawnExtraCoins();

        // Hitung jumlah coin yang di-spawn dan laporkan ke CoinManager
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
                // Cek apakah di atas tile tidak ada tile lagi
                Vector3Int tileAbove = new Vector3Int(cellPos.x, cellPos.y + 1, cellPos.z);
                TileBase aboveTile = platformTilemap.GetTile(tileAbove);

                if (aboveTile == null)
                {
                    // Spawn hanya setiap 3 tile agar tidak terlalu padat
                    if (counter % 3 == 0)
                    {
                        Vector3 worldPos = platformTilemap.CellToWorld(cellPos) + new Vector3(0.5f, coinOffsetY, 0f);
                        worldPos.z = 0f; // Pastikan Z = 0 agar tidak tersembunyi
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
            Vector3 worldPos = new Vector3(pos.x, pos.y, 0f); // Pastikan Z = 0
            Instantiate(coinPrefab, worldPos, Quaternion.identity, this.transform);
        }
    }
}
