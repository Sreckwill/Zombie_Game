using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackiState : ZombieStates
{
    public ZombieAttackiState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Attack");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}
