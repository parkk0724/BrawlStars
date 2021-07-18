using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : Character
{
    [Header("Moving")]
    public LayerMask m_lmPicking_Mask;
    public float m_fMove_Speed = 5.0f;
    public float m_fRotate_Speed = 0.0f;
    public ParticleSystem m_ptsRevival;
    public GameObject m_objPlayerDir;
    public GameObject m_objCharacter;
    public GameObject m_objUIDie;
    public Text m_tDie;

    [Header("Target")]
    public float m_fTargetRotSpeed = 10.0f;
    public GameObject m_objTargetEffect;
    public Transform m_tfResultTarget;
    public bool m_bRotStart = false;
    public bool m_bMoveStart;
    public bool m_bCheckStart = false;
    public float m_fTargetRange = 0f;
    public LayerMask m_lmEnemyLayer = 0;
    protected override void Start()
    {
        m_bMoveStart = true;
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;   // Current Hp
        m_fMaxStamina = 3.0f;
        m_fStamina = m_fMaxStamina;
        m_fMaxFever = 10.0f;
        m_fFever = m_fMaxFever;
        m_nATK = 10;
        m_nDEF = 5;
        m_nSkillDamage = 20;
        m_fMoveSpeed = 5.0f;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;

        // UI: HP, Max
        //HealthBar.SetHealth(m_nMaxHP);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            m_bRotStart = true;
            SearchTarget();
        }
        if (m_bRotStart) LookatEnemy();//LookEnemy(); // 임시로 lookat만들어서 사용
        if (!m_bDie && m_nHP <= 0) StartCoroutine(Die());
    }

    public override void Move()
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
            m_Animator.SetBool("bMove", true);
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


    public override void Attack()
    {
       // if (Input.GetMouseButtonDown(0))
       // {
       //     m_Animator.SetTrigger("tBAttack");
       // }
    }

    public override void SkillAttack()
    {
       // if (Input.GetMouseButtonDown(1))
       // {
       //     m_Animator.SetTrigger("tSAttack");
       // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();

            Destroy(other.gameObject);
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
    public override void Revival()
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
        yield return new WaitForSeconds(3);
        m_ptsRevival.gameObject.SetActive(false);
    }
    void TransformHero(Vector3 m_objPlayerDir, float delta) // 움직임
    {
        if(m_bMoveStart)
        {
            this.transform.Translate(m_objPlayerDir * delta, Space.World);
            m_Animator.SetBool("bMove", true);
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

    void SearchTarget()
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
    void LookatEnemy()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        this.transform.LookAt(m_tfResultTarget);
    }

    void LookEnemy()
    {
        if (m_tfResultTarget == null)
        {
            //m_bRotStart = false;
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
            Instantiate(m_objTargetEffect, m_tfResultTarget.position, Quaternion.Euler(m_tfResultTarget.rotation.x + 90, m_tfResultTarget.rotation.y, m_tfResultTarget.rotation.z));

            if (angle - delta < 0.0f)
            {
                delta = angle;
            }

            this.transform.Rotate(Vector3.up, delta * rotDir);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, m_fRange);
    }
}