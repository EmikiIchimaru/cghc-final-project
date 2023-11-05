using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class WizardEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject Player;
    public GameObject flame1; 
    public GameObject flame2;
    public GameObject detector;
    public float detectionRange = 5f;
    public float attackDistance = 1.5f;
    public float moveSpeed = 2f;
    public int damageAmount = 10;
    public int maxHealth = 100;
    public Slider healthSlider;
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
        currentHealth = maxHealth;
        UpdateHealthUI();
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set the color of the detection range to red
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Draw a wire sphere to represent the detection range
    }


    // Called when the wizard is hit by the player


    void Die()
    {
        // Play death animation or handle death logic here
        animator.SetTrigger("Die");
        Destroy(flame1);
        Destroy(flame2);
        Destroy(detector);
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


}
