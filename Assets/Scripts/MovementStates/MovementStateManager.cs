using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3;

    [HideInInspector] public Vector3 dir;

    private float vInput, hInput;

    private CharacterController controller;
    
    //gravity
    [SerializeField] private float groundYOffset;

    [SerializeField] private LayerMask groundMask;
    private float gravity=-9.8f;
    private Vector3 Velocity;

    public Vector3 spherePos;

    public MovementBaseState currentState;
    public IdleState Idle = new IdleState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public WalkState Walk = new WalkState();
    
    //ani
    [HideInInspector] public Animator animator;
    void Start()
    {

        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hInput;
        controller.Move(dir * Time.deltaTime);
        Gravity();
        animator.SetFloat("hzInput",hInput);
        animator.SetFloat("vInput",vInput);

        currentState.UpdateState(this);

    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
        
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, transform.position.y - groundYOffset, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!isGrounded()) Velocity.y += gravity * Time.deltaTime;
        else if (Velocity.y < 0) Velocity.y = -2;
        controller.Move(Velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.black;
        Gizmos.DrawWireSphere(spherePos, transform.position.y - groundYOffset);
    }
}
