using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMgr : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile2 tilePrefab;

    [SerializeField] private Transform cam;

    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(i,y), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {y}";

                var isOffset = (i % 2 == 0 && y % 2 != 0) || (i % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
