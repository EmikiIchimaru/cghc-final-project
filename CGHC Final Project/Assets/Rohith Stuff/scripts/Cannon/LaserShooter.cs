using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float detectionRange = 5f; // Detection range to start shooting
    public float shootInterval = 0.5f; // Time interval between each laser shot
    public float laserSpeed = 10f; // Speed of the laser
    public float laserDuration = 5f; // Time duration for which the laser stays visible
    public GameObject laserPrefab; // Prefab for the laser GameObject representing the laser beam

    private Transform player; // Reference to the player's transform
    private float timeSinceLastShot;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player GameObject by tag
    }

    void Update()
    {
        // Check if the player is within the detection range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && Time.time - timeSinceLastShot > shootInterval)
        {
            // Rotate the shooter to face the player
            Vector3 targetDirection = player.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            // Shoot the laser
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        // Instantiate the laser prefab
        GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        Destroy(laser, laserDuration); // Destroy the laser after laserDuration seconds

        // Move the laser towards the player's position
        Rigidbody2D laserRigidbody = laser.GetComponent<Rigidbody2D>();
        laserRigidbody.velocity = laser.transform.up * laserSpeed;
        timeSinceLastShot = Time.time;
    }
}