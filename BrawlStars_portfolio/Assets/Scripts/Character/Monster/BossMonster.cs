using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public Material m_mHeader = null;
    public Material m_mBody = null;
    public GameObject m_objBullet = null;
    public GameObject m_Skill2 = null;
    public GameObject m_objBasicAttackPos = null;
    public Transform[] m_FirePos = null;

    bool[] m_bPhase = new bool[2] { false, false };
    float m_fCurTime = 0.0f;
    float m_fMaxMoveTime = 0.0f;
    float m_fMaxIdleTime = 0.0f;
    float m_fBasicAttackRange = 0.0f;
    float m_fSkill1_AttackRange = 0.0f;
    float m_fSkill2_AttackRange = 0.0f;

    GameObject Dark_Effect;

    Coroutine die = null;
    protected override void Start()
    {
        base.Start();

        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;
        m_nATK = 20;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;

        m_fMaxMoveTime = 3.0f;
        m_fMaxIdleTime = 1.0f;
        m_fBasicAttackRange = 2.0f;
        m_fSkill1_AttackRange = 10.0f;
        m_fSkill2_AttackRange = 7.0f;
        this.GetComponentInChildren<Animation_Event>().endAttack = EndAttack;
        this.GetComponentInChildren<Animation_Event>().bossMonFire = BossMonFire;
        this.GetComponentInChildren<Animation_Event>().skill_attack2 = BossMon_Skill2;
        this.GetComponentInChildren<Animation_Event>().basicAttack = OnBasicAttack;
        Dark_Effect = GameObject.Find("CFX3_DarkMagicAura_A");
        ColorChange(m_mHeader, 1.0f, 1.0f, 1.0f);
        ColorChange(m_mBody, 1.0f, 1.0f, 1.0f);
        Dark_Effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_nHP <= 0)
        {
            ColorChange(m_mHeader, 1.0f, 1.0f, 1.0f);
            ColorChange(m_mBody, 1.0f, 1.0f, 1.0f);

            ChangeState(State.DEAD);
            Dark_Effect.SetActive(false);
        }
        m_fCurTime += Time.deltaTime;
        ProgressState();
        CheckPhase();
    }

    protected override void ChangeState(State state)
    {
        if (m_eState == state) return;
        m_eState = state;

        switch (m_eState)
        {
            case State.IDLE:
                m_NavMeshAgent.SetDestination(this.transform.position);
                m_Animator.SetBool("bMove", false);
                m_fCurTime = 0.0f;
                break;
            case State.MOVE:
                if (m_tfTarget == null)
                {
                    m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
                }
                else
                {
                    FollowPlayer();
                    m_Animator.SetBool("bMove", true);
                }
                m_fCurTime = 0.0f;
                break;
            case State.ATTACK:
                m_NavMeshAgent.SetDestination(this.transform.position);
                Attack();
                break;
            case State.DEAD:
                if (die == null)
                    die = StartCoroutine(Die());
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
                // 한번만 호출되면 될것같아서 changestate로 옮김
                break;
            case State.DEAD:
                // 마찬가지
                break;
        }
    }
    public override void Idle()
    {
        if (m_fCurTime < m_fMaxIdleTime)
        {
            // Idle상태 유지
        }
        else
        {
            if (m_bPhase[1])
            {
                if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange)
                {
                    ChangeState(State.ATTACK);
                }
                else
                {
                    ChangeState(State.MOVE);
                }
            }
            else if (m_bPhase[0])
            {
                if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange)
                {
                    ChangeState(State.ATTACK);
                }
                else
                {
                    ChangeState(State.MOVE);
                }
            }
           
            else
            {
                if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
                {
                    ChangeState(State.ATTACK);
                }
                else
                {
                    ChangeState(State.MOVE);
                }
            }            
        }        
    }
    public override void Move()
    {
        int rnd = Random.Range(1, 100);
        if (m_bPhase[1])
        {
            FollowPlayer();

            if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill2_AttackRange) // 이 부분에서 계속 걸려서 이동을 안함.. 여기는 내일 고쳐야즤
            {
                if (rnd <= 65)
                    ChangeState(State.ATTACK);
                else
                    ChangeState(State.MOVE);
            }
        }
        else if (m_bPhase[0])
        {
            FollowPlayer();

            if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange)
            {
                if (rnd <= 35)
                    ChangeState(State.ATTACK);
                else
                    ChangeState(State.MOVE);
            }
            else
            {
                ChangeState(State.MOVE);
            }
        }
        else
        {
            if (m_fCurTime < m_fMaxMoveTime)
            {
                if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
                    ChangeState(State.ATTACK);
                else
                    ChangeState(State.MOVE);
            }
            else
            {
                ChangeState(State.IDLE);
            }
        }
    }
    void CheckPhase()
    {
        //if (m_nHP <= m_nMaxHP && m_nHP > m_nMaxHP / 2 && !m_bPhase[0] && !m_bPhase[1])
        //{
        //    ColorChange(m_mHeader, 1.0f, 1.0f, 1.0f);
        //    ColorChange(m_mBody, 1.0f, 1.0f, 1.0f);
        //}
        if (m_nHP <= m_nMaxHP / 2 && !m_bPhase[0]) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 첫 페이즈가 바뀔때)
        {
            m_Animator.SetTrigger("tPowerUp");
            ColorChange(m_mHeader, 1.0f, 0.5f, 0.5f);
            ColorChange(m_mBody, 1.0f, 0.5f, 0.5f);
            Dark_Effect.SetActive(true);
            m_bPhase[0] = true;

            m_fMaxIdleTime = 0.5f;
            // 여기서 상태값 조절
        }
        else if (m_nHP <= m_nMaxHP / 4 && !m_bPhase[1]) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 두번째 페이즈가 바뀔때)
        {
            m_Animator.SetTrigger("tPowerUp");
            ColorChange(m_mHeader, 1.0f, 0.0f, 0.0f);
            ColorChange(m_mBody, 1.0f, 0.0f, 0.0f);
            m_bPhase[1] = true;

            m_fMaxIdleTime = 0.1f;
        }
    }
    public override void Attack()
    {
        int rnd = Random.Range(1, 100);
        if (m_bPhase[1])
        {
            if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
            {
                if (rnd <= 70)
                    BasicAttack();
                else
                    ChangeState(State.IDLE);
            }
            else if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange && Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fSkill2_AttackRange)
            {
                if (rnd <= 90)
                {
                    if (rnd < 20)
                    {
                        Debug.Log("skill1");
                        SkillAttack1();
                    }
                    else
                    {
                        Debug.Log("Move");
                        ChangeState(State.MOVE);
                    }
                }
                else
                {
                    Debug.Log("skill2");
                    SkillAttack2();
                }
            }
            else if (Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fBasicAttackRange && Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill2_AttackRange)
            {
                if (rnd <= 20)
                {
                    //Debug.Log("skill2");
                    SkillAttack2();
                }
                else if (rnd > 20 && rnd < 40)
                {
                    //Debug.Log("skill1");
                    SkillAttack1();
                }
                else
                {
                    //Debug.Log("Idle");
                    ChangeState(State.IDLE);
                }
            }
            else
            {
                ChangeState(State.MOVE);
            }
        }
        else if (m_bPhase[0])
        {
            if (Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fBasicAttackRange && Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
            {
                if(rnd < 70)
                {
                    BasicAttack();
                }
                else
                {
                    ChangeState(State.IDLE);
                }
            }
            else if (Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fBasicAttackRange && Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange)
            {
                if (rnd < 70)
                {
                    SkillAttack1();
                }
                else
                {
                    ChangeState(State.MOVE);
                }
            }
        }
        else
        {
            BasicAttack();
        }
    }

    void BasicAttack()
    {
        if(Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
        {
          this.transform.LookAt(m_tfTarget);
          m_Animator.SetTrigger("tBAttack");
        }
        else
        {
          ChangeState(State.MOVE);
        }
    }
    void SkillAttack1()
    {
        if (Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fSkill1_AttackRange)
        {
          this.transform.LookAt(m_tfTarget);
          m_Animator.SetTrigger("tSkillAttack1");
        }
        else
        {
          ChangeState(State.MOVE);
        }
    }
    void SkillAttack2()
    {
        this.transform.LookAt(m_tfTarget);
        m_Animator.SetTrigger("tSkillAttack2");           
    }
    void EndAttack()
    {
        ChangeState(State.IDLE);
    }

    void ColorChange(Material m, float r, float g, float b)
    {
        Color c = m.color;
        c.r = r;
        c.g = g;
        c.b = b;
        m.SetColor("_Color", c);
    }

    void BossMonFire(int n)
    {
        Instantiate(m_objBullet, m_FirePos[n].position, m_FirePos[n].rotation);
    }

    void BossMon_Skill2()
    {
        Instantiate(m_Skill2, this.transform.position, Quaternion.identity);
        Collider[] player = Physics.OverlapSphere(this.transform.position, m_fSkill2_AttackRange);
        foreach (Collider Player in player)
        {
            if (Player.tag == "Player" )
            {
                Player.GetComponent<Character>().Hit(15, Color.red);
            }
        }
    }

    void OnBasicAttack()
    {
        m_objBasicAttackPos.SetActive(true);
        StartCoroutine(BasicAttackOffCollider());
    }

    IEnumerator BasicAttackOffCollider()
    {
        yield return new WaitForSeconds(0.05f);
        m_objBasicAttackPos.SetActive(false);
    }

    void FollowPlayer()
    {
        Vector3 pos = m_tfTarget.position; // 점프대에서 점프하면 y값이 변화하여 navMesh에서 인식을 못함
        pos.y = 0;
        m_NavMeshAgent.SetDestination(pos); // 플레이어 계속 따라다니게
    }
}