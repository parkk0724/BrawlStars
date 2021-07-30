using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMan : Hero
{
    enum AttackState { NONE, BASIC, SKILL };
    // Start is called before the first frame update
    AttackState m_AttackState = AttackState.NONE;
    BoxManWeapon m_BoxManWeapon;
    [SerializeField] GameObject m_objDirBasicAttack = null;
    [SerializeField] GameObject m_objDirSkillAttack = null;
    [SerializeField] float m_fMaxMouseButton = 0.0f;
    float m_fCurMouseButton = 0.0f;
    float m_fAttackStamina = 0.0f;
    
    protected override void Start()
    {
        base.Start();
        m_BoxManWeapon = GetComponentInChildren<BoxManWeapon>();
        m_BoxManWeapon.SetRange(m_fRange = 10.0f);
        m_BoxManWeapon.OnFeverUp = FeverUp;
        m_BoxManWeapon.SetATK(m_nATK);
        m_fMaxMouseButton = 0.3f;
        m_fAttackStamina = 1.0f;
    }

    protected override void Update()
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

        if (m_bRotStart) LookEnemy();
        if (!m_bDie && m_nHP <= 0) StartCoroutine(Die());
    }
    public override void Attack()
    {
        switch (m_AttackState)
        {
            case AttackState.NONE:
                if (Input.GetMouseButtonDown(0))
                {
                    m_AttackState = AttackState.BASIC;
                    SearchTarget();
                    m_bRotStart = true;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    m_AttackState = AttackState.SKILL;
                    SearchTarget();
                    m_bRotStart = true;
                }
                break;
            case AttackState.BASIC:
                BasicAttack();
                break;
            case AttackState.SKILL:
                SkillAttack();
                break;
        }
    }
    void BasicAttack()
    {
        if (Input.GetMouseButton(0))
        {
            CheckPressAttack(m_objDirBasicAttack);
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckUpAttack(m_objDirBasicAttack);

            if (m_fStamina > m_fAttackStamina && this.transform.position.y < 0.1f)
            {
                m_Animator.SetTrigger("tBAttack");
                m_fStamina -= m_fAttackStamina;
            }
        }
    }

    public override void SkillAttack()
    {
        if (Input.GetMouseButton(1))
        {
            CheckPressAttack(m_objDirSkillAttack);
        }
        if (Input.GetMouseButtonUp(1))
        {
            CheckUpAttack(m_objDirSkillAttack);

            if (m_fFever >= m_fMaxFever && this.transform.position.y < 1.0f)
            {
                m_Animator.SetTrigger("tSAttack");
                m_fFever = 0.0f;           
            }
        }
    }
    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
    }

    private void CheckPressAttack(GameObject objDir)
    {
        m_fCurMouseButton += Time.deltaTime;
        if (m_fCurMouseButton > m_fMaxMouseButton)
        {
            if (!objDir.activeSelf)
            {
                objDir.SetActive(true);
                m_tfResultTarget = null;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f, m_lmPicking_Mask))
            {
                Vector3 look = hit.point;
                look.y = this.transform.position.y;
                this.transform.LookAt(look);
            }
        }
    }

    private void CheckUpAttack(GameObject objDir)
    {
        m_AttackState = AttackState.NONE;
        objDir.SetActive(false);
        m_fCurMouseButton = 0.0f;
    }
}
