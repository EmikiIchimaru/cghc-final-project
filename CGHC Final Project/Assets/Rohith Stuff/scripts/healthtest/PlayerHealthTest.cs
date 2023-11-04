using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthTest : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth; // Serialized field to show in the Inspector
    public Image healthBarImage; // Reference to the Image component of your health bar

    void Start()
    {
        GameObject healthBarUI = GameObject.FindGameObjectWithTag("HealthBarUI");
        if (healthBarUI != null)
        {
            healthBarImage = healthBarUI.GetComponent<Image>();
            if (healthBarImage == null)
            {
                Debug.LogError("Image component not found on the GameObject with the HealthBarUI tag.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag HealthBarUI not found in the scene.");
        }
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce the player's health
        currentHealth -= damageAmount;
        UpdateHealthBar();

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    void UpdateHealthBar()
    {
        // Calculate the fill amount based on current health and max health
        float fillAmount = (float)currentHealth / maxHealth;

        // Set the fill amount of the health bar image
        healthBarImage.fillAmount = fillAmount;
    }

    void Die()
    {
        float delay = 2f; // Delay in seconds

        // Invoke the DelayedResetAndHealthBarUpdate method after the specified delay
        Invoke("DelayedResetAndHealthBarUpdate", delay);
        // Get the Health component from the same GameObject
        Health healthComponent = GetComponent<Health>();

        // Check if the Health component is not null
        if (healthComponent != null)
        {
            // Call the KillPlayer method from the Health component
            healthComponent.KillPlayer();
        }
        else
        {
            // Handle the case where the Health component is not found
            Debug.LogError("Health component not found!");
        }
    }

    void DelayedResetAndHealthBarUpdate()
    {
        // Reset player's health to max health
        currentHealth = maxHealth;
        Debug.Log("Player's health reset to max: " + currentHealth);

        // Update the health bar to full after the delay
        UpdateHealthBar();
        Debug.Log("Health bar updated!");
    }
}
