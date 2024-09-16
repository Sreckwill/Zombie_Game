using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : BaseActionState
{
    

    public override void EnterState(StatesActionManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = 0;
        actionStateManager.LeftHandIK.weight = 0;
        actionStateManager.animator.SetTrigger("Reload");
    }

    public override void UpdateState(StatesActionManager actionStateManager)
    {
        
    }
}


