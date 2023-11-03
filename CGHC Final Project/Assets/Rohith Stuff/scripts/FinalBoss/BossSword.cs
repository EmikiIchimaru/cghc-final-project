using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            int damageAmount = Random.Range(10, 21);
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
