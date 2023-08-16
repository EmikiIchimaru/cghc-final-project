using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private Camera mainCamera;
    // return if player can dash
    public bool canDash { get; private set; }

    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {

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
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = -mainCamera.transform.position.z;

        Vector3 worldCursorPos = mainCamera.ScreenToWorldPoint(cursorPosition);
        Vector2 dashDirection = (worldCursorPos - transform.position).normalized;
        
        _playerController.SetVerticalForce(dashDirection.y * dashDistance);
        _playerController.SetHorizontalForce(dashDirection.x * dashDistance);
        //_playerController.Conditions.IsJumping = true;
    }

    public override void SetAnimation()
    {
        
    }

}