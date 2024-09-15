using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMechine : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public Collider[] rangeChecks;

    public NavMeshAgent Agent;
    public Animator Animator;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkingPointrange;
    public LayerMask ground;
    public float DistaceToAttack;


    public bool waitingAtPoint;


    private ZombieStates currentState;
    public ZombieFOVState zombieFOVState;
    public ZombieMovingState zombieMovingState;
    public ZombieAttackState zombieAttackState;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        zombieFOVState = new ZombieFOVState(this);
        zombieMovingState = new ZombieMovingState(this);
        zombieAttackState = new ZombieAttackState(this);

        currentState = zombieMovingState;
        currentState?.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState();
    }
    public void SwitchState(ZombieStates newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
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
