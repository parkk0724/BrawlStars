using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public enum Start_State { NONE, START }
    public Start_State m_Start = Start_State.NONE;
    protected enum PhaseState { NONE, ANGRY, FEVER }
    PhaseState m_phase = PhaseState.NONE;

    public Material m_mHeader = null;
    public Material m_mBody = null;
    public GameObject m_Skill_2 = null;
    public GameObject m_objBullet = null;
    public GameObject m_objBasicAttackPos = null;
    public Transform[] m_FirePos = null;
    public float m_RotToTarget_Speed = 5.0f;

    bool[] m_bPhase = new bool[2] { false, false };
    float m_fCurTime = 0.0f;
    float m_fMaxMoveTime = 0.0f;
    float m_fMaxIdleTime = 0.0f;
    float m_fBasicAttackRange = 0.0f;
    float m_fSkill1_AttackRange = 0.0f;
    float m_fSkill2_AttackRange = 0.0f;
    bool m_bSkil2_active = false;
    GameObject Dark_Effect;
    Main_Camera_Moving main = null;

    Coroutine die = null;
    Coroutine rot = null;

    SoundManager m_Sound;
    public bool Getactive() { return m_bSkil2_active; }
    private void Awake()
    {
        m_Sound = GameObject.Find("Sound").GetComponent<SoundManager>();
        m_objIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Indicators/BossMonster"), transform);
        main = GameObject.Find("StartCamerPosition").GetComponent<Main_Camera_Moving>();
    }
    protected override void Start()
    {
        base.Start();

        m_nMaxHP = 2500;
        m_nHP = m_nMaxHP;
        m_nATK = 20;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;

        m_fMaxMoveTime = 3.0f;
        m_fMaxIdleTime = 1.0f;
        m_fBasicAttackRange = 2.5f;
        m_fSkill1_AttackRange = 10.0f;
        m_fSkill2_AttackRange = 7.0f;
        this.GetComponentInChildren<Animation_Event>().endAttack = EndAttack;
        this.GetComponentInChildren<Animation_Event>().bossMonFire = BossMonFire;
        this.GetComponentInChildren<Animation_Event>().skill_attack2 =()=>StartCoroutine(BossMon_Skill2());
        this.GetComponentInChildren<Animation_Event>().basicAttack = OnBasicAttack;
        Dark_Effect = GameObject.Find("CFX3_DarkMagicAura_A");        
        ColorChange(m_mHeader, 1.0f, 1.0f, 1.0f);
        ColorChange(m_mBody, 1.0f, 1.0f, 1.0f);
        Dark_Effect.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Start == Start_State.START)
        {
            if (m_nHP <= 0)
            {
                ColorChange(m_mHeader, 1.0f, 1.0f, 1.0f);
                ColorChange(m_mBody, 1.0f, 1.0f, 1.0f);

                ChangeState(State.DEAD);
                Dark_Effect.SetActive(false);
            }
            else
            {
                m_fCurTime += Time.deltaTime;
                ProgressState();
                CheckPhase();
            }
        }

        // 히어로가 부쉬에서 나갔을 때
        if (!HeroOnBush() && m_bBushAttack || !HeroOnBush() && m_tfTarget == null)
        {
            m_bBushAttack = false;
            m_tfTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // 히어로가 부쉬에 들어갔을 때
        if (HeroOnBush() && m_tfTarget != null && !m_bBushAttack)
        {
            m_tfTarget = null;
            ChangeState(State.PATROL);
        }
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
                    ChangeState(State.PATROL);
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
            case State.PATROL:
                if (RandomPoint(out m_vDestination))
                {
                    m_NavMeshAgent.SetDestination(m_vDestination); // 랜덤하게 포인트를 정해서 갈수있는 곳일경우 이동
                    m_Animator.SetBool("bMove", true);
                }
                break;
            case State.DEAD:
                if (die == null)
                {
                    m_NavMeshAgent.SetDestination(this.transform.position);
                    die = StartCoroutine(Die());
                }
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
            case State.PATROL:
                Patrol();
                break;
            case State.DEAD:
                // 마찬가지
                break;
        }
    }

    public void Patrol()
    {
        if (Vector3.Distance(this.transform.position, m_vDestination) < 3.0f)
        {
            ChangeState(State.IDLE);
        }
        else if (m_tfTarget != null && Vector3.Distance(m_tfTarget.position, this.transform.position) < m_fRange)
        {
            ChangeState(State.ATTACK);
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
            if (m_tfTarget == null)
            {
                ChangeState(State.PATROL);
                return;
            }

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
        if (m_tfTarget == null) return;

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
        if (m_nHP <= m_nMaxHP / 2 && m_phase == PhaseState.NONE) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 첫 페이즈가 바뀔때)
        {
            Main_Camera_Moving main = GameObject.Find("StartCamerPosition").GetComponent<Main_Camera_Moving>();
            StartCoroutine(Sound_FadeOut());

            m_phase = PhaseState.ANGRY;
            m_Animator.SetTrigger("tPowerUp");
            ColorChange(m_mHeader, 1.0f, 0.5f, 0.5f);
            ColorChange(m_mBody, 1.0f, 0.5f, 0.5f);
            Dark_Effect.SetActive(true);
            m_bPhase[0] = true;

            m_fMaxIdleTime = 0.5f;
            // 여기서 상태값 조절
        }
        else if (m_nHP <= m_nMaxHP / 4 && m_phase == PhaseState.ANGRY) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 두번째 페이즈가 바뀔때)
        {
            m_phase = PhaseState.FEVER;
            m_Animator.SetTrigger("tPowerUp");
            ColorChange(m_mHeader, 1.0f, 0.0f, 0.0f);
            ColorChange(m_mBody, 1.0f, 0.0f, 0.0f);
            m_bPhase[1] = true;

            m_fMaxIdleTime = 0.1f;
        }
    }

    IEnumerator Sound_FadeOut()
    {
        float volume = m_Sound.Playing.GetComponent<AudioSource>().volume;
        while (volume > 0.0f)
        {
            float delta = 1.0f * Time.deltaTime;

            if (volume - delta < 0.0f)
            {
                delta = volume;
            }

            volume -= delta;
            m_Sound.Playing.GetComponent<AudioSource>().volume = volume;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        m_Sound.Playing.SetActive(false);
        m_Sound.PlaySound(m_Sound.Playing_Angry);
    }
    public override void Attack()
    {
        int rnd = Random.Range(1, 100);

        switch(m_phase)
        {
            case PhaseState.FEVER :
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
                break;
            case PhaseState.ANGRY:
                {
                    if (Vector3.Distance(this.transform.position, m_tfTarget.position) > m_fBasicAttackRange && Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
                    {
                        if (rnd < 70)
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
                break;
            case PhaseState.NONE:
                {
                    BasicAttack();
                }
                break;
        }
    }

    void BasicAttack()
    {
        if(Vector3.Distance(this.transform.position, m_tfTarget.position) <= m_fBasicAttackRange)
        {
            //if (rot != null) StopCoroutine(rot); // 부드럽게 로테이션 돌리려고 slerp사용 했는데 버그 약간 있어서 잠시 주석
            //rot = StartCoroutine(LookAtTarget(m_tfTarget));
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
            //if (rot != null) StopCoroutine(rot);
            //rot = StartCoroutine(LookAtTarget(m_tfTarget));
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
        //if (rot != null) StopCoroutine(rot);
        //rot = StartCoroutine(LookAtTarget(m_tfTarget));
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

    IEnumerator BossMon_Skill2()
    {
        Instantiate(m_Skill_2, this.transform.position, Quaternion.identity);
        Collider[] player = Physics.OverlapSphere(this.transform.position, m_fSkill2_AttackRange);
        foreach (Collider Player in player)
        {
            if (Player.tag == "Player" && Player.gameObject.layer == 7)
            {
                if(Player.GetComponent<Hero>())
                {
                    Player.GetComponent<Hero>().Hit(15, Color.red);
                }
            }
        }
        m_bSkil2_active = true;
        yield return null;
        m_bSkil2_active = false;
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

    IEnumerator LookAtTarget(Transform target)
    {
        Vector3 targetRot = this.transform.rotation.eulerAngles;
        Vector3 Rot = targetRot;
        float d1 = Vector3.Dot(this.transform.forward, (target.position - this.transform.position).normalized);
        float r = Mathf.Acos(d1);
        float e = 180.0f * (r / Mathf.PI);
        float d2 = Vector3.Dot(this.transform.right, (target.position - this.transform.position).normalized);

        if (d2 < 0.0f)
            e = -e;

        while (e > 0.0f)
        {
            float delta = e * 10.0f * Time.deltaTime;           

            if (e - delta < 0.0f)
            {
                delta = e;
            }
            targetRot.y += delta;
            e -= delta;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(targetRot), m_RotToTarget_Speed * Time.deltaTime);

            yield return null;
        }
        Rot.y += e;
        this.transform.rotation = Quaternion.Euler(Rot);
        rot = null;
    }
}