using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBloom : MonoBehaviour
{
  [SerializeField] private float defaultBloomAngle = 3;
  [SerializeField] private float walkBloomMultipler = 1.5f;
  [SerializeField] private float CrouchBloomMultiper = 0.5f;
  [SerializeField] private float SprintBloomMultiper = 2f;
  [SerializeField] private float ADSBloomMultiper = 0.5f;

  private MovementStateManager movementState;
  private AimStateManager aimState;

  private float currBloom;
  private void Start()
  {
    movementState = GetComponentInParent<MovementStateManager>();
    aimState = GetComponentInParent<AimStateManager>();
  }

  public Vector3 BloomAngle(Transform barrelPos)
  {
      if (movementState.currentState == movementState.Idle) currBloom = defaultBloomAngle;
      else if (movementState.currentState == movementState.Walk) currBloom = defaultBloomAngle * walkBloomMultipler;
      else if (movementState.currentState == movementState.Run) currBloom = defaultBloomAngle * SprintBloomMultiper;
      else if (movementState.currentState == movementState.Crouch)
      {
          if (movementState.dir.magnitude == 0) currBloom = defaultBloomAngle * CrouchBloomMultiper;
          else currBloom = defaultBloomAngle * CrouchBloomMultiper * walkBloomMultipler;
      }
      if (aimState.currState == aimState.Aim) currBloom *= ADSBloomMultiper;
      
      float randomX = Random.Range(-currBloom, currBloom);
      float randomY = Random.Range(-currBloom, currBloom);
      float randomZ = Random.Range(-currBloom, currBloom);

      Vector3 randomRotaion = new Vector3(randomX, randomY, randomZ);
      return barrelPos.localEulerAngles + randomRotaion;
  }

  
}
