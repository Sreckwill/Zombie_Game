using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    public ZombieAttackState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Attack");
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    
}
