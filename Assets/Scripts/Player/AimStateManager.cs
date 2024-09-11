using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimStateManager : MonoBehaviour
{
    [SerializeField] private float mouseSense = 1;
    private float xAxis, yAxis;

    [SerializeField] public Transform canFollowPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X")*mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y")*mouseSense;
        yAxis = Math.Clamp(yAxis, -80, 80);
    }

    private void LateUpdate()
    {
        canFollowPos.localEulerAngles =
            new Vector3(yAxis, canFollowPos.localEulerAngles.y, canFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
