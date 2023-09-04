using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float fallDelay;
    private bool isFallDelay = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            
            if (!isFallDelay)
            {
                isFallDelay = true;
                Debug.Log("Test");
                StartCoroutine(FallTimer());
            }
            
        }
    }

    private IEnumerator FallTimer()
    {
        bool hasChangedGravity = false;
        float fallDelayRemaining = 0f;

        while (fallDelayRemaining < 5f)
        {
            fallDelayRemaining += Time.deltaTime;
            Debug.Log("Falling Soon");
            if (!hasChangedGravity && fallDelayRemaining > fallDelay)
            {
                hasChangedGravity = true;
                Debug.Log("Falling!");
                rb.gravityScale = 1.5f;
            }
            yield return null;
        }
        
        gameObject.SetActive(false);
              
    }
}
