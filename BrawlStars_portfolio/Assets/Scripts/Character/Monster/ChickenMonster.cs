using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMonster : Monster
{
    float m_fCurTime = 0.0f;
    float m_fMaxIdleTime = 0.0f;
    float m_fRandomMoveRange = 0.0f;
    Vector3 m_vDestination;
    protected override void Start()
    {
        

        base.Start();

        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;
        m_nATK = 7;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 5.0f;
        m_fMaxIdleTime = 5.0f;
        m_fRandomMoveRange = 10.0f;
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
                {
                    m_fCurTime = 0.0f;
                    m_Animator.SetBool("bMove", false);
                    int rnd = Random.Range(0, 2);
                    if(rnd == 0) m_Animator.SetTrigger("tIdle2");
                    else m_Animator.SetTrigger("tIdle3");

                }
                break;
            case State.MOVE:
                {
                    if (RandomPoint(out m_vDestination))
                    {
                        m_NavMeshAgent.SetDestination(m_vDestination); // 랜덤하게 포인트를 정해서 갈수있는 곳일경우 이동
                        m_Animator.SetBool("bMove", true);
                    }
                    else
                    {
                        ChangeState(State.IDLE); // 아닐경우 다시 IDLE
                    }
                }
                break;
            case State.ATTACK:
                m_Animator.SetTrigger("tRun");
                m_NavMeshAgent.speed *= 4;
                m_NavMeshAgent.SetDestination(m_tfTarget.position);
                break;
            case State.DEAD:
                StartCoroutine(Die());
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
                break;
        }
    }

    bool RandomPoint(out Vector3 result)
    {
        for(int i = 0; i < 30; i++)
        {
            Vector3 rndPoint = this.transform.position + Random.insideUnitSphere * m_fRandomMoveRange; // 랜덤하게 이 오브젝트 주면 위치를 가져옴
            NavMeshHit hit;
            if(NavMesh.SamplePosition(rndPoint,out hit, 1.0f, NavMesh.AllAreas)) // 랜덤한 위치가 NavMesh로 이동할 수 있는지 확인
            {
                result = hit.position; // 가능하면 그 위치값 내보냄
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    public override void Idle()
    {
        if(m_fCurTime >= m_fMaxIdleTime)
        {
            ChangeState(State.MOVE);
        }
        else if (m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange)
        {
            ChangeState(State.ATTACK);
        }
    }

    public override void Move()
    {
        if (Vector3.Distance(this.transform.position, m_vDestination) < 1.5f)
        {
            ChangeState(State.IDLE);
        }
        else if (m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange)
        {
            ChangeState(State.ATTACK);
        }
    }

    public override void Attack()
    {
        m_NavMeshAgent.SetDestination(m_tfTarget.position);

        if (Vector3.Distance(this.transform.position, m_tfTarget.position) < 1.5f)
        {
            transform.LookAt(m_tfTarget);
        }
    }

    public override IEnumerator Die()
    {
        Destroy(this.transform.parent.gameObject);
        yield return null;
    }

    public override void Hit(int damage, Color c)
    {
        base.Hit(damage, c);
        m_NavMeshAgent.velocity = Vector3.zero;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(m_vDestination, 1.0f);
    //}
}
