using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : Character
{
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
    public ParticleSystem m_ptsRevival;
    public GameObject m_objPlayerDir;
    public GameObject m_objCharacter;
    public GameObject m_objUIDie;
    public Text m_tDie;

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

    public float m_Jump_maxDelay = 0.0f;
    float m_Jump_curDelay = 0.0f;

    Transform Jump_Destination_Pos1;
    Transform Jump_Destination_Pos2;
    Coroutine Jump_Delay;
    Coroutine Jump1;
    Coroutine Jump2;


    public List<Item> items = new List<Item>();
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
    protected virtual void Start()
    {
        Jump_Destination_Pos1 = GameObject.Find("Jump_Destination_Pos1").transform;
        Jump_Destination_Pos2 = GameObject.Find("Jump_Destination_Pos2").transform;
        m_bMoveStart = true;
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 1000;
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
        m_fJump_Height = 5.0f;
        m_fJump_Speed = 10.0f;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!m_bDie)
        {
            m_fCurBodyAttack += Time.deltaTime;
            RecoveryStamina();
            Move();
            Attack();
        }

        SearchTargetEffect();
        TargetEffect();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            SearchTarget();
            m_bRotStart = true;
        }
        if (m_bRotStart) LookEnemy();
        if (!m_bDie && m_nHP <= 0) StartCoroutine(Die());
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
                    RotaeProcess(m_objPlayerDir.transform.forward, delta, 1.0f, m_objPlayerDir.transform.right);
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(-m_objPlayerDir.transform.forward, delta);
                if (!m_bRotStart)
                    RotaeProcess(-m_objPlayerDir.transform.forward, delta, -1.0f, m_objPlayerDir.transform.right);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(-m_objPlayerDir.transform.right, delta);
                if (!m_bRotStart)
                    RotaeProcess(-m_objPlayerDir.transform.right, delta, 1.0f, m_objPlayerDir.transform.forward);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
            {
                TransformHero(m_objPlayerDir.transform.right, delta);
                if (!m_bRotStart)
                    RotaeProcess(m_objPlayerDir.transform.right, delta, 1.0f, -m_objPlayerDir.transform.forward);
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(((m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized, delta, 1.0f, (m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
            {
                TransformHero(((m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta, 1.0f, (-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
            {
                TransformHero(((-m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized), delta);
                if (!m_bRotStart)
                    RotaeProcess((-m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized, delta, 1.0f, (m_objPlayerDir.transform.forward - m_objPlayerDir.transform.right).normalized);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
            {
                TransformHero((-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta);
                if (!m_bRotStart)
                    RotaeProcess((-m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized, delta, -1.0f, (m_objPlayerDir.transform.forward + m_objPlayerDir.transform.right).normalized);
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

                if (dropitem.GetItem().itemtype == ITemType.Potion)
                {
                    switch (dropitem.GetItem().use) 
                    {
                        case USE.HP:
                            m_nHP += dropitem.GetItem().itemCount;
                            if (m_nHP > m_nMaxHP)
                                m_nHP = m_nMaxHP;
                            break;
                        case USE.STAMINA:
                            m_fStamina += dropitem.GetItem().itemCount;
                            if (m_fStamina > m_fMaxStamina)
                                m_fStamina = m_fMaxStamina;
                            break;
                        case USE.FEVER:
                            m_fFever += dropitem.GetItem().itemCount;
                            if (m_fFever > m_fMaxFever)
                                m_fFever = m_fMaxFever;
                            break;
                        case USE.TSTOP:

                            break;
                        case USE.INVINCIBLE:
                            StartCoroutine(Invincible(5f));
                            break;
                    }
                }
                else
                {
                    items.Add(dropitem.GetItem());
                }
                dropitem.Death();
            }
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
        if (other.tag == "Jump")
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jump" || other.tag == "Jump2")
        {
            m_Jump_curDelay = 0.0f;
        }
    }
    public override IEnumerator Die()
    {
        m_bDie = true;
        m_Animator.SetTrigger("tDie");
        m_Animator.SetBool("bDie", m_bDie);
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
        m_nHP = m_nMaxHP;


        m_objCharacter.SetActive(true);
        m_bDie = false;
        m_Animator.SetBool("bDie", m_bDie);
    }
    IEnumerator RevivalEffect()
    {
        m_ptsRevival.gameObject.SetActive(true);
        m_ptsRevival.Play();
        yield return new WaitForSeconds(3);
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

    void RotaeProcess(Vector3 m_objPlayerDir, float delta, float movedir, Vector3 dir) //로테이션
    {
        float dot = Vector3.Dot(m_objPlayerDir, this.transform.forward);
        if (dot == 1.0f)
        {
            //this.transform.Translate(this.transform.forward * delta, Space.World);
        }
        else
        {
            float dot1 = Vector3.Dot(dir, this.transform.forward);
            float rdelta = m_fRotate_Speed * Time.deltaTime;
            float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

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
    }
    #region SearchTarget
    protected void SearchTarget()
    {
        float Shortdist = 10;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer);
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
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
    #region Lookenemy
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
            Vector3 dir = m_tfResultTarget.position - this.transform.position;
            Vector3 Bot = new Vector3(dir.x, 0, dir.z);
            Bot.Normalize();
            float r = Mathf.Acos(Vector3.Dot(Bot, this.transform.forward)); //라디안
            float angle = 180.0f * r / Mathf.PI; //this
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
    #region SerchTargetEffect
    protected void SearchTargetEffect()
    {
        float Shortdist = 10;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer);
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
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
        if (m_tfEfResultTarget == null)
        {
            m_objTargetEffect.Stop();
        }
        else
        {
            m_objTargetEffect.gameObject.transform.position = m_tfEfResultTarget.transform.position;
            m_objTargetEffect.Play();
        }
    }
    #endregion
    IEnumerator Jump(Transform destination)
    {
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
    IEnumerator Invincible(float time)
    {
        this.gameObject.layer = 9;
        yield return new WaitForSeconds(time);
        this.gameObject.layer = 7;
    }
}