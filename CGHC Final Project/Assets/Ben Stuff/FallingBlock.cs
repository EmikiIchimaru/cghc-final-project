using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public bool isStartActive;
    [SerializeField] private bool fallOnContact;
    [SerializeField] private float fallDelay;
    [SerializeField] private float summonDuration;
    [SerializeField] private Vector3 summonOffset = new Vector3(0f,-10f,0f);

    private bool isFallDelay = false;
    private bool isSummoning = false;
    private Rigidbody2D rb;

    private Vector3 basePosition;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!isStartActive)
        {
            gameObject.SetActive(false);
        }
        
        basePosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fallOnContact) { return; }
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (!pc) { return; }
        if (!pc.Conditions.IsCollidingBelow) { return; }
           
        StartFall();
    
    }

    public void Summon()
    {
        if (!isSummoning)
        {
            isSummoning = true;
            //Debug.Log("Test");
            gameObject.SetActive(true);
            StartCoroutine(SummonCoroutine());
        }
    }

    public void StartFall()
    {
        if (!isFallDelay)
        {
            isFallDelay = true;
            //Debug.Log("Test");
            StartCoroutine(FallTimer());
        }
    }

    private IEnumerator FallTimer()
    {
        bool hasChangedGravity = false;
        float fallDelayRemaining = 0f;

        while (fallDelayRemaining < 5f)
        {
            fallDelayRemaining += Time.deltaTime;
            //Debug.Log("Falling Soon");
            if (!hasChangedGravity && fallDelayRemaining > fallDelay)
            {
                hasChangedGravity = true;
                Debug.Log("Falling!");
                rb.gravityScale = 3f;
            }
            yield return null;
        }
        
        gameObject.SetActive(false);
              
    }

    private IEnumerator SummonCoroutine()
    {
        float summonElapsed = 0f;
        transform.position = transform.position + summonOffset;
        
        while (summonElapsed <= summonDuration)
        {
            summonElapsed += Time.deltaTime;
            transform.position = transform.position - summonOffset/summonDuration * Time.deltaTime;

            yield return null;

        }

        transform.position = basePosition;
    }
}
