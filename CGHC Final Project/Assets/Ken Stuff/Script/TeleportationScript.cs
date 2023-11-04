using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    public Transform teleportDestination; // The destination where the player will be teleported.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player (or any other object with a Collider2D) entered the teleportation area.
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the destination.
            other.transform.position = teleportDestination.position;
        }
    }
}
