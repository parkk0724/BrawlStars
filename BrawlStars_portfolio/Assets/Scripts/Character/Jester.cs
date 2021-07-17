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
    UnityEngine.Coroutine m_cAttack = null;
    protected override void Start()
    {
        base.Start();
        anim = GetComponentsInChildren<Animator>();
    }
    public override void Attack()
    {
        if (m_tfResultTarget != null) m_bCheckStart = true;
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
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(coBasicAttack());
                    m_AttackState = AttackState.BASIC;
                }
                else if (Input.GetMouseButtonDown(1)) m_AttackState = AttackState.SKILL;
                break;
            case AttackState.BASIC:
                //BasicAttack();
                if (m_cAttack != null) StopCoroutine(m_cAttack);
                m_cAttack = StartCoroutine(coBasicAttack());
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
            Vector3 Bojung = m_tfResultTarget.position - m_objBulletPos[0].position;
            Vector3 yBojung = new Vector3(Bojung.x, 0, Bojung.z);
            yBojung.Normalize();
            Vector3 Bojung_1 = m_tfResultTarget.position - m_objBulletPos[1].position;
            Vector3 yBojung_1 = new Vector3(Bojung.x, 0, Bojung.z);
            yBojung.Normalize();
            Vector3 Bojung_2 = m_tfResultTarget.position - m_objBulletPos[2].position;
            Vector3 yBojung_2 = new Vector3(Bojung.x, 0, Bojung.z);
            yBojung.Normalize();
            yBojung_1.Normalize();
            yBojung_2.Normalize();

            yield return new WaitForSeconds(0.15f);
            GameObject instantBullet = Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_objBulletPos[0].forward * 30f;
            //bulletRigid.velocity = yBojung * 30f;
            yield return new WaitForSeconds(0.05f);
            GameObject instantBullet_1 = Instantiate(m_objbullet, m_objBulletPos[1].position, m_objBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_objBulletPos[1].forward * 30f;
            //bulletRigid_1.velocity = yBojung_1 * 30f;
            yield return new WaitForSeconds(0.05f);
            GameObject instantBullet_2 = Instantiate(m_objbullet, m_objBulletPos[2].position, m_objBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_objBulletPos[2].forward * 30f;
            //bulletRigid_2.velocity = yBojung_2 * 30f;
        }
        if (!m_bCheckStart)
        {

            GameObject instantBullet = Instantiate(m_objbullet, m_objBulletPos[0].position, m_objBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_objBulletPos[0].forward * 30f;
            yield return new WaitForSeconds(0.05f);
            GameObject instantBullet_1 = Instantiate(m_objbullet, m_objBulletPos[1].position, m_objBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_objBulletPos[1].forward * 30f;
            yield return new WaitForSeconds(0.05f);
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
