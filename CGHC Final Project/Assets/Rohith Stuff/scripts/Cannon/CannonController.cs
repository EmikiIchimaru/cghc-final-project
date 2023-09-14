using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float radius = 5.0f; // Radius within which the cannon will start facing the player
    public Transform player;    // Reference to the player's transform

    private void Update()
    {
        // Check if the player is within the cannon's radius
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the distance between the cannon and the player
            float distanceToPlayer = directionToPlayer.magnitude;

            // If the player is within the radius, face the player
            if (distanceToPlayer <= radius)
            {
                // Calculate the angle between the cannon's forward direction and the direction to the player
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // Rotate the cannon towards the player's direction
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
