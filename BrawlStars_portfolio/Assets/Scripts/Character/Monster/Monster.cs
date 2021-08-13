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

        while (true) //�ϴ� �װ��� ���� ���� ��ų �� �����ؼ� ��Ʈ���̴� �ּ�ó�� ��
        {
            //Destroy(this.transform.parent.gameObject); // �ϴ� ������ ������� ����
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
            Vector3 rndPoint = this.transform.position + Random.insideUnitSphere * m_fRandomMoveRange; // �����ϰ� �� ������Ʈ �ָ� ��ġ�� ������
            NavMeshHit hit;
            if (NavMesh.SamplePosition(rndPoint, out hit, 1.0f, NavMesh.AllAreas)) // ������ ��ġ�� NavMesh�� �̵��� �� �ִ��� Ȯ��
            {
                result = hit.position; // �����ϸ� �� ��ġ�� ������
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
