﻿using System;
using System.ComponentModel;
using LopapaGames.ScriptableObjects;
using UnityEngine;

public class CharacterControllerBehavior : MonoBehaviour
{
    [Header("Stats")]
    public Float jumpForce;
    public Float speed;
    [Space(1)]
    [Header("References")]
    [Tooltip("The character Model")]
    [Description("The character Model")]
    public Transform character;
    public Transform characterHead;
    public Animator Animator;
    [Header("Ground")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRange;
    [ReadOnly(true)]
    public bool isGrounded;

    [Header("Jumps")]
    public int maxJumps = 1;
    [ReadOnly(true)]
    public int jumps;
    
    private Rigidbody rb;
    private Camera _mainCamera;
    private bool _isMoving;
    private Vector3 _targetPosition;
    private readonly Vector3 _yv = new Vector3(0, 1, 0);
    private Vector3 _lookAt = Vector3.zero;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    //private CharacterController _controllerBehavior;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _mainCamera = FindCamera();
        //_controllerBehavior = GetComponent<CharacterController>();
        jumps = maxJumps;
    }
    public Camera FindCamera()
    {
        return _mainCamera ? _mainCamera : Camera.main;
    }

    private void Update()
    {
        GroundCheck();
    }
    

    void FixedUpdate()
    {
        if (rb.velocity.y <= 0 && isGrounded)
        {
            jumps = maxJumps;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        
        // //camera forward and right vectors:
        var forward = _mainCamera.transform.forward;
        var right = _mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        // _direction = forward * vertical + right * horizontal;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var jmpTrigger = Input.GetButtonDown("Jump");
        //_direction = new Vector3(horizontal, 0, vertical).normalized;
        var direction = forward * vertical + horizontal * right;
        // Vector3 m_Input = new Vector3(horizontal, 0, vertical);
        _isMoving = direction != Vector3.zero;
        Animator.SetBool(IsWalking, _isMoving);
        if (_isMoving)
        {
            Move(direction);
            var lookAt = direction.normalized;
            LookAt(lookAt);
        }
        // && _controllerBehavior.isGrounded
        if (jmpTrigger && isGrounded && jumps > 0)
        {
            isGrounded = false;
            jumps--;
            //rb.AddForce(transform.up * jumpForce.Value, ForceMode.Impulse);
            rb.AddForce(0, jumpForce.Value, 0, ForceMode.Impulse);
            Debug.Log($"Impulsing. Speed up: {rb.velocity.y}");
        }

    }

    [Header("Collision detect")]
    public float collisionCheckDistance;

    private void Move(Vector3 direction)
    {
        RaycastHit hit;
        if (rb.SweepTest(direction, out hit, collisionCheckDistance))
        {
            Debug.Log($"Collision with {LayerMask.LayerToName(hit.transform.gameObject.layer)} in ${hit.distance}");
            // direction.x = 0;
            // direction.z = 0;
        }

        // transform.Translate(direction * (speed.Value * Time.deltaTime));
        var newPos = transform.position + direction * (Time.deltaTime * speed.Value);
        // Debug.Log($"Moving from {transform.position} to {newPos}");
        rb.MovePosition(newPos);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(groundCheck.position, -transform.up, groundCheckRange, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = characterHead.position;
        Gizmos.DrawLine(position, position + _lookAt.normalized*10);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position - transform.up * groundCheckRange);
    }

    void LookAt(Vector3 lookAtDirection)
    {
        _lookAt = lookAtDirection.normalized;
        
        lookAtDirection.y = 0;
        var position = character.position;
        var localTransform2 = new Vector2(position.x, position.z);
        //Target position must be position + look at direction for rotation
        //Target must be the look at direction if we want an actual look at a specific point.
        var targetPosition2 = new Vector2(position.x + lookAtDirection.x, position.z + lookAtDirection.z);
        var diff = targetPosition2 - localTransform2;
        var radians = (Mathf.Atan2(diff.y, diff.x));
        var degrees = radians * 180 / Mathf.PI;
        if (degrees > 90)
        {
            degrees = 450 - degrees;
        }
        else
        {
            degrees = 90 - degrees;
        }

        var rotation = Quaternion.AngleAxis(degrees, _yv);
        character.rotation = rotation;
    }

}
