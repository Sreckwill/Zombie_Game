using UnityEngine;
using UnityEngine.AI;
public class MoveRanges : MonoBehaviour
{

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkingPointrange;
    public LayerMask ground;


    private Animator animator;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.CrossFadeInFixedTime("ZombieWalk", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkingPointrange, walkingPointrange);
        float randomX = Random.Range(-walkingPointrange, walkingPointrange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
        {
            walkPointSet = true;
        }
    }
}
