using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5f;
    public float detectionRange = 5f;
    public float attackSpeed = 10f;
    public GameObject player;
    public Animator animator;
    public int damageAmount = 10;
    public float attackDistance = 1.5f;
    private bool playerInRange = false;
    private Transform currentWaypoint;
    private bool isAttacking = false;


    void Start()
    {
        // Set the initial waypoint
        currentWaypoint = GetRandomWaypoint();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        CheckPlayerInRange();
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (playerInRange)
        {
            MoveTowardsPlayer();
            if (distanceToPlayer <= attackDistance && !isAttacking)
            {
                // Start attacking
                isAttacking = true;
                AttackPlayer();
            }
            else if (distanceToPlayer > attackDistance)
            {
                // Stop attacking if player is out of attack range
                isAttacking = false;
            }

            // Determine the horizontal movement direction
            float horizontalMove = player.transform.position.x - transform.position.x;
            FlipSprite(horizontalMove);
        }
        else
        {
            MoveTowardsWaypoints();
            // Stop attacking when not in range
            isAttacking = false;

            // Determine the horizontal movement direction
            float horizontalMove = currentWaypoint.position.x - transform.position.x;
            FlipSprite(horizontalMove);
        }

        bool isMoving = (Vector2)transform.position != (Vector2)currentWaypoint.position;
        animator.SetBool("Flight", isMoving && !isAttacking);
    }

    void MoveTowardsWaypoints()
    {
        // Check if the creature has reached the current waypoint
        if ((Vector2)transform.position == (Vector2)currentWaypoint.position)
        {
            // Get a new random waypoint that is not the current one
            Transform newWaypoint = GetRandomWaypoint();
            while (newWaypoint == currentWaypoint)
            {
                newWaypoint = GetRandomWaypoint();
            }
            currentWaypoint = newWaypoint;
        }

        // Move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
    }

    Transform GetRandomWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Length)];
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attackSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Trigger random attack animation
        int randomAttack = Random.Range(0, 2);
        animator.SetTrigger("Attack" + (randomAttack + 1));

        // Wait for the attack animation to finish before allowing movement again

    }

 

    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Deal damage to the wizard
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue; // Set the color of the attack range to blue
        Gizmos.DrawWireSphere(transform.position, attackDistance); // Draw a wire sphere to represent the attack range

        Gizmos.color = Color.red; // Set the color of the detection range to red
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void TakeHit()
    {
        animator.SetTrigger("Takehit");
    }


    public void Die()
    {
        animator.SetTrigger("Die");
    }

    void FlipSprite(float horizontalMove)
    {
        if (horizontalMove > 0f) // Moving right
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalMove < 0f) // Moving left
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}