using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public GameObject objectToFlipCollider; // Reference to the game object whose collider you want to flip

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for movement input (horizontal axis)
        float moveInput = Input.GetAxis("Horizontal");

        // Set IsRunning parameter based on movement input
        bool isRunning = Mathf.Abs(moveInput) > 0f;
        animator.SetBool("Run", isRunning);

        // Check for attack input (P key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Trigger the Attack animation
            animator.SetTrigger("Attack");
        }

        // Flip the sprite and collider based on movement direction
        FlipSprite(moveInput);
    }

    void FlipSprite(float moveInput)
    {
        if (moveInput > 0f)
        {
            // Player is moving right, flip the sprite and the other object's collider
            transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
            FlipCollider(false); // Pass 'false' to indicate not flipping the other object's collider
        }
        else if (moveInput < 0f)
        {
            // Player is moving left, flip the sprite and the other object's collider
            transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
            FlipCollider(true); // Pass 'true' to indicate flipping the other object's collider
        }
        // Handle other cases (e.g., moveInput == 0) as needed
    }

    void FlipCollider(bool flipX)
    {
        // Check if the objectToFlipCollider is assigned
        if (objectToFlipCollider != null)
        {
            Collider2D colliderToFlip = objectToFlipCollider.GetComponent<Collider2D>();
            if (colliderToFlip != null)
            {
                // Flip the collider of the other object based on the flipX parameter
                if (flipX)
                {
                    // Flip the collider by changing its size and offset
                    colliderToFlip.offset = new Vector2(-colliderToFlip.offset.x, colliderToFlip.offset.y);
                }
                else
                {
                    // Reset the collider to its original size and offset
                    colliderToFlip.offset = new Vector2(Mathf.Abs(colliderToFlip.offset.x), colliderToFlip.offset.y);
                }
            }
        }
    }
}
