using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToActivate1; // First game object to activate
    public GameObject objectToActivate2; // Second game object to activate
    public float detectionRadius = 5f; // Detection range radius

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player entered the detection range!");

            // Activate specified game objects
            if (objectToActivate1 != null)
            {
                objectToActivate1.SetActive(true);
            }
            if (objectToActivate2 != null)
            {
                objectToActivate2.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player left the detection range!");

            // Deactivate specified game objects
            if (objectToActivate1 != null)
            {
                objectToActivate1.SetActive(false);
            }
            if (objectToActivate2 != null)
            {
                objectToActivate2.SetActive(false);
            }
        }
    }

    // Optionally, you can visualize the detection range in the Scene view using Gizmos.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
