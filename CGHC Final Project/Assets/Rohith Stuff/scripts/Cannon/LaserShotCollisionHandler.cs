using UnityEngine;


public class LaserShotCollisionHandler : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is the player GameObject.
        if (other.gameObject == player)
        {
            player.GetComponent<Health>().KillPlayer();
        }
    }
}
