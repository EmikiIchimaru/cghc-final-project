using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public int damageAmount = 30;
    public GameObject creature;
    // Start is called before the first frame update
    void Start()
    {
        creature = GameObject.FindGameObjectWithTag("Creature");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the sword collides with the creature
        CreatureController creature = other.GetComponent<CreatureController>();
        if (creature != null)
        {
            // Deal damage to the creature
            creature.TakeDamage(damageAmount);
        }
    }
}
