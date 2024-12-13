using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  // Make sure this is included

public class PlayerMovement : MonoBehaviour
{
    PlyerControls controls;             // Reference to the generated PlayerControls class
    float direction = 0;                // Direction of the player
    public float speed = 400;
    public float jumpForce = 5;         // Jump force that will be applied
    int numberOfJumps = 0;              //kitni jumps maar skta hai vo double jump mai

    public Rigidbody2D playerRB;        // Reference to Rigidbody2D component
    public Animator animator;           // Reference to Animator component
    public bool isFacingRight = true;          // Which direction player is facing(by defult its right)
    bool isGrounded;                    // Chexking whether player is touch
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Awake()
    {
        // Assign the Rigidbody if not done in the Inspector
        if (playerRB == null)
        {
            playerRB = GetComponent<Rigidbody2D>();
        }

        // Initialize PlayerControls
        controls = new PlyerControls();
        controls.Enable();

        // Subscribe to Move action
        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx =>
        {
            Jump();
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
  
        // Update the player's velocity based on input direction
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(direction));

        //Changing player direction
        if(isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            numberOfJumps++;
            AudioManager.instance.Play("Jump");
        }
        else
        {
            if(numberOfJumps == 1)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                numberOfJumps++;
                AudioManager.instance.Play("Jump");
            }
        }
    }

    // Remember to disable controls when the object is destroyed or disabled
    private void OnDisable()
    {
        controls.Disable();
    }
}
