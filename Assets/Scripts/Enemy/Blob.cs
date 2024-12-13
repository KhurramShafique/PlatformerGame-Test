using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3;

    float startingY;
    float startingX;
    int dir = -1;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        startingX = transform.position.x;  // Initialize startingX
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //For moving object in Y direction
        /*transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
        if(transform.position.y < startingY || transform.position.y > startingY + range)
        {
            dir *= -1;
        }*/

        // For moving object in X direction
        transform.Translate(Vector2.right * speed * Time.deltaTime * dir);

        // Check if the object has reached the edge of its range
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;  // Reverse direction

            // Flip the sprite based on direction
            // spriteRenderer.flipX = dir < 0;  // Flip when moving left (dir < 0)
            if (dir == 1)
            {
                spriteRenderer.flipX = true;
            }
            if (dir == -1)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}