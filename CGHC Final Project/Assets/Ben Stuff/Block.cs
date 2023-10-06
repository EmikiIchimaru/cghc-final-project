using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum ButtonFunc { None, Fall, Toggle, Summon }
    public ButtonFunc buttonFunc;
    public bool isStartActive;
    [SerializeField] private bool instantFall;
    [SerializeField] private bool fallOnContact;
    [SerializeField] private float fallDelay;
   
    [SerializeField] private float summonDuration;
    [SerializeField] private Vector3 summonOffset = new Vector3(0f,-10f,0f);
    [SerializeField] private Vector3 moveOffset = new Vector3(0f,10f,0f);

    private bool isFallDelay = false;
    private bool isSummoning = false;
    private bool isToggle = false;
    private Rigidbody2D rb;

    private Vector3 basePosition;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        basePosition = transform.position;
        ResetBlock();
    }

    void Start()
    { 
        if (instantFall) { StartFall(); }
    }

    public void ResetBlock()
    {
        StopCoroutine(FallTimer());
        StopCoroutine(SummonCoroutine());
        StopCoroutine(ToggleCoroutine());

        if (!isStartActive)
        {
            gameObject.SetActive(false);
        }

        rb.gravityScale = 0f;
        transform.position = basePosition;
        isToggle = false;

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

    public void ToggleDoor()
    {
        isToggle = !isToggle;
        StopCoroutine(ToggleCoroutine());
        StartCoroutine(ToggleCoroutine());
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
                //Debug.Log("Falling!");
                rb.gravityScale = 3f;
            }
            yield return null;
        }
        
        isFallDelay = false;
        gameObject.SetActive(false);
              
    }

    private IEnumerator SummonCoroutine()
    {
        float summonElapsed = 0f;
        transform.position = basePosition + summonOffset;
        
        while (summonElapsed <= summonDuration)
        {
            summonElapsed += Time.deltaTime;
            transform.position = transform.position - summonOffset/summonDuration * Time.deltaTime;

            yield return null;

        }

        transform.position = basePosition;
        isSummoning = false;
    }

    private IEnumerator ToggleCoroutine()
    {
        Vector3 targetPosition = (isToggle) ? basePosition + moveOffset : basePosition;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        //isSummoning = false;
    }

}
