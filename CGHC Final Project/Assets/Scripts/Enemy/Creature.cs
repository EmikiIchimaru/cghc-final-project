using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5f;
    public float detectionRange = 5f;
    public float attackSpeed = 10f;
    public GameObject player;
    public Animator animator;
    public float attackDistance = 1.5f;
    private bool playerInRange = false;
    private Transform currentWaypoint;
    private bool isAttacking = false;
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider;

    void Start()
    {
        // Set the initial waypoint
        currentWaypoint = GetRandomWaypoint();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        UpdateHealthUI();
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
        // Implement your attack logic here
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play hit animation or effect if needed
            animator.SetTrigger("Takehit");
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // Play death animation
        animator.SetTrigger("Die");

        // Wait for 2 seconds before destroying the creature
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
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
    private void OnDrawGizmos()
    {
        // Draw detection range (detectionRange variable)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range (attackDistance variable)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            int damageAmount = Random.Range(10, 21);
            // Deal damage to the wizard
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                Debug.Log("colliding");
                playerHealth.TakeDamage(damageAmount);
            }
            if (playerHealth == null)
            {
                Debug.Log("No health script");
            }
        }
    }
}
