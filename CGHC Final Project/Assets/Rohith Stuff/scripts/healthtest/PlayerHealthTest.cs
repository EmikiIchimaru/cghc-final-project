using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth; // Serialized field to show in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce the player's health
        currentHealth -= damageAmount;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Get the Health component from the same GameObject
        Health healthComponent = GetComponent<Health>();

        // Check if the Health component is not null
        if (healthComponent != null)
        {
            // Call the KillPlayer method from the Health component
            healthComponent.KillPlayer();
            currentHealth = maxHealth;
        }
        else
        {
            // Handle the case where the Health component is not found
            Debug.LogError("Health component not found!");
        }
    }
}
