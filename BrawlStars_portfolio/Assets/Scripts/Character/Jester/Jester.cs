using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : Hero
{
    // Start is called before the first frame update
    enum AttackState
    { NONE, BASIC, SKILL }
    AttackState m_AttackState = AttackState.NONE;
    public ParticleSystem m_ptsBoom;
    Animator anim;
    //UnityEngine.Coroutine j_Attack = null;
    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }
    public override void Attack()
    {
        switch (m_AttackState)
        {
            case AttackState.NONE:
                if (Input.GetMouseButtonDown(0)) m_AttackState = AttackState.BASIC;
                else if (Input.GetMouseButtonDown(1)) m_AttackState = AttackState.SKILL;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1000.0f, m_lmPicking_Mask))
        {
            this.transform.LookAt(hit.point);
            anim.SetTrigger("tBAttack");
        }
    }
    #region coBasicAttck_first
    /*IEnumerator coBasicAttack()
    {

        //Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
        if (m_tfResultTarget == null) //Ÿ���� ������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f, m_lmPicking_Mask))
            {
                if (m_fFireReady)
                {
                    this.transform.LookAt(hit.point);
                    anim[1].SetTrigger("doShoot");
                    m_Animator.SetTrigger("tBAttack");
                    m_fCurfireTime = 0;
                    m_bMoveStart = false;
                    m_bRotStart = true;
                    StartCoroutine(BulletInit());

                }
            }
        }
        if (m_tfResultTarget != null) //Ÿ���� ������
        {
            if (m_fFireReady && m_bCheckStart)
            {
                anim[1].SetTrigger("doShoot");
                m_Animator.SetTrigger("tBAttack");
                m_fCurfireTime = 0;
                m_bMoveStart = false;
                m_bRotStart = true;
                StartCoroutine(BulletInit());
                m_bCheckStart = false;

            }
            yield return null;
        }
    }*/
    #endregion
    #region
    /*IEnumerator BulletInit()
    {
        if (m_bCheckStart)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet = Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_tBulletPos[0].forward * 30f;
            BullutCaseInit(2);
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_1 = Instantiate(m_objbullet, m_tBulletPos[1].position, m_tBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_tBulletPos[1].forward * 30f;
            BullutCaseInit(2);

            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_2 = Instantiate(m_objbullet, m_tBulletPos[2].position, m_tBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_tBulletPos[2].forward * 30f;
            BullutCaseInit(2);
        }
        if (!m_bCheckStart)
        {

            GameObject instantBullet = Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_tBulletPos[0].forward * 30f;
            BullutCaseInit(2);
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_1 = Instantiate(m_objbullet, m_tBulletPos[1].position, m_tBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_tBulletPos[1].forward * 30f;
            BullutCaseInit(2);
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_2 = Instantiate(m_objbullet, m_tBulletPos[2].position, m_tBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_tBulletPos[2].forward * 30f;
            BullutCaseInit(2);

        }

    }*/
    #endregion
    public override void SkillAttack()
    {

    }
    // Update is called once per frame
    public void SetRot_flase(bool b)
    {
        m_bRotStart = b;
    }
}
