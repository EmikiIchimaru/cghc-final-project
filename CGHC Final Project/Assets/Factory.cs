using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private float loopDuration;
    [SerializeField] private int itemAmount;
    [SerializeField] private float itemInterval;
    [SerializeField] private float itemDuration = 3f;
    // Start is called before the first frame update

    private float timeCounter = 0f;
    private int itemCounter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= loopDuration) 
        { 
            timeCounter -= loopDuration; 
            itemCounter = 0;
        }
        if (itemCounter >= itemAmount) { return; }
        if (timeCounter >= itemInterval * itemCounter)
        {
            itemCounter++;
            GameObject go = Instantiate(item, transform.position, Quaternion.identity);
            Destroy(go, itemDuration);
            //Debug.Log(itemCounter.ToString());
        }
    }
}
