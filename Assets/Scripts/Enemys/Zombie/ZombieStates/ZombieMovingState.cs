using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovingState : ZombieStates
{

    public ZombieMovingState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }
    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        MoveToThePoint();
        FieldOfViewCheck();
    }

    public override void ExitState()
    {

    }

    private void MoveToThePoint()
    {
        if (!stateMechine.walkPointSet) SearchWalkPoint();
        if (stateMechine.walkPointSet)
        {
            stateMechine.Agent.SetDestination(stateMechine.walkPoint);
        }
        Vector3 distanceToWalkPoint = stateMechine.transform.position - stateMechine.walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            stateMechine.walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-stateMechine.walkingPointrange, stateMechine.walkingPointrange);
        float randomX = Random.Range(-stateMechine.walkingPointrange, stateMechine.walkingPointrange);

        stateMechine.walkPoint = new Vector3(stateMechine.transform.position.x + randomX, stateMechine.transform.position.y, stateMechine.transform.position.z + randomZ);

        if (Physics.Raycast(stateMechine.walkPoint, -stateMechine.transform.up, 2f, stateMechine.ground))
        {
            stateMechine.walkPointSet = true;
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
                    stateMechine.SwitchState(new ZombieFOVState(stateMechine));
                }
            }
        }
    }
}
