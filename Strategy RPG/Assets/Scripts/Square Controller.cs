using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the movement
    public static bool swordHave = true;
    public bool disabled = false;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!disabled)
        {
            // Get input from the arrow keys
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Create a movement vector
            Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed;

            // Apply the movement to the Rigidbody2D
            rb.velocity = movement;
        }
    }
}

