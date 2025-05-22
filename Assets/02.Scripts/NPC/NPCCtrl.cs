using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum eNPCState
{
    Idle,
    Walk,
    Wandering,
    Attack
}


public class NPCCtrl : MonoBehaviour
{
    
    public Transform player;
    public float distanceFromTarget;

    [Header("States")]
    public int hp;
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    private NavMeshAgent agent; 
    private eNPCState state;
    public float detectDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;

    [Header("Attack")]
    public int Damage;
    public float attackRate;


    Animator animator;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    void Start()
    {
        SetState(eNPCState.Idle);
    }

    void Update()
    {
        distanceFromTarget = Vector3.Distance(transform.position, CharacterManager.Instance.player.transform.position);

        switch (state)
        {
            case eNPCState.Idle:
                SetState(eNPCState.Idle);
                break;
            case eNPCState.Walk:
                SetState(eNPCState.Walk);
                break;
            case eNPCState.Attack:
                SetState(eNPCState.Attack);
                break;
        }
    }

    void SetState(eNPCState _state)
    {
        switch (_state)
        {
            case eNPCState.Idle:
                agent.speed = walkSpeed;
                FindTarget();
                break;
            case eNPCState.Walk:
                agent.speed = walkSpeed;
                break;
            case eNPCState.Wandering:
                agent.speed = walkSpeed;
                break;
            case eNPCState.Attack:
                agent.speed = runSpeed;
                break;
        }
    }

    void FindTarget()
    {
        if (distanceFromTarget < detectDistance)
        {
            agent.SetDestination(CharacterManager.Instance.player.transform.position);
            animator.SetBool("Move", true);
        }
        else
        {
            state = eNPCState.Idle;
            animator.SetBool("Move", false);
        }
    }
}
