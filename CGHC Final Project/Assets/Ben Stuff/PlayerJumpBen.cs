using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBen : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 1;

    private int _jumpAnimatorParameter = Animator.StringToHash("Jump");
    private int _doubleJumpParameter = Animator.StringToHash("DoubleJump");
    private int _fallAnimatorParameter = Animator.StringToHash("Fall");

    // Return how many jumps we have left
    public int JumpsLeft { get; set; }

    private PlayerWallClimb wallComp;
    private bool hasWallComp {get { return wallComp;} }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
        wallComp = GetComponent<PlayerWallClimb>();
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.IsCollidingBelow ) //&& _playerController.Force.y == 0f)
        {
            JumpsLeft = maxJumps;
            _playerController.Conditions.IsJumping = false;
            if (hasWallComp) { wallComp.ResetStamina(); } 
        }
    }

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerController.Conditions.IsWallClinging)
            {
                Jump();
                if (hasWallComp) { wallComp.stamina -= 0.5f; }   
            }

            if (JumpsLeft > 0) 
            {
                JumpsLeft -= 1;
                Jump();
            }
            
            
        }
    }

    private void Jump()
    {
        
        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(_playerController.Gravity));
        _playerController.SetVerticalForce(jumpForce);
        _playerController.Conditions.IsJumping = true;
    }

    public override void SetAnimation()
    {
        // Jump
        _animator.SetBool(_jumpAnimatorParameter, _playerController.Conditions.IsJumping 
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft > 0
                                                  && !_playerController.Conditions.IsFalling
                                                  && !_playerController.Conditions.IsJetpacking);
        
        // Double jump
        _animator.SetBool(_doubleJumpParameter, _playerController.Conditions.IsJumping 
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft == 0
                                                  && !_playerController.Conditions.IsFalling
                                                  && !_playerController.Conditions.IsJetpacking);
        
        // Fall
        _animator.SetBool(_fallAnimatorParameter, _playerController.Conditions.IsFalling 
                                                  //&& _playerController.Conditions.IsJumping
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && !_playerController.Conditions.IsJetpacking);
    }

    private void JumpResponse(float jump)
    {
        _playerController.SetVerticalForce(Mathf.Sqrt(2f * jump * Mathf.Abs(_playerController.Gravity)));
    }
    
    private void OnEnable()
    {
        Jumper.OnJump += JumpResponse;
    }

    private void OnDisable()
    {
        Jumper.OnJump -= JumpResponse;
    }

}