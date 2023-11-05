using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    public Slider healthSlider;
    private int currentHealth;
    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private bool isIdle = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        currentHealth = maxHealth;
        UpdateHealthUI();
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
                int randomAttack = UnityEngine.Random.Range(1, 3); // Randomly select Attack1 or Attack2
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
        // Play death animation or handle death logic here
        animator.SetTrigger("Die");

        // Destroy the wizard after the death animation
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
