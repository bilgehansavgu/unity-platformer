using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "playerConfig", menuName = "Player/Configuration")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] float movementSpeed = 10f;
        [SerializeField] float groundModifier = 1f;
        [SerializeField] float airbourneModifier = 0.6f;
        [SerializeField] float groundedFriction = 1f;
        [Header("Dashing")]
        [SerializeField] float dashForce = 14f;
        [SerializeField] float dashCooldown = 1f;
        
        [Header("Combat")]
        [SerializeField] float hitForce = 4f;
        [SerializeField] float attackCooldown = 1f;
        [SerializeField] float chargeMultiplier = 1f;

        [Header("Gravity values")]
        [SerializeField] float groundedForce = 1f;
        [SerializeField] float fallLoad = 4f;
        [SerializeField] float jumpLoad = 6f;
        
        [Header("Ground Check Values")]
        [SerializeField] float groundCheckDistance = 3f;
        [SerializeField] float fallCheckDistance = 0.55f;
        
        [Header("Collision Layers")]
        [SerializeField] LayerMask whatIsGround;
        [SerializeField] LayerMask whatIsWall;
    
        public float MovementSpeed => movementSpeed;
        public float GroundModifier => groundModifier;
        public float AirbourneModifier => airbourneModifier;
        public float GroundedFriction => groundedFriction;
        public float DashForce => dashForce;
        public float DashCooldown => dashCooldown;
        public float HitForce => hitForce;
        public float AttackCooldown => attackCooldown;
        public float ChargeMultiplier => chargeMultiplier;
        public float GroundedForce => groundedForce;
        public float FallLoad => fallLoad;
        public float JumpLoad => jumpLoad;
        public float GroundCheckDistance => groundCheckDistance;
        public float FallCheckDistance => fallCheckDistance;
        public LayerMask WhatIsGround => whatIsGround;
        public LayerMask WhatIsWall => whatIsWall;

    }

}

