using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public int damageAmount = 30;
    public GameObject creature;
    public GameObject bringerofdeath;
    public GameObject Wizard;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        creature = GameObject.FindGameObjectWithTag("Creature");
        bringerofdeath = GameObject.FindGameObjectWithTag("BOD");
        Wizard = GameObject.FindGameObjectWithTag("Wizard");
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
        WizardEnemy wizardenemy = other.GetComponent<WizardEnemy>();
        EnemyController enemycontroller = other.GetComponent<EnemyController>();
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
        if (wizardenemy != null)
        {
            // Deal damage to the Bringer of Death
            wizardenemy.TakeDamage(damageAmount);
        }
        if (enemycontroller != null)
        {
            // Deal damage to the Bringer of Death
            enemycontroller.TakeDamage(damageAmount);
        }
    }
}