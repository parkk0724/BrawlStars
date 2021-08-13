using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    protected delegate bool DelegateHeroOnBush();
    protected DelegateHeroOnBush HeroOnBush;
    protected bool m_bBushAttack = false;
    protected enum State { IDLE, MOVE, PATROL, ATTACK, DEAD }
    protected State m_eState = State.IDLE;
    protected NavMeshAgent m_NavMeshAgent;
    protected Transform m_tfTarget;
    protected Vector3 m_vDestination;
    float m_fRandomMoveRange = 0.0f;
    protected virtual void Start()
    {
        m_UITextDamage = GameObject.Find("UI").GetComponentInChildren<UITextDamage>();
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;   // Current Hp
        m_nATK = 10;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;
        m_fRandomMoveRange = 10.0f;
        m_NavMeshAgent = this.GetComponent<NavMeshAgent>();
        m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        HeroOnBush = m_tfTarget.GetComponent<Hero>().GetOnBush;
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
    protected virtual void ChangeState(State state)
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

    protected virtual void ProgressState()
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

    public virtual void Idle() {}
    public override void Move() {}
    public override IEnumerator Die()
    {

        m_Animator.SetTrigger("tDie");

        while (true) //일단 죽고나서 게임 종료 시킬 거 생각해서 디스트로이는 주석처리 함
        {
            //Destroy(this.transform.parent.gameObject); // 일단 죽으면 사라지게 만듬
            yield return null;
        }
    }
    public virtual void Attack()
    {
    }

    public override void Hit(int damage, Color c)
    {
        base.Hit(damage, c);

        m_bBushAttack = true;
        m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public bool RandomPoint(out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 rndPoint = this.transform.position + Random.insideUnitSphere * m_fRandomMoveRange; // 랜덤하게 이 오브젝트 주면 위치를 가져옴
            NavMeshHit hit;
            if (NavMesh.SamplePosition(rndPoint, out hit, 1.0f, NavMesh.AllAreas)) // 랜덤한 위치가 NavMesh로 이동할 수 있는지 확인
            {
                result = hit.position; // 가능하면 그 위치값 내보냄
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
