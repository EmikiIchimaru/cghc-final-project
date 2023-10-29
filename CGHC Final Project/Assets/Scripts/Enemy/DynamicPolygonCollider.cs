using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicPolygonCollider : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Call the function to update the collider initially and whenever the sprite changes
        UpdatePolygonCollider();
    }

    void UpdatePolygonCollider()
    {
        Vector2[] spriteVertices = spriteRenderer.sprite.vertices;

        // Transform local sprite vertices to world space
        for (int i = 0; i < spriteVertices.Length; i++)
        {
            spriteVertices[i] = transform.TransformPoint(spriteVertices[i]);
        }

        polygonCollider.SetPath(0, spriteVertices);
    }

    void LateUpdate()
    {
        // Check if the sprite has changed (width, height, etc.) and update the collider shape
        if (spriteRenderer.sprite != null)
        {
            UpdatePolygonCollider();
        }
    }
}
