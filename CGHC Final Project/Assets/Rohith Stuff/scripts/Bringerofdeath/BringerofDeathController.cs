using UnityEngine;

public class BringerofDeathController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isIdle = true;
    private bool isUsingSecondAttack = false; // Added variable to track second attack state
    public float spellCastDelay = 1f; // Delay before dealing damage after spell animation

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (distanceToPlayer <= attackRange)
            {
                isIdle = false;
                int randomAttack = Random.Range(1, 3); // Randomly select Attack1 or Attack2
                animator.SetInteger("AttackIndex", randomAttack);

                // For the second boss, set isUsingSecondAttack to true when using the second attack
                if (randomAttack == 2)
                {
                    isUsingSecondAttack = true;
                    CastAnimationEnd();
                }
            }
            else
            {
                isIdle = false;
                animator.SetInteger("AttackIndex", 0);
                transform.Translate(direction * moveSpeed * Time.deltaTime);

                if (direction.x < 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (direction.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
        else
        {
            isIdle = true;
            animator.SetInteger("AttackIndex", 0);
        }

        animator.SetBool("IsIdle", isIdle);
    }

    // Method to be called when the cast animation ends (via animation events)
    public void CastAnimationEnd()
    {
        if (isUsingSecondAttack)
        {
            // Find the player's position
            Vector3 targetPosition = player.position;

            // Implement spell animation logic here targeting the player's position
            Debug.Log("Casting Spell Animation towards Player's Position");

            // Set the "IsSpell" parameter to true to trigger the transition to the "Spell" state
            SetSpellAnimationTrue();

            // Add damage logic here (e.g., Instantiate a spell prefab at targetPosition)
            // Instantiate(spellPrefab, targetPosition, Quaternion.identity);

            // Reset the second attack flag after spell animation ends (or wherever appropriate)
            isUsingSecondAttack = false;

            // Set the "IsSpell" parameter to false after using the spell animation
            Invoke("SetSpellAnimationFalse", spellCastDelay); // Delay setting the bool to false if needed
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void SetSpellAnimationTrue()
    {
        animator.SetBool("IsSpell", true);
    }

    // Method to set the "IsSpell" parameter to false in the animator
    private void SetSpellAnimationFalse()
    {
        animator.SetBool("IsSpell", false);
    }
}