using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudent : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    private Vector2 dest = Vector2.zero; // Destination position
    public event Action<Vector2> OnMove; // Event to notify movement


    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position; // Initialize the destination position to the current position


    }

    // Update is called once per frame
    void Update()
    {
        updateMove(); // Update movement direction
        Move(); // Move the game object
    }

    private void updateMove()
    {
        // Check if a key is pressed and update the destination position (dest)
        if (Input.GetKeyDown(KeyCode.W))
        {
            dest = (Vector2)transform.position + Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dest = (Vector2)transform.position + Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dest = (Vector2)transform.position + Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dest = (Vector2)transform.position + Vector2.right;
        }


        Vector2 dir = dest - (Vector2)transform.position; // Calculate the movement direction
        GetComponent<Animator>().SetFloat("DirX", dir.x); // Set the animation parameter DirX
        GetComponent<Animator>().SetFloat("DirY", dir.y); // Set the animation parameter DirY
    }

    private void Move()
    {
        // Move towards the destination position using the MoveTowards method
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(temp);


        // Notify subscribers about the movement
        OnMove?.Invoke(temp);
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (this is a Unity built-in method)

}
