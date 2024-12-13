using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3;
    public float jumpForce = 5f;           // Strength of the jump
    public float jumpCooldown = 2f;        // Time between jumps

    private float startingX;
    private bool isGrounded = false;       // Check if the frog is on the ground
    private float lastJumpTime = 0;        // Time of the last jump
    private int dir = -1;                  // Initial direction

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;                // Reference to Rigidbody2D
    private Animator animator;             // Reference to Animator component
    public Transform groundCheck;          // Transform for ground check position
    public LayerMask groundLayer;          // Layer for the ground

    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();    // Reference to Rigidbody2D
        animator = GetComponent<Animator>();  // Reference to Animator
    }

    // FixedUpdate is called at a fixed time interval (best for physics-related updates)
    void FixedUpdate()
    {
        // For moving the object in the X direction
        transform.Translate(Vector2.right * speed * Time.deltaTime * dir);

        // Check if the object has reached the edge of its range
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;  // Reverse direction

            // Flip the sprite based on the movement direction
            spriteRenderer.flipX = (dir == 1);  // Flip the sprite if moving right
        }

        // Check if the frog is grounded before jumping again
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Set the "IsGrounded" parameter in the animator (for idle animation)
        animator.SetBool("IsGrounded", isGrounded);

        // Trigger the jump after the cooldown
        if (isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {
            Jump();
        }
    }

    // Function to make the frog jump
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Apply upward force for jump
        lastJumpTime = Time.time;  // Update the last jump time

        // Trigger the jump animation
        animator.SetTrigger("Jump");
    }

    void OnTriggerEnter() 
    {
    }
}