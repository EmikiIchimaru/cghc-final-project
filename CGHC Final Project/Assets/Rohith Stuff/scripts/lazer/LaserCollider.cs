using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    private Vector3 previousEndPoint; // Store the previous endpoint of the Line Renderer

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Initialize the previous endpoint to the initial endpoint of the Line Renderer
        previousEndPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
    }

    private void Update()
    {
        // Check if the endpoint of the Line Renderer has changed
        Vector3 currentEndPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        if (currentEndPoint != previousEndPoint)
        {
            // Calculate the new size and position of the Box Collider based on the Line Renderer's endpoint change
            UpdateCollider(currentEndPoint);

            // Update the previous endpoint to the current endpoint
            previousEndPoint = currentEndPoint;
        }
    }

    private void UpdateCollider(Vector3 endPoint)
    {
        if (lineRenderer != null && boxCollider != null)
        {
            // Calculate the size of the Box Collider based on the Line Renderer's endpoint change
            Vector2 size = new Vector2(boxCollider.size.x, Mathf.Abs(endPoint.y - transform.position.y));

            // Set the Box Collider's size
            boxCollider.size = size;

            // Calculate the position of the Box Collider as the midpoint between the Line Renderer's endpoints
            Vector3 position = (endPoint + transform.position) / 2f;

            // Set the Box Collider's position
            boxCollider.offset = new Vector2(position.x - transform.position.x, position.y - transform.position.y);
        }
    }
}
