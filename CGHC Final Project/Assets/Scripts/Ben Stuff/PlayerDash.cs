using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float fixedDashX;
    
    [SerializeField] private float dashCooldown;

    [SerializeField] private Camera mainCamera;
    // return if player can dash
    public bool canDash = true;
    public bool isDashing { get; private set; }
    
    private Vector2 dashDirection;

    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.IsCollidingBelow && _playerController.Force.y == 0f)
        {
            canDash = true;
            SetDashColour(true);
            //_playerController.Conditions.IsJumping = false;
        }
    }

    protected override void GetInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (!canDash) { return; }

        canDash = false;
        SetDashColour(false);

        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = -fixedDashX * mainCamera.transform.position.z;

        Vector3 worldCursorPos = mainCamera.ScreenToWorldPoint(cursorPosition);
        dashDirection = (worldCursorPos - transform.position).normalized;
        Debug.Log((worldCursorPos - transform.position).ToString());
        Debug.Log(dashDirection.ToString());

        StartCoroutine(DashDuration());
        //StartCoroutine(DashCooldown());
        
        //_playerController.Conditions.IsJumping = true;
    }

    public override void SetAnimation()
    {
        
    }

    private IEnumerator DashDuration()
    {
        float dashRemaining = dashDuration;
        
        while (dashRemaining > 0f)
        {
            dashRemaining -= Time.deltaTime;
            //Debug.Log(dashRemaining.ToString());
            float decayratio = dashRemaining / dashDuration;
            _playerController.SetHorizontalForce(dashDirection.x * dashSpeed * decayratio);
            _playerController.SetVerticalForce(dashDirection.y * dashSpeed * decayratio);
            yield return null;
        }
    }

    private void SetDashColour(bool newState)
    {
        sr.color = (newState) ? Color.white : Color.blue;
    }

    //unused cooldown
    private IEnumerator DashCooldown()
    {
        float dashElapsedCD = 0f;

        while (dashElapsedCD < dashCooldown)
        {
            dashElapsedCD += Time.deltaTime;
            //Debug.Log(dashRemaining.ToString());
            
            yield return null;
        }

        canDash = true;
    }

    


}