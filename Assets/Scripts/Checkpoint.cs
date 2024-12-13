using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Assign this new sprite in the Inspector for when the player touches the checkpoint
    public Sprite newObjectSprite;
    private SpriteRenderer checkpointSpriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component of the checkpoint object
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer exists
        if (checkpointSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on this checkpoint object!");
        }

        // Check if the new sprite is assigned
        if (newObjectSprite == null)
        {
            Debug.LogWarning("newObjectSprite is not assigned in the Inspector!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Checkpoint triggered by Player");

            // Update the checkpoint position in PlayerManager (if applicable)
            PlayerManager.lastCheckPointPos = transform.position;

            if (checkpointSpriteRenderer != null && newObjectSprite != null)
            {
                Debug.Log("Changing checkpoint sprite");
                checkpointSpriteRenderer.sprite = newObjectSprite;
            }
            else
            {
                Debug.LogWarning("Could not change sprite because either checkpointSpriteRenderer or newObjectSprite is missing.");
            }
        }
    }
}
