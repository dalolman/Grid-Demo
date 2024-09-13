using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int cords;
    public bool walkable;
    public bool explored;
    public bool path;
    public Node connectTo;

    public Node(Vector2Int cords, bool walkable)
    {
        this.cords = cords;
        this.walkable = walkable;
        cords.x = (int)this.transform.position.x;
        cords.y = (int)this.transform.position.y;
    }
}
