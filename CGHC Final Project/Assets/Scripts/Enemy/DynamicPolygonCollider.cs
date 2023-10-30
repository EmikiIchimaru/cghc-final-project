using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicEnemyCollider2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        if (spriteRenderer != null && polygonCollider != null)
        {
            UpdateCollider();
        }
        else
        {
            Debug.LogError("SpriteRenderer or PolygonCollider2D component is missing!");
        }
    }

    void Update()
    {
        // Check for animation state changes or sprite changes here
        // For example, if animation state changes:
        bool isAttacking = GetComponent<Animator>().GetBool("Attack");
        bool isdeath = GetComponent<Animator>().GetBool("Die");
        bool ismove = GetComponent<Animator>().GetBool("Move");
        bool istakehit = GetComponent<Animator>().GetBool("Hit");
        bool isidle = GetComponent<Animator>().GetBool("Idle");


        // Update collider based on animation state
        if (isAttacking)
        {
            // Call UpdateCollider() when attacking animation is active
            UpdateCollider();
        }
        if (isdeath)
        {
            // Call UpdateCollider() when attacking animation is active
            UpdateCollider();
        }
        if (ismove)
        {
            // Call UpdateCollider() when attacking animation is active
            UpdateCollider();
        }
        if (istakehit)
        {
            // Call UpdateCollider() when attacking animation is active
            UpdateCollider();
        }
        if (isidle)
        {
            // Call UpdateCollider() when attacking animation is active
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        // Get sprite boundary vertices in local space
        Vector2[] spriteVertices = spriteRenderer.sprite.vertices;

        // Check if the sprite is flipped (looking to the left)
        if (spriteRenderer.flipX)
        {
            // Mirror the collider points along the Y-axis
            for (int i = 0; i < spriteVertices.Length; i++)
            {
                spriteVertices[i] = new Vector2(-spriteVertices[i].x, spriteVertices[i].y);
            }
        }

        // Assign sprite boundary vertices to collider points
        polygonCollider.SetPath(0, spriteVertices);
    }
}
