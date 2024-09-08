using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize;
    public int UnityGridSize { get { return unityGridSize; } }
    
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            return grid[coords];
        }
        return null;
    }

    public void BlockNode(Vector2Int coords)
    {
        if (grid.ContainsKey(coords)) {
            grid[coords].walkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }

    public Vector2Int GetCoordsFromPos(Vector3 pos)
    {
        Vector2Int coords = new Vector2Int();

        coords.x = Mathf.RoundToInt(pos.x / unityGridSize);
        coords.y = Mathf.RoundToInt(pos.z / unityGridSize);

        return coords;
    }

    public Vector3 GetPosFromCoords(Vector2Int coords)
    {
        Vector3 pos = new Vector3();

        pos.x = coords.x * unityGridSize;
        pos.y = coords.y * unityGridSize;

        return pos;
    }

    private void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(i, y);
                grid.Add(cords, new Node(cords, true));
            }
        }
    }

}
