using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCling : PlayerStates
{
    [Header("Settings")] 
    [SerializeField] private float fallFactor = 0.5f;
    
    protected override void GetInput()
    {
        if (_horizontalInput <= -0.1f || _horizontalInput >= 0.1f)
        {
            WallCling();
        }
    }

    public override void ExecuteState()
    {
        CheckExitWallCling();
    }

    private void WallCling()
    {
        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0) //on the FLOOR or in the AIR
        {
            return;
        }

        if (_playerController.Conditions.IsCollidingLeft && _horizontalInput <= -0.1f ||
            _playerController.Conditions.IsCollidingRight && _horizontalInput >= 0.1f)
        {
            _playerController.SetWallClingMultiplier(fallFactor);
            _playerController.Conditions.IsWallClinging = true;
        }
    }

    private void CheckExitWallCling()
    {
        if (!_playerController.Conditions.IsWallClinging) { return; }

        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
        {
            ExitWallCling();
            return;
        }

        if (_playerController.MovePosition.x > 0.01f || _playerController.MovePosition.x < -0.01f )
        {
            ExitWallCling();
            return;
        }

        if (_playerController.FacingRight && _horizontalInput < 0.01f)
        {
            ExitWallCling();  
            return;
        }

        if (!_playerController.FacingRight && _horizontalInput > -0.01f)
        {
            ExitWallCling();
            return;
        }
    }

    private void ExitWallCling()
    {
        _playerController.SetWallClingMultiplier(0f);
        _playerController.Conditions.IsWallClinging = false;
    }

    /* private void ExitWallCling()
    {
        if (_playerController.Conditions.IsWallClinging)
        {
            if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }

            if (_playerController.FacingRight)
            {
                if (_horizontalInput <= -0.1f || _horizontalInput < 0.1f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
            else
            {
                if (_horizontalInput >= 0.1f || _horizontalInput > -0.1f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
        }
    } */
}   