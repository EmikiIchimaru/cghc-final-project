using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : LevelComponent
{
    public int damageAmount = 100;
    public GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }
    public override void Damage(PlayerMotor player)
    {
        base.Damage(player);
        Debug.Log("Blade");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Deal damage to the wizard
            PlayerHealthTest playerHealth = other.GetComponent<PlayerHealthTest>();
            if (playerHealth != null)
            {
                Debug.Log("colliding");
                playerHealth.TakeDamage(damageAmount);
            }
            if (playerHealth = null)
            {
                Debug.Log("No health script");
            }
        }
    }
}
