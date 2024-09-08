using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSquares : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    private bool active;
    private bool attackable;
    private bool blocked;
    public bool walkable;
    public bool explored;
    public bool path;
    private Renderer objectRenderer;
    private int damage = 0;
     public float moveUpDistance = 2.0f;
    public Color newColor;
    public Color defaultColor;
    public Color attackColor;
    public Transform Tile;
    public Transform Player;
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;

    List<Node> pathing = new List<Node>();

    private float playerX, playerZ, targetX, targetZ;
    private float distanceX, distanceZ;

    GridManager gridManager;
    GridMovement pathfinder;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<GridMovement>();

        if (Player != null && Tile != null)
        {
            UpdateDistances();
            UpdateActiveState();
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to where the mouse was clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hit the current object (this object)
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform && active)
                {
                    /*   // Move the objectToMove to the clicked position
                       Vector3 newPosition = transform.position;
                       Player.position = newPosition;

                       // Move the object up by the specified distance
                       Vector3 moveUpPosition = newPosition;
                       moveUpPosition.y += moveUpDistance;
                       Player.position = moveUpPosition;
                    */
                    Vector2Int targetCords = hit.transform.GetComponent<Node>().cords;
                    Vector2Int startCords = new Vector2Int((int)Player.transform.position.x, (int)Player.transform.position.z) / gridManager.UnityGridSize;
                    pathfinder.SetNewDestination(startCords, targetCords);
                    RecalculatePath(true);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateDistances();
            UpdateActiveState();
        }

        if (Input.GetKeyDown(KeyCode.K) && Square_Controller.swordHave)
        {
            UpdateDistances();
            attackable = distanceX <= 2.0f || distanceZ <= 2.0f;
        } else if (Input.GetKeyUp(KeyCode.K)){
            attackable = false;
        } else if (Input.GetKeyDown(KeyCode.Space) && attackable){
            damage += 5;
            Debug.Log(damage);
        }

        UpdateVisuals();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCords;
        }
        else
        {
            coordinates = gridManager.GetCoordsFromPos(transform.position);
        }
        StopAllCoroutines();
        pathing.Clear();
        pathing = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < pathing.Count; i++)
        {
            Vector3 startPosition = Player.position;
            Vector3 endPosition = gridManager.GetPosFromCoords(pathing[i].cords);
            float travelPercent = 0f;

            Player.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                Player.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }
    }

    private void UpdateDistances()
    {
        playerX = Player.position.x;
        playerZ = Player.position.z;
        targetX = Tile.position.x;
        targetZ = Tile.position.z;

        distanceX = Mathf.Abs(playerX - targetX);
        distanceZ = Mathf.Abs(playerZ - targetZ);
    }

    private void UpdateActiveState()
    {
        //active = Mathf.Abs(distanceX - distanceZ) <= 1.0f;
        active = distanceX <= 3.0f && distanceZ <= 3.0f;
    }

    private void UpdateVisuals()
    {
        Color color = defaultColor;
        bool setActive = true;

        if (active)
        {
            color = attackable ? attackColor : newColor;
            setActive = false;
        }
        else if (attackable)
        {
            color = attackColor;
        }

        objectRenderer.material.color = color;
        SetDirectionIndicators(setActive);
    }

    private void SetDirectionIndicators(bool active)
    {
        Up.SetActive(active);
        Down.SetActive(active);
        Right.SetActive(active);
        Left.SetActive(active);
    }
}