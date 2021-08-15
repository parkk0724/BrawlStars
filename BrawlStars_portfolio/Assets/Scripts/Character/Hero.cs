using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : Character
{
    public enum Start_State { NONE, START }
    public Start_State m_Start = Start_State.NONE;

    [Header("Status")]
    protected float m_fStamina;
    protected float m_fMaxStamina;
    protected float m_fFever;
    protected float m_fMaxFever;
    protected int m_nSkillDamage;
    protected bool m_bDie;
    protected float m_fBodyAttackDelay;
    protected float m_fCurBodyAttack;

    [Header("Moving")]
    public LayerMask m_lmPicking_Mask;
    public float m_fMove_Speed = 5.0f;
    public float m_fRotate_Speed = 0.0f;
    public float m_fJump_Height = 0.0f;
    public float m_fJump_Speed = 0.0f;   
    public GameObject m_objCharacter;

    protected GameObject m_objPlayerDir;
    private GameObject m_objUIDie;
    private TMPro.TMP_Text m_tDie;
    private ParticleSystem m_ptsRevival;
    private BoxCollider m_boxCollider;

    [Header("Target")]
    public float m_fTargetRotSpeed = 10.0f;
    public ParticleSystem m_objTargetEffect;
    public Transform m_tfResultTarget;
    private Transform m_tfEfResultTarget;
    public bool m_bRotStart = false;
    public bool m_bMoveStart;
    public bool m_bMoveValid = true; // 점프대 사용 시 이동 막는 bool값
    public bool m_bCheckStart = false;
    public float m_fTargetRange = 0f;    
    public LayerMask m_lmEnemyLayer = 0;

    [Header("Jump")]
    public float m_Jump_maxDelay = 0.0f;
    public GameObject m_Jump_StartSound = null;
    float m_Jump_curDelay = 0.0f;
    Transform Jump_Destination_Pos1;
    Transform Jump_Destination_Pos2;
    Coroutine Jump_Delay;
    Coroutine Jump1;
    Coroutine Jump2;

    [Header("RecoveryItem")]
    [SerializeField]private GameObject m_objHp;
    [SerializeField]private GameObject m_objStamina;
    [SerializeField]private GameObject m_objFever;
    [SerializeField]private GameObject m_objInvicible;
    [SerializeField]private ParticleSystem m_objGhost;
    public SkinnedMeshRenderer[] m_meshRenderer;
    Coroutine Hp;
    Coroutine St;
    Coroutine Fe;
    bool[] b_active = { false,false,false,false,false };
    float m_fCurtime;
    float m_fCurtime_2;

    public List<reitem> items = new List<reitem>();

    protected bool m_bOnBush = false;
    public bool GetOnBush() { return m_bOnBush; }
    public bool GetJump() { return m_bMoveValid; }
    public float GetFever() { return m_fFever; }
    public float GetMaxFever() { return m_fMaxFever; }
    public virtual void FeverUp()
    {
        if (m_fFever < m_fMaxFever) m_fFever += 10.0f;
        else m_fFever = m_fMaxFever;
    }
    public float GetStamina() { return m_fStamina; }
    public float GetMaxStamina() { return m_fMaxStamina; }
    public void RecoveryStamina()
    {
        if (m_fStamina < m_fMaxStamina)
        {
            m_fStamina += Time.deltaTime;
            if (m_fStamina > m_fMaxStamina) m_fStamina = m_fMaxStamina;
        }
    }

    protected virtual void Awake()
    {
        m_Jump_StartSound = GameObject.Find("JumpStart");
        m_objTargetEffect = GameObject.Find("TargetIndicator").GetComponent<ParticleSystem>();
        GameObject GhostEffct = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/GhostEffect"), transform.parent);
        GhostEffct.SetActive(false);
        m_objGhost = GhostEffct.GetComponent<ParticleSystem>();
        GameObject inviEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/IncivibleEffect"), transform.parent);
        inviEffect.SetActive(false);
        m_objInvicible = inviEffect.gameObject;
        GameObject FvEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/FVPotionEffect"), transform.parent);
        FvEffect.SetActive(false);
        m_objFever = FvEffect.gameObject;
        GameObject SpEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/STPotionEffect"), transform.parent);
        SpEffect.SetActive(false);
        m_objStamina = SpEffect.gameObject;
        GameObject HpeFFect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/HPPotionEffect"), transform.parent);
        HpeFFect.SetActive(false);
        m_objHp = HpeFFect.gameObject;
        GameObject JumpEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/JumpEffect"), transform.parent);
        GameObject revivalEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Particles/RevivalEffect"), transform.parent);
        revivalEffect.SetActive(false);
        m_ptsRevival = revivalEffect.GetComponent<ParticleSystem>();

        m_boxCollider = GetComponent<BoxCollider>();
        m_objIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Indicators/Hero"),transform);
        m_Jump_StartSound = GameObject.Find("JumpStart");
    }
    protected virtual void Start()
    {
        
        m_objPlayerDir = GameObject.Find("PlayerDirection");
        m_objUIDie = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UIDie"), GameObject.Find("UI").transform);
        m_objUIDie.SetActive(false);
        m_tDie = m_objUIDie.GetComponentInChildren<TMPro.TMP_Text>();
        Jump_Destination_Pos1 = GameObject.Find("Jump_Destination_Pos1").transform;
        Jump_Destination_Pos2 = GameObject.Find("Jump_Destination_Pos2").transform;        
        m_bMoveStart = true;
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;   // Current Hp
        m_fMaxStamina = 3.0f;
        m_fStamina = m_fMaxStamina;
        m_fMaxFever = 100.0f;
        m_fFever = 0.0f;
        m_nATK = 20;
        m_nDEF = 5;
        m_nSkillDamage = 20;
        m_fMoveSpeed = 5.0f;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;
        m_fBodyAttackDelay = 1.0f;
        m_fCurBodyAttack = m_fBodyAttackDelay;
        m_fJump_Height = 9.0f;
        m_fJump_Speed = 10.0f;
    }
    // Update is called once per frame
    protected virtual void Update()
    {

        if (m_Start == Start_State.START)
        {
            if (!m_bDie)
            {
                if (Time.timeScale > 0.0f)
                {
                    m_fCurBodyAttack += Time.deltaTime;
                    RecoveryStamina();
                    Move();
                    Attack();

                    if (b_active[0])
                    {
                        if (Hp != null) StopCoroutine(Hp);
                        Hp = StartCoroutine(RecoverHP());
                    }
                    if (b_active[1])
                    {
                        if (St != null) StopCoroutine(St);
                        St = StartCoroutine(RecoverST());
                    }
                    if (b_active[2])
                    {
                        if (Fe != null) StopCoroutine(Fe);
                        Fe = StartCoroutine(RecoverFV());
                    }
                    Ghostitem();
                    invicibleitem();
                    SearchTargetEffect();
                    TargetEffect();
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                    {
                        SearchTarget();
                        m_bRotStart = true;
                    }
                    if (m_bRotStart) LookEnemy();
                    if (m_nHP <= 0) StartCoroutine(Die());
                }
            }
        }
    }
    #region Hero Move
    public override void Move()
    {
        if (m_bMoveValid)
        {
            float delta = m_fMove_Speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(m_objPlayerDir.transform.forward, delta);
                if (!m_bRotStart)
                    RotaeProcess(m_objPlayerDir.transform.forward, delta, 1.0f, m_objPlayerDir.transform.right, 1.0f);
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(-m_objPlayerDir.transform.forward, delta);
                if (!m_bRotStart)
                    RotaeProcess(-m_objPlayerDir.transform.forward, delta, -1.0f, m_objPlayerDir.transform.right, 1.0f);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(-m_objPlayerDir.transform.right, delta);
                if (!m_bRotStart)
                    RotaeProcess(-m_objPlayerDir.transform.right, delta, 1.0f, m_objPlayerDir.transform.forward, 1.0f);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
            {
                TransformHero(m_objPlayerDir.transform.right, delta);
                if (!m_bRotStart)
                    RotaeProcess(m_objPlayerDir.transform.right, delta, 1.0f, -m_objPlayerDir.transform.forward, -1.0f);
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(((m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized, delta, 1.0f, (m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, 1.0f);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
            {
                TransformHero(((m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta, 1.0f, (-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, 1.0f);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(((-m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((-m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized, delta, 1.0f, (m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized, 1.0f);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
            {
                TransformHero((-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta);
                if (!m_bRotStart)
                    RotaeProcess((-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta, -1.0f, (m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, 1.0f);
            }

            else
            {
                m_Animator.SetBool("bMove", false);
            }
        }
    }

    #endregion
    public virtual void Attack()
    {
    }
    public virtual void SkillAttack()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (other.GetComponent<DropItem>())
            {
                DropItem dropitem = other.GetComponent<DropItem>();

                if (dropitem.GetItem().itemtype == "Potion")
                {
                    if(dropitem.GetItem().uSEitem == "HP")
                    {
                        m_nHP += dropitem.GetItem().itemCount; //아이템 카운트 숫자값 받아와서 더해줌 
                        if (b_active[0] && m_objHp.activeSelf) //Dropitem에서 받아옴
                        {
                            b_active[0] = false;
                            m_objHp.gameObject.SetActive(false);
                        }
                        b_active[0] = true;
                        if (m_nHP > m_nMaxHP)
                            m_nHP = m_nMaxHP;
                    }
                    else if(dropitem.GetItem().uSEitem == "STAMINA")
                    {
                        m_fStamina += dropitem.GetItem().itemCount;
                        if (b_active[1] && m_objStamina.activeSelf)
                        {
                            b_active[1] = false;
                            m_objStamina.gameObject.SetActive(false);
                        }
                        b_active[1] = true;
                        if (m_fStamina > m_fMaxStamina)
                            m_fStamina = m_fMaxStamina;
                    }
                    else if(dropitem.GetItem().uSEitem == "FEVER")
                    {
                        m_fFever += dropitem.GetItem().itemCount;
                        if (b_active[2] && m_objFever.activeSelf)
                        {
                            b_active[2] = false;
                            m_objFever.gameObject.SetActive(false);
                        }
                        b_active[2] = true;
                        if (m_fFever > m_fMaxFever)
                            m_fFever = m_fMaxFever;
                    }
                    else if(dropitem.GetItem().uSEitem == "INVINCIBLE")
                    {
                        if (b_active[3])
                        {
                            b_active[3] = false;
                            m_fCurtime = 0;
                            m_objInvicible.gameObject.SetActive(false);
                        }
                        b_active[3] = true;
                    }
                    else if(dropitem.GetItem().uSEitem == "GHOST")
                    {
                        if(b_active[4])
                        {
                            b_active[4] = false;
                            m_fCurtime_2 = 0;
                            //m_objGhost.gameObject.SetActive(true);
                        }
                        b_active[4] = true;
                    }
                    #region ##FirstSolution
                    /*
                    switch (dropitem.GetItem().use) 
                    {
                        case USE.HP:
                            m_nHP += dropitem.GetItem().itemCount; //아이템 카운트 숫자값 받아와서 더해줌 
                            if (b_active[0] && m_objHp.activeSelf) //Dropitem에서 받아옴
                            {
                                b_active[0] = false;
                                m_objHp.gameObject.SetActive(false);
                            }
                             b_active[0] = true;
                            if (m_nHP > m_nMaxHP)
                                m_nHP = m_nMaxHP;
                            break;
                        case USE.STAMINA:
                            m_fStamina += dropitem.GetItem().itemCount;
                            if (b_active[1] && m_objStamina.activeSelf)
                            {
                                b_active[1] = false;
                                m_objStamina.gameObject.SetActive(false);
                            }
                            b_active[1] = true;
                            if (m_fStamina > m_fMaxStamina)
                                m_fStamina = m_fMaxStamina;
                            break;
                        case USE.FEVER:
                            m_fFever += dropitem.GetItem().itemCount;
                            if (b_active[2] && m_objFever.activeSelf)
                            {
                                b_active[2] = false;
                                m_objFever.gameObject.SetActive(false);
                            }
                            b_active[2] = true;
                            if (m_fFever > m_fMaxFever)
                                m_fFever = m_fMaxFever;
                            break;
                        case USE.TSTOP:

                            break;
                        case USE.INVINCIBLE:
                            
                            if(b_active[3])
                            {
                                b_active[3] = false;
                                m_fCurtime = 0;
                                m_objInvicible.gameObject.SetActive(false);
                            }
                            b_active[3] = true;
                            break;
                    }*/
                    #endregion 첫번째처리방법
                }
                else
                {
                    items.Add(dropitem.GetItem());
                }
                dropitem.Death();
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Bush"))
        {
            m_bOnBush = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (m_fCurBodyAttack >= m_fBodyAttackDelay)
        {
            if (other.tag == "Monster")
            {
                Monster m = other.GetComponent<Monster>();
                if (m != null) Hit((int)other.GetComponent<Monster>()?.GetATK(), new Color(1, 0, 0, 1)); // (과녁용)몬스터스크립트 없을때
                m_fCurBodyAttack = 0.0f;
            }
        }
        if (other.tag == "Jump" )
        {
          
           m_Jump_curDelay += Time.deltaTime;
           if (m_Jump_curDelay > m_Jump_maxDelay)
           {
               if (Jump1 != null) StopCoroutine(Jump1);
               Jump1 = StartCoroutine(Jump(Jump_Destination_Pos1));
           }

        }
        if (other.tag == "Jump2")
        {

           m_Jump_curDelay += Time.deltaTime;
           if (m_Jump_curDelay > m_Jump_maxDelay)
           {
               if (Jump2 != null) StopCoroutine(Jump2);
               Jump2 = StartCoroutine(Jump(Jump_Destination_Pos2));
           }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bush"))
        {
            m_bOnBush = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jump" || other.tag == "Jump2")
        {
            m_Jump_curDelay = 0.0f;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bush"))
        {
            m_bOnBush = false;
        }
    }
    public override IEnumerator Die()
    {
        m_bDie = true;
        m_Animator.SetTrigger("tDie");
        m_Animator.SetBool("bDie", m_bDie);
        m_boxCollider.enabled = false;

        yield return new WaitForSeconds(2);

        int count = 3;

        m_objCharacter.SetActive(false);
        m_objUIDie.SetActive(true);

        while (count > 0)
        {
            m_tDie.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        m_objUIDie.SetActive(false);

        Revival();
    }
    public void Revival()
    {
        StartCoroutine(RevivalEffect());
        this.transform.position = m_vOriginPos;
        this.transform.rotation = Quaternion.Euler(m_vOriginRot);
    }
    IEnumerator RevivalEffect()
    {
        m_ptsRevival.gameObject.SetActive(true);
        m_ptsRevival.Play();
        yield return new WaitForSeconds(3);

        m_nHP = m_nMaxHP;

        m_boxCollider.enabled = true;
        m_objCharacter.SetActive(true);
        m_bDie = false;
        m_Animator.SetBool("bDie", m_bDie);

        yield return new WaitForSeconds(m_ptsRevival.main.duration);
        m_ptsRevival.gameObject.SetActive(false);
    }
    void TransformHero(Vector3 m_objPlayerDir, float delta) // 움직임
    {
        if (m_bMoveStart)
        {
            m_Animator.SetBool("bMove", true);
            this.transform.Translate(m_objPlayerDir * delta, Space.World);
        }
    }


    void RotateLR(Vector3 m_objPlayerDir)
    {
        float dot = Vector3.Dot(m_objPlayerDir, this.transform.forward);

    }

    void RotaeProcess(Vector3 m_objPlayerDir, float delta, float movedir, Vector3 dir, float dir2) //로테이션
    {
        Vector3 playerRot = this.transform.rotation.eulerAngles;
        float dot = Vector3.Dot(m_objPlayerDir, this.transform.forward);
        float dot1 = Vector3.Dot(dir, this.transform.forward);
        float rdelta = m_fRotate_Speed * Time.deltaTime;
        float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

        playerRot.y += eurler;

        if (dot == 1.0f)
        {
            //this.transform.Translate(this.transform.forward * delta, Space.World);
        }
        else if (dot >= -1.04f && dot <= -0.96f)
        {
            this.transform.Rotate(-Vector3.up * dir2 * movedir * rdelta, Space.World);
        }
        else
        {
            if (eurler - rdelta < 0.0f)
                rdelta = eurler;

            if (dot1 >= 0.0f)
            {
                //this.transform.Translate(m_objPlayerDir * delta, Space.World);
                this.transform.Rotate(-Vector3.up * movedir * rdelta, Space.World);
            }
            else
            {
                //this.transform.Translate(m_objPlayerDir * delta, Space.World);
                this.transform.Rotate(Vector3.up * movedir * rdelta, Space.World);
            }
        }
        if (Vector3.Dot(m_objPlayerDir, this.transform.forward) >= 0.97f && Vector3.Dot(m_objPlayerDir, this.transform.forward) <= 1.03f) //솔져 자꾸 방향틀면 각도 제대로 못잡는 문제때문에 오차 예외처리 한것
            this.transform.forward = m_objPlayerDir;
    }
    #region SearchTarget 몬스터를 콜라이더로 담아서 타겟팅
    protected void SearchTarget() //타겟팅 될때만 업데이트 되도록 처리
    {
        float Shortdist = 7;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer); // 주변의 검출된 콜라이더 검출
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
                if (EnemyCollider[i].GetComponent<BossMonster>()) //예외처리 부분 보스몬스터에게 먼저 타겟팅 되도록 설정함
                {
                    shorTarget = EnemyCollider[i].transform;
                    break;
                }
                if (Shortdist > Dist)
                {
                    Shortdist = Dist;
                    shorTarget = EnemyCollider[i].transform;
                }
            }
        }
        m_tfResultTarget = shorTarget; // 최종값
    }
    #endregion
    #region Lookenemy // 타겟팅을 바라보는 기능
    protected void LookEnemy()
    {
        if (m_tfResultTarget == null)
        {
            m_objTargetEffect.Stop(); 
        }
        else
        {
            float rotDir = 1.0f;
            float delta = m_fTargetRotSpeed * Time.deltaTime;
            Vector3 dir = m_tfResultTarget.position - this.transform.position; //내적 활용 해서 어느방향으로 돌껀지 결정
            Vector3 Bot = new Vector3(dir.x, 0, dir.z);
            Bot.Normalize();
            float r = Mathf.Acos(Vector3.Dot(Bot, this.transform.forward)); //라디안
            float angle = 180.0f * r / Mathf.PI; 
            float r2 = Vector3.Dot(Bot, this.transform.right);
            if (r2 < 0.0f) rotDir = -1.0f;

            if (angle - delta < 0.0f)
            {
                delta = angle;
            }

            this.transform.Rotate(Vector3.up, delta * rotDir);
        }
    }
    #endregion
    #region SerchTargetEffect // 타겟팅에 업데이트문으로 파티클 생성
    protected void SearchTargetEffect() //업데이트 문에서 계속 몬스터를 체크해주면서 처리
    {
        float Shortdist = 7;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer);
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
                if (EnemyCollider[i].GetComponent<BossMonster>())
                {
                    shorTarget = EnemyCollider[i].transform;
                    break;
                }
                if (Shortdist > Dist)
                {
                    Shortdist = Dist;
                    shorTarget = EnemyCollider[i].transform;
                }
            }
        }
        m_tfEfResultTarget = shorTarget; // 최종값
    }
   protected void TargetEffect()
    {
        if (m_tfEfResultTarget == null) //타겟이 null이면 이펙트가 스탑함
        {
            m_objTargetEffect.Stop();
        }
        else
        {
            
            Vector3 pos = m_tfEfResultTarget.transform.position;
            pos.y = 1.0f;
            m_objTargetEffect.gameObject.transform.position = pos;

            m_objTargetEffect.transform.localScale = m_tfEfResultTarget.localScale;

            m_objTargetEffect.Play();
        }
    }
    #endregion
    
    IEnumerator Jump(Transform destination)
    {
        m_Jump_StartSound.GetComponent<AudioSource>().Play();

        m_bMoveValid = false;
        this.transform.LookAt(destination.position);
        Vector3 dir = destination.position - this.transform.position;
        float dist1 = dir.magnitude;
        dir.Normalize();

        float dist2 = 0.0f;

        while (dist2 < dist1)
        {
            float delta = m_fJump_Speed * Time.deltaTime;
            dist2 += delta;

            if (dist2 > dist1)
            {
                delta = dist1 - (dist2 - delta);
                dist2 = dist1;
            }

            float height = Mathf.Sin(dist2 * (Mathf.PI / dist1)) * m_fJump_Height;

            this.transform.Translate(dir * delta, Space.World);

            Vector3 pos = this.transform.position;
            pos.y = height;

            this.transform.position = pos;

            yield return null;
        }
        m_Jump_curDelay = 0.0f;
        m_bMoveValid = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, m_fRange);
    }
    #region ItemEffect //아이템 이펙트 수정 여기서
    virtual protected void invicibleitem() //무적아이템이펙트  콜라이더 때문에 별도의 스크립트에서 나머지 이펙트 처리
    {
        if (b_active[3])
        {
            m_fCurtime += Time.deltaTime;
            m_objInvicible.gameObject.transform.position = this.transform.position;
            m_objInvicible.gameObject.SetActive(true);
            this.gameObject.layer = 9;
            if(m_fCurtime > 6)
            {
                b_active[3] = false;
                m_fCurtime = 0;
            }
        }
        else
        {
            this.gameObject.layer = 7;
            m_objInvicible.gameObject.SetActive(false);
        }
    }
    virtual protected void Ghostitem()
    {
        if(b_active[4])
        {
            
            m_fCurtime_2 += Time.deltaTime;
            m_objGhost.gameObject.transform.position = this.transform.position;
            m_objGhost.gameObject.SetActive(true);
            m_meshRenderer[0].material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            m_meshRenderer[0].material.color = new Color(1, 1, 1, 0.3f);
            m_meshRenderer[1].material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            m_meshRenderer[1].material.color = new Color(1, 1, 1, 0.3f);
            m_meshRenderer[2].material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            m_meshRenderer[2].material.color = new Color(1, 1, 1, 0.3f);
            this.gameObject.layer = LayerMask.NameToLayer("Ghost");

            m_fMove_Speed = 10f;
            if (m_fCurtime_2 > 6)
            {
                b_active[4] = false;
                m_fCurtime_2 = 0;
            }
        }
        else
        {
            m_meshRenderer[0].material.shader = Shader.Find("Standard");
            m_meshRenderer[0].material.color = new Color(1, 1, 1, 1);
            m_meshRenderer[1].material.shader = Shader.Find("Standard");
            m_meshRenderer[1].material.color = new Color(1, 1, 1, 1);
            m_meshRenderer[2].material.shader = Shader.Find("Standard");
            m_meshRenderer[2].material.color = new Color(1, 1, 1, 1);
            this.gameObject.layer = 7;
            m_objGhost.gameObject.SetActive(false);
            m_fMove_Speed = 5f;
        }
    }
   IEnumerator RecoverHP() //Hp회복 이펙트
    {

        m_objHp.gameObject.transform.position = this.transform.position;
        m_objHp.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_objHp.gameObject.SetActive(false);
        b_active[0] = false;
        
    }
    IEnumerator RecoverST()//St회복 이펙트
    {
        m_objStamina.gameObject.transform.position = this.transform.position;
        m_objStamina.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_objStamina.gameObject.SetActive(false);
        b_active[1] = false;
    }
    IEnumerator RecoverFV()//Fv회복 이펙트
    {
        m_objFever.gameObject.transform.position = this.transform.position;
        m_objFever.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_objFever.gameObject.SetActive(false);
        b_active[2] = false;
    }
    #endregion
}