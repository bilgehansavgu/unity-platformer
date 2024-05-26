using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "playerConfig", menuName = "Player/Configuration")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float landingSpeed = 8f; //to slow down the landing
    
    [Header("Airbourne & Jump")]
    [SerializeField] float airbourneMoveSpeed = 5f;
    [SerializeField] float jumpVelocity = 15f;
    [SerializeField] float jumpLoad = 3f;
    [SerializeField] float fallLoad = 4f;
    
    [SerializeField] float maxFallSpeed = -15f;

    [SerializeField] int jumpCount;
    
    [SerializeField] float dashForce = 14f;
    
    [Header("Ground Check Values")]
    [SerializeField] float groundCheckDistance = 3f;
    [SerializeField] float fallCheckDistance = 0.55f;
    
    [Header("Collision Layers")]
    [SerializeField] LayerMask groundLayer;

    public float MovementSpeed => movementSpeed;
    public float LandingSpeed => landingSpeed;
    public float AirbourneMoveSpeed => airbourneMoveSpeed;
    public float JumpVelocity => jumpVelocity;
    public float JumpLoad => jumpLoad;
    public float FallLoad => fallLoad;
    public float MaxFallSpeed => maxFallSpeed;
    public int JumpCount => jumpCount;
    public float DashForce => dashForce;
    public float GroundCheckDistance => groundCheckDistance;
    public float FallCheckDistance => fallCheckDistance;
    public LayerMask GroundLayer => groundLayer;

}
