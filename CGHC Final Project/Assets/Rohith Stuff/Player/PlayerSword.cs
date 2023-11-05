using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public int damageAmount = 30;
    public GameObject creature;
    public GameObject bringerofdeath;
    // Start is called before the first frame update
    void Start()
    {
        creature = GameObject.FindGameObjectWithTag("Creature");
        bringerofdeath = GameObject.FindGameObjectWithTag("BOD");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the sword collides with the creature
        CreatureController creatureController = other.GetComponent<CreatureController>();
        BringerofDeathController bringerofdeathController = other.GetComponent<BringerofDeathController>();

        if (creatureController != null)
        {
            // Deal damage to the creature
            creatureController.TakeDamage(damageAmount);
        }

        if (bringerofdeathController != null)
        {
            // Deal damage to the Bringer of Death
            bringerofdeathController.TakeDamage(damageAmount);
        }
    }
}
