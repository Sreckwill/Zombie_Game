using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    Collider[] rangeChecks;
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
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
         rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        Debug.Log($"Number of targets in range: {rangeChecks.Length}");

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            Debug.Log($"Direction to target: {directionToTarget}");

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                Debug.Log("Target is within field of view angle.");

                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                Debug.Log($"Distance to target: {distanceToTarget}");

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    Debug.Log("No obstruction detected, player is visible.");
                    canSeePlayer = true;
                }
                else
                {
                    Debug.Log("Obstruction detected, player is not visible.");
                    canSeePlayer = false;
                }
            }
            else
            {
                Debug.Log("Target is outside field of view angle.");
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            Debug.Log("No targets in range, player is no longer visible.");
            canSeePlayer = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Draw the field of view angle
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // If the player is visible, draw a line to the player
        if (canSeePlayer && rangeChecks.Length != 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, rangeChecks[0].transform.position);
        }
    }
}
