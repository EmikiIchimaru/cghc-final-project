using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WizardEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject Player;
    public float detectionRange = 5f;
    public float attackDistance = 1.5f;
    public float moveSpeed = 2f;
    public int damageAmount = 10;
    public int maxHealth = 50;

    private int currentHealth;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("Not Found");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within the detection range but outside the attack distance
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackDistance)
        {
            // Move towards the player
            Vector2 moveDirection = (player.position - transform.position).normalized;

            // Flip the sprite based on movement direction
            FlipSprite(moveDirection.x);

            // Move the wizard towards the player
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Play move animation
            animator.SetTrigger("Move");
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Wizard is near the player, stop moving and attack
            // Play attack animation
            animator.SetTrigger("Attack");
            // You can add logic to deal damage to the player here
        }
        else
        {
            // Play idle animation
            animator.SetTrigger("Idle");
        }
    }


    // Called when the wizard is hit by the player
    public void TakeDamage(int damageAmount)
    {
        // Reduce wizard's health
        currentHealth -= damageAmount;

        // Play hit animation
        animator.SetTrigger("Hit");

        // Check if the wizard is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation or handle death logic here
        animator.SetTrigger("Die");

        // Destroy the wizard after the death animation
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    // Called when the fire attack collides with something
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Deal damage to the wizard
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                Debug.Log("colliding");
                playerHealth.TakeDamage(damageAmount);
            }
            if (playerHealth = null)
            {
                Debug.Log("No health script");
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Deal damage to the player continuously while staying in the trigger area
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Colliding");
            }
        }
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
