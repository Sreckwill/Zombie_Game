using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    
    public override void EnterState(AimStateManager aimStateManager)
    {
        aimStateManager.anim.SetBool("Aiming",true);
      
    }

    public override void UpdateState(AimStateManager aimStateManager)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aimStateManager.SwitchState(aimStateManager.Aim);
        }
    }
}
