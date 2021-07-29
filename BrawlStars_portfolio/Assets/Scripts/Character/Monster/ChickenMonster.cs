using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMonster : Monster
{
    float m_fCurTime = 0.0f;
    float m_fMaxIdleTime = 0.0f;
    protected override void Start()
    {
        base.Start();

        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;
        m_nATK = 10;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;
        m_fMaxIdleTime = 5.0f;
    }

    private void Update()
    {
        if (m_nHP <= 0)
        {
            ChangeState(State.DEAD);
        }
        m_fCurTime += Time.deltaTime;
        ProgressState();
    }

    protected override void ChangeState(State state)
    {
        if (m_eState == state) return;
        m_eState = state;

        switch (m_eState)
        {
            case State.IDLE:
                m_fMaxIdleTime = 0.0f;
                break;
            case State.MOVE:
                //m_NavMeshAgent.SetDestination(m_NavMeshAgent.Ran);
                break;
            case State.ATTACK:
                break;
            case State.DEAD:
                break;
        }
    }

    protected override void ProgressState()
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

    bool RandomPoint(out Vector3 result)
    {
        Vector3 rndPoint = this.transform.position + Random.insideUnitSphere * m_fRange;


        result = Vector3.zero;
        return true;

        return false;
    }
}
