using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    protected enum State { IDLE, MOVE, ATTACK, DEAD }
    itemDatabase databas;
    protected State m_eState = State.IDLE;
    protected NavMeshAgent m_NavMeshAgent;
    protected Transform m_tfTarget;
    protected virtual void Start()
    {
        databas = FindObjectOfType<itemDatabase>();
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
        m_NavMeshAgent = this.GetComponent<NavMeshAgent>();
        m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_nHP <= 0)
        {
            ItemDrop();
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

    public virtual void Idle()
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
            Vector3 pos = m_tfTarget.position; // �����뿡�� �����ϸ� y���� ��ȭ�Ͽ� navMesh���� �ν��� ����
            pos.y = 0;
            m_NavMeshAgent.SetDestination(pos); // target����ٴϵ��� �������� �Ź� ����
            if (m_NavMeshAgent.remainingDistance < 1.0f) ChangeState(State.ATTACK); // �������� �Ÿ��� 1.0f���� �۴ٸ� ������ȯ
        }
    }
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
        ChangeState(State.MOVE); // test��
        //if (m_NavMeshAgent.remainingDistance > 1.0f) ChangeState(State.MOVE); // �������� �Ÿ��� 1.0f���� ũ�ٸ� �̵���ȯ
    }
    public virtual void ItemDrop()
    {
        int firstrage = Random.Range(0, 10);
        
        if (firstrage < 3)
        {
            return;
        }
        else if (firstrage < 6)
        {
            ArrayList table = new ArrayList();
            for (int i = 0; i < databas.itemList.Count; i++)
            {
                if (databas.itemList[i].itemGrade == (ITemGrade)3)
                {
                    table.Add(i);
                }
            }
            int tableindex = Random.Range(0, table.Count);
            Instantiate(databas.itemList[(int)table[tableindex]].itemPrefab, this.gameObject.transform.position, databas.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);

        }
        else if (firstrage < 10)
        {
            ArrayList table = new ArrayList();
            for (int i = 0; i < databas.itemList.Count; i++)
            {
                if (databas.itemList[i].itemGrade == (ITemGrade)2)
                {
                    table.Add(i);
                }
            }
            int tableindex = Random.Range(0, table.Count);
            Instantiate(databas.itemList[(int)table[tableindex]].itemPrefab, this.gameObject.transform.position, databas.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);
        }
    }
}
