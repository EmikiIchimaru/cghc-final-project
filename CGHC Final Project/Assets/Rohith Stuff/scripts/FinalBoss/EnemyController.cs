using UnityEngine;

public class BossController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private bool isIdle = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (distanceToPlayer <= attackRange)
            {
                // Player is inside attack range, attack
                isIdle = false;
                int randomAttack = Random.Range(1, 3); // Randomly select Attack1 or Attack2
                animator.SetInteger("AttackIndex", randomAttack);
            }
            else
            {
                // Player is inside detection range but outside attack range, move towards the player
                isIdle = false;
                animator.SetInteger("AttackIndex", 0); // Set AttackIndex to 0 for move animation
                transform.Translate(direction * moveSpeed * Time.deltaTime);

                // Flip the sprite to face the player's direction
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false; // Player is on the right side of the boss
                }
                else if (direction.x < 0)
                {
                    spriteRenderer.flipX = true; // Player is on the left side of the boss
                }
            }
        }
        else
        {
            // Player is outside detection range, go back to idle state
            isIdle = true;
            animator.SetInteger("AttackIndex", 0); // Set AttackIndex to 0 for idle animation
        }

        animator.SetBool("IsIdle", isIdle);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
