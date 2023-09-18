using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public float moveSpeed = 2.0f;
    public float stopTime = .1f;
    public float moveDistance = 5.0f; 

    private Rigidbody2D rb;
    private Vector2 originalPosition;
    private bool movingRight = true;
    private float traveledDistance = 0.0f;
    private float stopTimer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
        }
        else
        {
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                traveledDistance = transform.position.x - originalPosition.x;

                if (traveledDistance >= moveDistance)
                {
                    movingRight = false;
                    stopTimer = stopTime;
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                traveledDistance = originalPosition.x - transform.position.x;

                if (traveledDistance >= moveDistance)
                {
                    movingRight = true;
                    stopTimer = stopTime;
                }
            }
        }
    }
}

