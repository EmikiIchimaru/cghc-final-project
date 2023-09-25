using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint; // The position where the player should respawn when this checkpoint is activated.
    public GameObject checkpointActivatedEffect; // Optional: Particle effect or visual indicator when the checkpoint is activated.

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            // Set the player's spawn point to this checkpoint's position.
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetSpawnPoint(spawnPoint.position);
            }

            // Optionally, play a checkpoint activated effect.
            if (checkpointActivatedEffect != null)
            {
                Instantiate(checkpointActivatedEffect, transform.position, Quaternion.identity);
            }

            // Mark the checkpoint as activated to prevent multiple activations.
            activated = true;
        }
    }
}
