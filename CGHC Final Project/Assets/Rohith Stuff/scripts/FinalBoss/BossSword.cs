using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    public GameObject player;
    private Transform boss;
    public GameObject character; // Reference to the character GameObject
    public Vector3 localPositionOffset = Vector3.zero; // Offset to adjust the collider's position when flipped
    public Vector3 localRotationOffset = Vector3.zero;
    private SpriteRenderer characterSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        characterSpriteRenderer = character.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the character's sprite is flipped
        bool isFlipped = characterSpriteRenderer.flipX;

        // Flip the collider's local scale based on the character's sprite flip
        Vector3 newScale = transform.localScale;
        newScale.x = isFlipped ? -1f : 1f;
        transform.localScale = newScale;

        // Apply position and rotation offsets based on the flip state
        transform.localPosition = isFlipped ? new Vector3(-localPositionOffset.x, localPositionOffset.y, localPositionOffset.z) : localPositionOffset;
        transform.localEulerAngles = isFlipped ? new Vector3(-localRotationOffset.x, localRotationOffset.y, localRotationOffset.z) : localRotationOffset;
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
