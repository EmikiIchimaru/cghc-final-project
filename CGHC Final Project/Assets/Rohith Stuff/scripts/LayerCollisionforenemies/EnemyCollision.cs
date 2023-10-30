using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public LayerMask collisionLayers; // Specify which layers the enemy should collide with

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a layer specified in the collisionLayers mask
        if (collisionLayers == (collisionLayers | (1 << collision.gameObject.layer)))
        {
            // Stop the enemy's movement when colliding with objects on specified layers
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}