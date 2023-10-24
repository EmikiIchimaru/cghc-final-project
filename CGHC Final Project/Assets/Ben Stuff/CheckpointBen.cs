using UnityEngine;

public class CheckpointBen : MonoBehaviour
{
    [SerializeField] private Area newArea;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject checkpointActivatedEffect; // Optional: Particle effect or visual indicator when the checkpoint is activated.

    public bool activated = false;
	private Transform spawnPoint; // The position where the player should respawn when this checkpoint is activated.
	
    
    void Awake()
    {
        spawnPoint = gameObject.transform;
        //levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            CollideActions();
        }
    }

    private void CollideActions()
    {
        Debug.Log("checkpoint!");

        levelManager.levelStartPoint = spawnPoint;	

        if (checkpointActivatedEffect != null)
        {
            GameObject vfx = Instantiate(checkpointActivatedEffect, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (newArea != null)
        {
            levelManager.currentArea = newArea;
        }

        activated = true;
    }
}
