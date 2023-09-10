using UnityEngine;
using System.Collections;

public class AnimatedLaserBeam : MonoBehaviour
{
    public Transform laserEnd; // Reference to the end point of the laser beam
    public float duration = 2.0f; // Duration of the laser animation in seconds
    public float pauseDuration = 1.0f; // Duration to pause the laser beam in seconds

    private LineRenderer lineRenderer;
    private Vector3 startPosition;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startPosition = transform.position;
        StartCoroutine(AnimateLaser());
    }

    IEnumerator AnimateLaser()
    {
        while (true)
        {
            // Animate the laser beam
            float timer = 0.0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / duration);
                Vector3 newPosition = Vector3.Lerp(startPosition, laserEnd.position, t);
                lineRenderer.SetPosition(1, newPosition);
                yield return null;
            }

            // Pause the laser beam
            yield return new WaitForSeconds(pauseDuration);

            // Reset the LineRenderer's position
            lineRenderer.SetPosition(1, startPosition);

            // Optionally, you can add more logic here or break out of the loop to stop the sequence.
        }
    }
}