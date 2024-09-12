using System.Collections;
using UnityEngine;

public class FOVState : ZombieStates
{
    public FOVState(ZombieStateMechine stateMechine) : base(stateMechine)
    {
    }

    public override void EnterState()
    {
        stateMechine.playerRef = GameObject.FindGameObjectWithTag("Player");
        stateMechine.StartCoroutine(FOVRoutine());
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        
    }
    public IEnumerator FOVRoutine()
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
        Debug.Log($"Number of targets in range: {stateMechine.rangeChecks.Length}");

        if (stateMechine.rangeChecks.Length != 0)
        {
            Transform target = stateMechine.rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - stateMechine.transform.position).normalized;

            Debug.Log($"Direction to target: {directionToTarget}");

            if (Vector3.Angle(stateMechine.transform.forward, directionToTarget) < stateMechine.angle / 2)
            {
                Debug.Log("Target is within field of view angle.");

                float distanceToTarget = Vector3.Distance(stateMechine.transform.position, target.position);
                Debug.Log($"Distance to target: {distanceToTarget}");
                stateMechine.SwitchStates(new ZombieAttackState(stateMechine));
                if (!Physics.Raycast(stateMechine.transform.position, directionToTarget, distanceToTarget, stateMechine.obstructionMask))
                {
                    Debug.Log("No obstruction detected, player is visible.");
                    stateMechine.canSeePlayer = true;
                }
                else
                {
                    Debug.Log("Obstruction detected, player is not visible.");
                    stateMechine.canSeePlayer = false;
                    SwitchToMoveState();
                }
            }
            else
            {
                Debug.Log("Target is outside field of view angle.");
                stateMechine.canSeePlayer = false;
                SwitchToMoveState();
            }
        }
        else if (stateMechine.canSeePlayer)
        {
            Debug.Log("No targets in range, player is no longer visible.");
            stateMechine.canSeePlayer = false;
            SwitchToMoveState();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(stateMechine.transform.position, stateMechine.radius);

        // Draw the field of view angle
        Vector3 leftBoundary = Quaternion.Euler(0, -stateMechine.angle / 2, 0) * stateMechine.transform.forward * stateMechine.radius;
        Vector3 rightBoundary = Quaternion.Euler(0, stateMechine.angle / 2, 0) * stateMechine.transform.forward * stateMechine.radius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(stateMechine.transform.position, stateMechine.transform.position + leftBoundary);
        Gizmos.DrawLine(stateMechine.transform.position, stateMechine.transform.position + rightBoundary);

        // If the player is visible, draw a line to the player
        if (stateMechine.canSeePlayer && stateMechine.rangeChecks.Length != 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(stateMechine.transform.position, stateMechine.rangeChecks[0].transform.position);
        }
    }

    private void SwitchToMoveState()
    {
        stateMechine.SwitchStates(new ZombieMoveState(stateMechine));
    }

}
