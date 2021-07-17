using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : Hero
{
    // Start is called before the first frame update
    enum AttackState
    { NONE, BASIC, SKILL }
    AttackState m_AttackState = AttackState.NONE;
    [SerializeField] bool m_fFireReady;
    [SerializeField] float m_fFireRate;
    [SerializeField] float m_fCurfireTime;
    [SerializeField] Transform[] m_objBulletPos;
    [SerializeField] GameObject m_objbullet;
    Animator[] anim;
    UnityEngine.Coroutine rotate = null;
    protected override void Start()
    {
        base.Start();
        anim = GetComponentsInChildren<Animator>();
    }
    public override void Attack()
    {
        m_fCurfireTime += Time.deltaTime;
        m_fFireReady = m_fFireRate < m_fCurfireTime;
        if (m_fFireReady)
        {
            m_bMoveStart = true;
            m_bRotStart = false;
        }
        switch (m_AttackState)
        {
            case AttackState.NONE:
                if (Input.GetMouseButtonDown(0)) m_AttackState = AttackState.BASIC;
                else if (Input.GetMouseButtonDown(1)) m_AttackState = AttackState.SKILL;
                break;
            case AttackState.BASIC:
                //BasicAttack();
                if (rotate != null) StopCoroutine(rotate);
                rotate = StartCoroutine(coBasicAttack());
                break;
            case AttackState.SKILL:
                SkillAttack();
                break;
        }
    }
    IEnumerator coBasicAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
            if (m_tfResultTarget == null) //타겟이 없을떄
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
            if (m_tfResultTarget != null) //타겟이 있을때
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
                    //if (!m_fFireReady)
                    //{
                    //    m_bMoveStart = true;
                    //    m_bRotStart = false;
                    //}

                }
                yield return null;
            }
        }
    }
    IEnumerator BulletInit()
    {
        if (m_bCheckStart)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet = Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_objBulletPos[0].forward * 30f;
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet_1 = Instantiate(m_objbullet, m_objBulletPos[1].position, m_objBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_objBulletPos[1].forward * 30f;
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet_2 = Instantiate(m_objbullet, m_objBulletPos[2].position, m_objBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_objBulletPos[2].forward * 30f;
        }
        if (!m_bCheckStart)
        {

            GameObject instantBullet = Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_objBulletPos[0].forward * 30f;
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet_1 = Instantiate(m_objbullet, m_objBulletPos[1].position, m_objBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_objBulletPos[1].forward * 30f;
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet_2 = Instantiate(m_objbullet, m_objBulletPos[2].position, m_objBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_objBulletPos[2].forward * 30f;
        }

    }
    void BasicAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_tfResultTarget != null)
            {
                m_bMoveStart = false;
                anim[1].SetTrigger("doShoot");
                m_Animator.SetTrigger("tBAttack");
                m_fCurfireTime = 0;

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (m_tfResultTarget == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, m_lmPicking_Mask))
                {
                    if (m_fFireReady)
                    {
                        this.transform.LookAt(hit.point);
                    }
                }
                m_bMoveStart = false;
                //m_bRotStart = true;
                anim[1].SetTrigger("doShoot");
                m_Animator.SetTrigger("tBAttack");
                m_fCurfireTime = 0;

            }
        }
    }
    public override void SkillAttack()
    {

    }
    // Update is called once per frame

}
