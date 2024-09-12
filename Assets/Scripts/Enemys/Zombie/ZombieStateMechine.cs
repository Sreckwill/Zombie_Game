using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMechine : MonoBehaviour
{
    private ZombieStates currentStates;

    public float waitingTime;
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public  Collider[] rangeChecks;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkingPointrange;
    public LayerMask ground;

    public NavMeshAgent agent;
    public Animator Animator;

    public FOVState FOVState;
    public ZombieMoveState MoveState;
    public ZombieAttackState AttackState;

    // Start is called before the first frame update
    void Start()
    {
        waitingTime = 0f;
        FOVState=new FOVState(this);
        MoveState = new ZombieMoveState(this);
        AttackState = new ZombieAttackState(this);
        currentStates = FOVState;
        currentStates?.EnterState();
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStates?.UpdateState();
    }

    public void SwitchStates(ZombieStates newStates)
    {
        currentStates?.ExitState();
        currentStates = newStates;
        currentStates?.EnterState();
    }

}
