using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    protected enum State { IDLE, MOVE, ATTACK, DEAD }

    State m_eState = State.MOVE;
    NavMeshAgent m_NavMeshAgent;
    Transform m_tfTarget;
    protected virtual void Start()
    {
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;   // Current Hp
        m_nATK = 10;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;
        m_NavMeshAgent = this.GetComponent<NavMeshAgent>();
        m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_nHP <= 0)
        {
            StartCoroutine(Die());
            ChangeState(State.DEAD);
        }
        ProgressState();
    }
    protected void ChangeState(State state)
    {
        if (m_eState == state) return;
        m_eState = state;

        switch (m_eState)
        {
            case State.IDLE:
                break;
            case State.MOVE:
                break;
            case State.ATTACK:
                break;
            case State.DEAD:
                break;
        }
    }

    protected void ProgressState()
    {
        switch (m_eState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.DEAD:
                Die();
                break;
        }
    }

    void Idle()
    {

    }
    public override void Move()
    {
        if (m_tfTarget == null)
        {
            m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        else
        {
            Vector3 pos = m_tfTarget.position; // 점프대에서 점프하면 y값이 변화하여 navMesh에서 인식을 못함
            pos.y = 0;
            m_NavMeshAgent.SetDestination(pos); // target따라다니도록 목적지를 매번 갱신
            if (m_NavMeshAgent.remainingDistance < 1.0f) ChangeState(State.ATTACK); // 목저지와 거리가 1.0f보다 작다면 공격전환
        }
    }
    public override IEnumerator Die()
    {
        m_Animator.SetTrigger("tDie");
        Destroy(this.transform.parent.gameObject); // 일단 죽으면 사라지게 만듬
        yield return null;
    }
    public virtual void Attack()
    {
        ChangeState(State.MOVE); // test용
        //if (m_NavMeshAgent.remainingDistance > 1.0f) ChangeState(State.MOVE); // 목저지와 거리가 1.0f보다 크다면 이동전환
    }
}
