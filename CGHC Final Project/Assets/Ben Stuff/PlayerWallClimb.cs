using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimb : PlayerStates
{
    [Header("Settings")] 
    [SerializeField] private float fallFactor = 0.5f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxStamina = 1.2f;
    private float wallClingBuffer;
    public float stamina;

    private float _movement;
    private bool canClimb { get { return stamina > 0f; } }
    //private float _verticalMovement;
    
    protected override void InitState()
    {
        base.InitState();
        stamina = maxStamina;
    }

    protected override void GetInput()
    {
        if (_horizontalInput <= -0.1f || _horizontalInput >= 0.1f)
        {   
            if (!canClimb) { return; }
            WallCling();
        }
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.IsWallClinging)
        {
            CheckExitWallCling();
            WallClimb();
        }

        if (_playerController.Conditions.IsCollidingBelow)
        {
            ResetStamina();
        }
        
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
            wallClingBuffer = 1f;
        }
    }

    private void CheckExitWallCling()
    {

        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
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
    }

    void Update()
    {
        if (wallClingBuffer > 0f)
        {
            wallClingBuffer -= Time.deltaTime;
            if (wallClingBuffer <= 0f)
            {
                _playerController.Conditions.IsWallClinging = false;
            }
        }
        
    }

    private void WallClimb()
    {
        Debug.Log(Mathf.Abs(_verticalInput).ToString());

        if (Mathf.Abs(_verticalInput) < 0.1f) { return; }

        if (!canClimb) { return; }

        _playerController.SetVerticalForce(_verticalInput * speed);

        stamina -= Time.deltaTime;

        //StartCoroutine(BurnStamina());
    }
    
    private IEnumerator BurnStamina()
    {
        if (stamina > 0f)
        {
            stamina -= Time.deltaTime;
            yield return null;
        }
    }

    public void ResetStamina()
    {
        stamina = maxStamina;
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