using UnityEngine;

public class ParticleSystemOnPlayerEnter : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject.
    public ParticleSystem particleSystem; // Reference to the Particle System to activate.

    private void Start()
    {
        // Find the player GameObject by tag.
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is the player GameObject.
        if (other.gameObject == player)
        {
            Debug.Log("Player entered trigger zone.");

            // Check if the Particle System component exists.
            if (particleSystem != null)
            {
                // Activate and play the Particle System.
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
            }
            else
            {
                Debug.LogError("Particle System is not assigned.");
            }
        }
    }
}