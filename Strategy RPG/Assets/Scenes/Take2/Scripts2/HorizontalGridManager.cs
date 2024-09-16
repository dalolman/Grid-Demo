using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalGridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private GameObject tilePrefab;

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
                var spawnedTile = Instantiate(tilePrefab, new Vector3(i, 0, y), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {y}";

                var isOffset = (i % 2 == 0 && y % 2 != 0) || (i % 2 != 0 && y % 2 == 0);
              //  spawnedTile.Init(isOffset);
            }
        }
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, 10, (float)height / 2 - 0.5f);
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
