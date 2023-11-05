using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for movement input (horizontal axis)
        float moveInput = Input.GetAxis("Horizontal");

        // Set IsRunning parameter based on movement input
        bool isRunning = Mathf.Abs(moveInput) > 0f;
        animator.SetBool("Run", isRunning);

        // Check for attack input (P key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Trigger the Attack animation
            animator.SetTrigger("Attack");
        }

        // Flip the sprite based on movement direction
        if (moveInput > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        }
        else if (moveInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
        }
    }
}
