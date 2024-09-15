using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFOVState : ZombieStates
{


    public ZombieFOVState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }

    public override void EnterState()
    {
        stateMechine.StartCoroutine(FOVRoutine());
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        stateMechine.rangeChecks = Physics.OverlapSphere(stateMechine.transform.position, stateMechine.radius, stateMechine.targetMask);

        if (stateMechine.rangeChecks.Length != 0)
        {
            Transform target = stateMechine.rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - stateMechine.transform.position).normalized;

            if (Vector3.Angle(stateMechine.transform.forward, directionToTarget) < stateMechine.angle / 2)
            {
                float distanceToTarget = Vector3.Distance(stateMechine.transform.position, target.position);

                if (!Physics.Raycast(stateMechine.transform.position, directionToTarget, distanceToTarget, stateMechine.obstructionMask))
                {
                    if (distanceToTarget <= stateMechine.DistaceToAttack)
                    {
                        // Switch to Attack state
                        Attack();
                    }
                }
            }
        }
        else
        {
            // If player is not detected, go back to moving state
            MoveToThePoints();
        }
    }


    private void MoveToThePoints()
    {
        stateMechine.SwitchState(new ZombieMovingState(stateMechine));
    }

    private void Attack()
    {
        stateMechine.SwitchState(new ZombieAttackState(stateMechine));
    }




}
