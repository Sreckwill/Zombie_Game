using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    

    public override void EnterState(ActionStateManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = 0;
        actionStateManager.LeftHandIK.weight = 0;
       
        actionStateManager.animator.SetTrigger("Reload");
        
        
    }

    public override void UpdateState(ActionStateManager actionStateManager)
    {
        
    }
}


