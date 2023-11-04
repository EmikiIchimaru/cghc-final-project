using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Transform[] teleportAreas;
    private int attacksCount = 0;
    private bool isCastingSpell = false;
    private bool isAttacking = false;
    public GameObject player;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckPlayerInRange();
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (!isAttacking && !isCastingSpell && attacksCount < 3)
        {
            isAttacking = true;
            attacksCount++;
            PlayMainAttackAnimation();
        }
        else if (!isAttacking && !isCastingSpell && attacksCount >= 3)
        {
            TeleportToRandomArea();
            isCastingSpell = true;
            PlayTeleportAnimation();
        }

        bool isMoving = (Vector2)transform.position != (Vector2)teleportAreas[0].position;
        animator.SetBool("Flight", isMoving && !isAttacking);
    }

    void PlayMainAttackAnimation()
    {
        // Implement your logic to play the main attack animation (sword attack)
        // Example: animator.SetTrigger("MainAttack");
        Debug.Log("Playing Main Attack Animation");
    }

    void TeleportToRandomArea()
    {
        Transform teleportArea = teleportAreas[Random.Range(0, teleportAreas.Length)];
        transform.position = teleportArea.position;
        Debug.Log("Teleporting to Random Area");
    }

    void PlayTeleportAnimation()
    {
        // Implement your logic to play the teleportation animation
        // Example: animator.SetTrigger("Teleport");
        Debug.Log("Playing Teleport Animation");
        PlaySpellCastingAnimation(); // Call spell casting animation after teleportation animation
    }

    void PlaySpellCastingAnimation()
    {
        // Implement your logic to play the spell casting animation
        // Example: animator.SetTrigger("SpellCast");
        Debug.Log("Playing Spell Casting Animation");

        // After spell casting animation, handle spell effect towards player's position
        Vector3 targetPosition = player.transform.position;
        // Implement logic to cast the spell effect towards targetPosition
        Debug.Log("Casting Spell towards Player");
    }

    void CheckPlayerInRange()
    {
        float detectionRange = 5f; // Set your detection range here
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            // Player is in range
            Debug.Log("Player in Range");
        }
        else
        {
            // Player is out of range
            Debug.Log("Player out of Range");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Deal damage to the player
            int damageAmount = 10; // Set your damage amount here
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Dealing Damage to Player: " + damageAmount);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        float attackDistance = 1.5f; // Set your attack distance here
        Gizmos.color = Color.blue; // Set the color of the attack range to blue
        Gizmos.DrawWireSphere(transform.position, attackDistance); // Draw a wire sphere to represent the attack range

        float detectionRange = 5f; // Set your detection range here
        Gizmos.color = Color.red; // Set the color of the detection range to red
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Draw a wire sphere to represent the detection range
    }
}
