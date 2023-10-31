using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    private Animator animator;
    private int attackCounter = 0;
    private bool isJumping = false;
    public float moveSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (player != null)
        {
            // Player detected
            // Follow the player (implement movement logic here)
            Vector2 playerPosition = player.transform.position;

            // Flip the enemy to face the player
            if (playerPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);

            // Implement random attack logic
            if (attackCounter < 4)
            {
                // Play attack 1 animation
                animator.SetTrigger("Attack1");
            }
            else
            {
                // Play attack 1 animation first and then attack 2
                animator.SetTrigger("Attack1");
                animator.SetTrigger("Attack2");
                attackCounter = -1; // Reset the counter
            }
            attackCounter++;
        }
        else
        {
            // Player not detected
            // Stop running
            animator.SetBool("isRunning", false);
        }
    }

    void OnDrawGizmos()
    {
        // Draw detection radius in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
