using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquares : MonoBehaviour
{
    private bool active;
    private bool attackable;
    private bool blocked;
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

    private float playerX, playerZ, targetX, targetZ;
    private float distanceX, distanceZ;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

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
                Debug.Log("Mouse clicked on " + hit);
                if (hit.transform == transform && active)
                {
                    // Move the objectToMove to the clicked position
                    Vector3 newPosition = transform.position;
                    Player.position = newPosition;

                    // Move the object up by the specified distance
                    Vector3 moveUpPosition = newPosition;
                    moveUpPosition.y += moveUpDistance;
                    Player.position = moveUpPosition;
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