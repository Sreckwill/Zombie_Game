using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMechine : MonoBehaviour
{
    private ZombieStates currentStates;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
   public  Collider[] rangeChecks;

    public FOVState FOVState;
    public ZombieAttackiState AttackiState;
    // Start is called before the first frame update
    void Start()
    {
        FOVState=new FOVState(this);
        AttackiState=new ZombieAttackiState(this);
        currentStates=FOVState;
        currentStates?.EnterState();
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
