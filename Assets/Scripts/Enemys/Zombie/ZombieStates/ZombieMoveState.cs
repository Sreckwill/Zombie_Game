using System.Collections;
using UnityEngine;

public class ZombieMoveState : ZombieStates
{
    private bool isWaiting = false; 

    public ZombieMoveState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entered ZombieMoveState");
    }

    public override void UpdateState()
    {
        if (!stateMechine.walkPointSet && !isWaiting)
            SearchWalkPoint();

        if (stateMechine.walkPointSet && !isWaiting)
        {
            stateMechine.agent.SetDestination(stateMechine.walkPoint);
            Vector3 distanceToWalkPoint = stateMechine.transform.position - stateMechine.walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                stateMechine.walkPointSet = false;
                stateMechine.StartCoroutine(WaitAtWalkPoint());
                Debug.Log("Sta");
            }
        }
    }

    public override void ExitState()
    {
        stateMechine.StopCoroutine(WaitAtWalkPoint());
        Debug.Log("Exit Move state");
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-stateMechine.walkingPointrange, stateMechine.walkingPointrange);
        float randomX = Random.Range(-stateMechine.walkingPointrange, stateMechine.walkingPointrange);
        Debug.Log("Moving to new point");

        stateMechine.walkPoint = new Vector3(stateMechine.transform.position.x + randomX,
                                             stateMechine.transform.position.y,
                                             stateMechine.transform.position.z + randomZ);

        if (Physics.Raycast(stateMechine.walkPoint, -stateMechine.transform.up, 2f, stateMechine.ground))
        {
            stateMechine.walkPointSet = true;
        }
    }

    private IEnumerator WaitAtWalkPoint()
    {
        isWaiting = true; 
        Debug.Log("Waiting at walk point for 1 second...");
        yield return new WaitForSeconds(stateMechine.waitingTime*Time.deltaTime);
        Debug.Log("Resuming movement");
        isWaiting = false; 
    }
}
