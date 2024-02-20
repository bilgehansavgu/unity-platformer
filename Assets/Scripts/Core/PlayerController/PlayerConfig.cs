using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerConfig", menuName = "Player/Configuration")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float airbourneMoveSpeed = 4f;
    [Header("Combat")]
    [SerializeField] float hitForce = 4f;
    [SerializeField] float chargeMultiplier = 1f;
    [Header("Gravity values")]
    [SerializeField] float fallLoad = 4f;
    [SerializeField] float jumpLoad = 6f;
    [Header("Ground Check Values")]
    [SerializeField] float groundCheckDistance = 3f;
    [SerializeField] float fallCheckDistance = 0.55f;
    [Header("Collision Layers")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsWall;

    public float MovementSpeed => movementSpeed;
    public float AirbourneMoveSpeed => airbourneMoveSpeed;
    public float HitForce => hitForce;
    public float ChargeMultiplier => chargeMultiplier;
    public float FallLoad => fallLoad;
    public float JumpLoad => jumpLoad;
    public float GroundCheckDistance => groundCheckDistance;
    public float FallCheckDistance => fallCheckDistance;
    public LayerMask WhatIsGround => whatIsGround;
    public LayerMask WhatIsWall => whatIsWall;
}
