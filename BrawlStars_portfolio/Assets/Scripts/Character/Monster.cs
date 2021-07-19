using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    // Start is called before the first frame update
    NavMeshAgent m_NavMeshAgent;
    Transform m_tfTarget;

    protected override void Start()
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
        m_tfTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(!m_bDie)
        {
            Move();
            Attack();
            if(m_nHP <= 0) StartCoroutine(Die());
        }
    }
    public override void Move()
    {
        m_NavMeshAgent.SetDestination(m_tfTarget.position); // target����ٴϵ��� �������� �Ź� ����
    }
    public override IEnumerator Die()
    {
        Destroy(this.transform.parent.gameObject); // �ϴ� ������ ������� ����
        yield return null;
    }

    public override void Revival()
    {
    }
    public override void Attack()
    {

    }

    public override void SkillAttack()
    {

    }


}
