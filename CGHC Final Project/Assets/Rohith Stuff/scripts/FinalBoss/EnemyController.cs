using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float detectionRange = 10f;   // Detection range within which the player is detected
    public float attackRange = 3f;       // Attack range within which the player can be attacked
    public float moveSpeed = 3f;         // Boss movement speed
    public Transform player;              // Reference to the player's transform
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the distance between the boss and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // If the player is within detection range, follow the player
        if (distanceToPlayer <= detectionRange)
        {
            // Calculate the direction from the boss to the player
            Vector2 moveDirection = (player.position - transform.position).normalized;

            // Flip the sprite based on movement direction
            FlipSprite(moveDirection.x);

            // Move the wizard towards the player
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Move the boss using Rigidbody2D to ensure smooth movement
            animator.SetBool("IDLE", false);
            animator.SetBool("Run", true);
        }
        else if (detectionRange <= attackRange)
        {
            // If the player is within attack range, perform attack logic here

            animator.SetBool("Run", false);
            int randomAttack = Random.Range(1, 3);

            // Set trigger based on the random number
            if (randomAttack == 1)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                animator.SetTrigger("Attack2");
            }
            
        }
        else
        {
            // If the player is outside the detection range, stop moving
            animator.SetBool("Run", false);
            animator.SetBool("IDLE", true);
            
            rb.velocity = Vector2.zero;
        }
    }

    // Draw detection and attack ranges using Gizmos for visualization purposes
    private void OnDrawGizmos()
    {
        // Draw detection range as a green circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range as a red circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void FlipSprite(float horizontalMove)
    {
        if (horizontalMove > 0f) // Moving right
        {
            spriteRenderer.flipX = false; // Sprite faces right (default orientation)
        }
        else if (horizontalMove < 0f) // Moving left
        {
            spriteRenderer.flipX = true; // Sprite faces left (flipped horizontally)
        }
        // No flip if horizontalMove is 0 (not moving horizontally)
    }
}
