using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicPolygonCollider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (polygonCollider == null || spriteRenderer == null)
        {
            Debug.LogError("PolygonCollider2D or SpriteRenderer component not found!");
            return;
        }

        UpdatePolygonCollider();
    }

    void UpdatePolygonCollider()
    {
        // Get the sprite's texture points in local space
        Vector2[] spritePoints = spriteRenderer.sprite.vertices;

        // Convert sprite points to world space
        for (int i = 0; i < spritePoints.Length; i++)
        {
            spritePoints[i] = transform.TransformPoint(spritePoints[i]);
        }

        // Update the Polygon Collider points
        polygonCollider.points = spritePoints;
    }

    void LateUpdate()
    {
        // Call the update method in LateUpdate to ensure that the sprite and collider are in sync after any sprite changes
        UpdatePolygonCollider();
    }
}