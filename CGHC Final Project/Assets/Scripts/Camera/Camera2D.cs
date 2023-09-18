using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Horizontal")] 
    //[SerializeField] private PlayerMotor playerToFollow;    //DELETE it
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;

    [Header("Horizontal")] 
    [SerializeField] [Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;

    [Header("Vertical")] 
    [SerializeField] [Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    // The target reference    
    public PlayerMotor Target { get; set; }

    // Position of the Target  
    public Vector3 TargetPosition { get; set; }    
   
    // Reference of the Target Position known by the Camera    
    public Vector3 CameraTargetPosition { get; set; }

    private float _targetHorizontalSmoothFollow;
    private float _targetVerticalSmoothFollow;

    /*private void Awake()    // REMOVE this because we will center the Camera 
    {					// when the Player is spawned using the CenterOnTarget() method
        CenterOnTarget(playerToFollow);
    }*/

    private void Update()
    {
        MoveCamera();
    }
	
	private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    // Moves our Camera
    private void MoveCamera()
    {
		if (Target.transform.position.x > -50 
            && Target.transform.position.x < 0 
            && Target.transform.position.y > -5
            && Target.transform.position.y < 10 )
        {
            horizontalInfluence = 0.5f;
            verticalInfluence = 0.2f; 
        }
        else
        {
            horizontalInfluence = 1f;
            verticalInfluence = 1f;
        }		
        // Calculate Position
        //TargetPosition = GetTargetPosition(playerToFollow);
        TargetPosition = GetTargetPosition(Target);
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);
        
        // Follow on selected axis
        float xPos = horizontalFollow ? CameraTargetPosition.x : transform.localPosition.x;
        float yPos = verticalFollow ? CameraTargetPosition.y : transform.localPosition.y;
        
        // Set offset
        CameraTargetPosition += new Vector3(horizontalFollow ? horizontalOffset : 0f, verticalFollow ? verticalOffset : 0f, 0f);
        
        // Set smooth value
        _targetHorizontalSmoothFollow = Mathf.Lerp(_targetHorizontalSmoothFollow, CameraTargetPosition.x,
            horizontalSmoothness * Time.deltaTime);
        _targetVerticalSmoothFollow = Mathf.Lerp(_targetVerticalSmoothFollow, CameraTargetPosition.y,
            verticalSmoothness * Time.deltaTime);
        
        // Get direction towards target pos
        float xDirection = _targetHorizontalSmoothFollow - transform.localPosition.x;
        float yDirection = _targetVerticalSmoothFollow - transform.localPosition.y;
        Vector3 deltaDirection = new Vector3(xDirection, yDirection, 0f);
        
        // New position
        Vector3 newCameraPosition = transform.localPosition + deltaDirection;
        
        // Apply new position
        transform.localPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.localPosition.z);
    }

    // Returns the position of out target
    private Vector3 GetTargetPosition(PlayerMotor player)
    {
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;
        
        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);
        return positionTarget;
    }

    // Centers our camera in the target position
    private void CenterOnTarget(PlayerMotor player)
    {
        //Vector3 targetPosition = GetTargetPosition(player);
        Target = player;

        Vector3 targetPos = GetTargetPosition(Target);   
        _targetHorizontalSmoothFollow = targetPos.x;
        _targetVerticalSmoothFollow = targetPos.y;
     
        transform.localPosition = targetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 camPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f);
        Gizmos.DrawWireSphere(camPosition, 0.5f);
    }

    private void OnEnable()
    {
        LevelManager.OnPlayerSpawn += CenterOnTarget;
    }

    private void OnDisable()
    {
        LevelManager.OnPlayerSpawn -= CenterOnTarget;
    }
}
