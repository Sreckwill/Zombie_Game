using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZombieAttackState : ZombieStates
{
    public ZombieAttackState(ZombieStateMechine stateMechine) : base(stateMechine)
    {

    }

    public override void EnterState()
    {
        stateMechine.Animator.SetBool("IsAttacking", true);
    }

    public override void UpdateState()
    {
        Debug.Log("Attack");
        stateMechine.Agent.SetDestination(stateMechine.playerRef.transform.position);
    }

    public override void ExitState()
    {
        stateMechine.Animator.SetBool("IsAttacking", false);
    }


}