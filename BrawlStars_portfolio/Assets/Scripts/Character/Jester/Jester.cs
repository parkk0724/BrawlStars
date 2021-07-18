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
    [SerializeField] Transform[] m_tBulletPos;
    [SerializeField] GameObject m_objbullet;
    [SerializeField] Transform m_tBulletPosCase;
    [SerializeField] GameObject m_objBulletCase;
    Animator[] anim;
    UnityEngine.Coroutine j_Attack = null;
    protected override void Start()
    {

        base.Start();
        anim = GetComponentsInChildren<Animator>();
    }
    public override void Attack()
    {
        m_fCurfireTime += Time.deltaTime;
        m_fFireReady = m_fFireRate < m_fCurfireTime;
        if (m_tfResultTarget != null) m_bCheckStart = true;
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

                if (Input.GetMouseButtonDown(0))
                {
                    if (j_Attack != null) StopCoroutine(j_Attack);
                    j_Attack = StartCoroutine(coBasicAttack());
                }

                break;
            case AttackState.SKILL:
                SkillAttack();
                break;
        }
    }
    IEnumerator coBasicAttack()
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

            }
            yield return null;
        }
    }
    IEnumerator BulletInit()
    {
        if (m_bCheckStart)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject instantBullet = Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_tBulletPos[0].forward * 30f;
            BullutCaseInit();
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_1 = Instantiate(m_objbullet, m_tBulletPos[1].position, m_tBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_tBulletPos[1].forward * 30f;
            BullutCaseInit();

            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_2 = Instantiate(m_objbullet, m_tBulletPos[2].position, m_tBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_tBulletPos[2].forward * 30f;
            BullutCaseInit();
        }
        if (!m_bCheckStart)
        {

            GameObject instantBullet = Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = m_tBulletPos[0].forward * 30f;
            BullutCaseInit();
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_1 = Instantiate(m_objbullet, m_tBulletPos[1].position, m_tBulletPos[1].rotation);
            Rigidbody bulletRigid_1 = instantBullet_1.GetComponent<Rigidbody>();
            bulletRigid_1.velocity = m_tBulletPos[1].forward * 30f;
            BullutCaseInit();
            yield return new WaitForSeconds(0.1f);

            GameObject instantBullet_2 = Instantiate(m_objbullet, m_tBulletPos[2].position, m_tBulletPos[2].rotation);
            Rigidbody bulletRigid_2 = instantBullet_2.GetComponent<Rigidbody>();
            bulletRigid_2.velocity = m_tBulletPos[2].forward * 30f;
            BullutCaseInit();

        }

    }
    void BullutCaseInit()
    {
        GameObject instanBulletCase = Instantiate(m_objBulletCase, m_tBulletPosCase.position, m_tBulletPosCase.rotation);
        Rigidbody bulletcaseRigid = instanBulletCase.GetComponent<Rigidbody>();
        Vector3 pos = m_tBulletPosCase.forward * Random.Range(-3, -2) + m_tBulletPosCase.up * Random.Range(2, 5);
        bulletcaseRigid.AddForce(pos, ForceMode.Impulse);
        bulletcaseRigid.AddTorque(Vector3.up * Random.Range(-10, 10));
    }

    public override void SkillAttack()
    {

    }
    // Update is called once per frame

}
