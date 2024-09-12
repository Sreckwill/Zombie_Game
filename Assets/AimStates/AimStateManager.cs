using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimStateManager : MonoBehaviour
{
    public AimBaseState currState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();
    
    [SerializeField] private float mouseSense = 1;
    private float xAxis, yAxis;
    [HideInInspector] public Animator anim;

    [SerializeField] public Transform canFollowPos;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X")*mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y")*mouseSense;
        yAxis = Math.Clamp(yAxis, -80, 80);
        currState.UpdateState(this);
    }

    private void LateUpdate()
    {
        canFollowPos.localEulerAngles =
            new Vector3(yAxis, canFollowPos.localEulerAngles.y, canFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currState = state;
        currState.EnterState(this);
        currState.EnterState(this);
            

    }
}
