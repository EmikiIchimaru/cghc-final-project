using UnityEngine;

public class FlameOn : MonoBehaviour
{
    public ParticleSystem particleSystem; // Reference to the Particle System you want to activate.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the player and the object you want to activate the particle system on.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activate and play the Particle System.
            if (particleSystem != null)
            {
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
            }
        }
    }
}