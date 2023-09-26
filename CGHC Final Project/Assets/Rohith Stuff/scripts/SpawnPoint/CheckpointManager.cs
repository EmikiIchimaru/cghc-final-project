using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the spawn point in the LevelManager to this checkpoint's position
            levelManager.UpdateSpawnPoint(transform.position);
        }
    }
}