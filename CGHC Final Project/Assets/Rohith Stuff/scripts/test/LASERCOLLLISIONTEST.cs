using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASERCOLLLISIONTEST : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject.

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
            Debug.Log("Colliding");

        }
    }
}