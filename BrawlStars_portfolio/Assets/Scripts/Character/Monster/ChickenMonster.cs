using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMonster : Monster
{
    public enum Start_State { NONE, START }
    public Start_State m_Start = Start_State.NONE;

    float m_fCurTime = 0.0f;
    float m_fMaxIdleTime = 0.0f;
    float m_fRunSpeed = 0.0f;

    BossMonster Boss;
    private void Awake()
    {
        m_objIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Indicators/Monster"), transform);
    }

    private void OnEnable()
    {
        Boss = GameObject.Find("BossMonster").GetComponentInChildren<BossMonster>();
        m_Start = Start_State.START;
    }
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
        m_fRunSpeed = 3.0f;
    }

    private void Update()
    {
        if (m_Start == Start_State.START)
        {
            if (m_nHP <= 0)
            {
                ChangeState(State.DEAD);
            }
            m_fCurTime += Time.deltaTime;
            ProgressState();

            // 히어로가 부쉬에서 나갔을 때
            if (m_tfTarget == null) m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
            else if (HeroOnBush == null) HeroOnBush = m_tfTarget.GetComponent<Hero>().GetOnBush;
            else if (!HeroOnBush() && m_bBushAttack)
            {
                m_bBushAttack = false;
            }
        }

        if (Boss.GetHp() <= 0.0f)
        {
            m_Start = Start_State.NONE;
        }
    }

    protected override void ChangeState(State state)
    {
        if (m_eState == state) return;
        m_eState = state;

        switch (m_eState)
        {
            case State.IDLE:
                {
                    m_NavMeshAgent.SetDestination(this.transform.position);
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
                m_Sound.PlaySound(m_Sound.m_MonsterFollow);
                m_Animator.SetTrigger("tRun");
                m_NavMeshAgent.speed *= m_fRunSpeed;
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
    public override void Idle()
    {
        if(m_fCurTime >= m_fMaxIdleTime)
        {
            ChangeState(State.MOVE);
        }
        else if (m_tfTarget == null) m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        else if (HeroOnBush == null) HeroOnBush = m_tfTarget.GetComponent<Hero>().GetOnBush;
        else if (!HeroOnBush() && m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange ||
                HeroOnBush() && m_bBushAttack && m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange)
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
        else if (m_tfTarget == null) m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        else if (HeroOnBush == null) HeroOnBush = m_tfTarget.GetComponent<Hero>().GetOnBush;
        else if (!HeroOnBush() && m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange ||
                 HeroOnBush() && m_bBushAttack && m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange)
        {
            ChangeState(State.ATTACK);
        }
    }

    public override void Attack()
    {
        if (m_tfTarget == null) m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        else if (HeroOnBush == null) HeroOnBush = m_tfTarget.GetComponent<Hero>().GetOnBush;
        else if (!m_tfTarget.gameObject.activeSelf || Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fRange * 2 || HeroOnBush() && !m_bBushAttack)
        {
            ChangeState(State.IDLE);
            m_Animator.SetTrigger("tIdle");
            m_NavMeshAgent.speed /= m_fRunSpeed;
        }
        else
        {
            m_NavMeshAgent.SetDestination(m_tfTarget.position);

            if (Vector3.Distance(this.transform.position, m_tfTarget.position) < 1.5f)
            {
                transform.LookAt(m_tfTarget);
            }
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
        ChangeState(State.ATTACK);
    }
}
