using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Cinemachine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class  AimStateManager : MonoBehaviour
{
    public AimBaseState currState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();
    
    
    [SerializeField] private float mouseSense = 1;
    private float xAxis, yAxis;
    [HideInInspector] public Animator anim;
    [SerializeField] private Transform canFollowPos;

    
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currFov;
    public float fovSmoothSpeed=10;

    
    public Transform aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] private float aimSmoothSpeed=20;
    [SerializeField] private LayerMask aimMask;


    private float xFollowPos;
    private float yFollowPos, originalYPos;
    [SerializeField] private float crouchCamHeight=0.6f;
    [SerializeField] private float shoulderSwapSpeed = 10;
    private MovementStateManager movementState;
    
    

    private void Start()
    {
        movementState = GetComponent<MovementStateManager>();
        xFollowPos = canFollowPos.localPosition.x;
        originalYPos = canFollowPos.localPosition.y;
        yFollowPos = originalYPos;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X")*mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y")*mouseSense;
        yAxis = Math.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currFov, fovSmoothSpeed * Time.deltaTime);


        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
      
     
            Ray ray = Camera.main.ScreenPointToRay(screenCentre);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }
        
        CameraLeftAndRightMovment();

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
    }

    void CameraLeftAndRightMovment()
    {

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
           
            xFollowPos = -xFollowPos;
        }

        if (movementState.currentState == movementState.Crouch)
        {
            yFollowPos = crouchCamHeight;
        }
        else
        {
            yFollowPos = originalYPos;
        }
        
        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, canFollowPos.localPosition.z);
        
        canFollowPos.localPosition = Vector3.Lerp(canFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);

    }
}
