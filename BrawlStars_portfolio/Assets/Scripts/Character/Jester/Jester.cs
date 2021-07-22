using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : Hero
{
    // Start is called before the first frame update
    enum AttackState
    { NONE, BASIC, SKILL }
    AttackState m_AttackState = AttackState.NONE;
    public float m_fCurMouseButton = 0;
    public float m_fMaxMouseButton = 0;
    public GameObject m_objDirSkillAttack = null;
    public GameObject m_objtsBoom = null;
    public GameObject m_objJesterSkill = null;
    UnityEngine.Coroutine skill = null;
    //UnityEngine.Coroutine j_Attack = null;
    protected override void Start()
    {
        //m_ptsBoom = GetComponent<ParticleSystem>();
        base.Start();
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
        if (Input.GetMouseButton(0))
        {
            m_fCurMouseButton += Time.deltaTime;
            if (m_fCurMouseButton > m_fMaxMouseButton)
            {
                m_tfResultTarget = null;
                //if (!m_objDirBasicAttack.activeSelf)
                //{
                //    m_objDirBasicAttack.SetActive(true);
                //    
                //}
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, m_lmPicking_Mask))
                {
                    this.transform.LookAt(hit.point);
                  
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_AttackState = AttackState.NONE;
            //m_objDirBasicAttack.SetActive(false);
            m_fCurMouseButton = 0.0f;
            
                m_Animator.SetTrigger("tBAttack");
            
           
            //if (m_fStamina > m_fAttackStamina)
            //{
            //  
            //    m_fStamina -= m_fAttackStamina;
            //}
        }

    }
    #region coBasicAttck_first
    /*IEnumerator coBasicAttack()
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
        if (Input.GetMouseButton(1))
        {
            m_fCurMouseButton += Time.deltaTime;
            if (m_fCurMouseButton > m_fMaxMouseButton)
            {
                m_tfResultTarget = null;
                if (!m_objDirSkillAttack.activeSelf)
                {
                    m_objDirSkillAttack.SetActive(true);
                  
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, m_lmPicking_Mask))
                {
                    m_objDirSkillAttack.transform.position = new Vector3(hit.point.x, 4, hit.point.z);
                    this.transform.LookAt(hit.point);
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (m_objDirSkillAttack.activeSelf)
            {
                m_objtsBoom.gameObject.transform.position = m_objDirSkillAttack.transform.position;
                Vector3 newPos = new Vector3(m_objtsBoom.gameObject.transform.position.x + Random.Range(-1,1), m_objtsBoom.gameObject.transform.position.y, m_objtsBoom.gameObject.transform.position.z + Random.Range(-1,1));
                Vector3 newPos_1 = new Vector3(m_objtsBoom.gameObject.transform.position.x + Random.Range(-1,1), m_objtsBoom.gameObject.transform.position.y, m_objtsBoom.gameObject.transform.position.z + Random.Range(-1, 1));
                if (skill != null) StopCoroutine(skill);
                skill = StartCoroutine(Effect());
                Instantiate(m_objJesterSkill, newPos, Quaternion.identity);
                Instantiate(m_objJesterSkill, newPos_1, Quaternion.identity);
            }
            m_Animator.SetTrigger("tSAttack");
            m_AttackState = AttackState.NONE;
            m_objDirSkillAttack.SetActive(false);
            m_fCurMouseButton = 0.0f;


            //if (m_fFever >= m_fMaxFever)
            //{
            //    
            //    m_fFever = 0.0f;
            //}
        }
    }
    // Update is called once per frame
    public void SetRot_flase(bool b)
    {
        m_bRotStart = b;
    }
    IEnumerator Effect()
    {
        GameObject obs = Instantiate(m_objtsBoom.gameObject, m_objDirSkillAttack.transform.position, m_objDirSkillAttack.transform.rotation);
        yield return new WaitForSeconds(2);
        Destroy(obs);
    }
}
