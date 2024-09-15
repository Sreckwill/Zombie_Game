using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        Debug.Log("Attack");
        animator.Play("ZombieAttack");
    }
}
