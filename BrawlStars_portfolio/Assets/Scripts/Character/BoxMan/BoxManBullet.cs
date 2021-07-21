using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxManBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform m_tfRotChild = null;
    [SerializeField] GameObject m_objHitMonsterEffect = null;
    [SerializeField] GameObject m_objHitObstacleEffect = null;

    TrailRenderer m_trailRenderer;
    Vector3 m_vOriPos;
    Transform m_tfHero = null;
    CapsuleCollider m_Collider;
    public UnityAction OnFeverUp = null;
    float m_fMoveSpeed = 0.0f;
    float m_fRotSpeed = 0.0f;
    float m_fDistance = 0.0f;
    float m_fSkillSize = 0.0f;
    float m_fSkillMaxStay = 0.0f;
    float m_fSkillStay = 0.0f;
    float m_fSkillAttackDelay = 0.0f;
    float m_fCurSkillAttack = 0.0f;
    float m_fDamage = 0.0f;
    
    bool m_bTurn = false;
    bool m_bSkill = false;

    public void SetDamage(float f) { m_fDamage = f; }
    public void SetDistance(float f) { m_fDistance = f; }
    public void SetPosParent(Transform pos) { m_tfHero = pos; }
    public void OnSkill() { m_bSkill = true; }
    
    void Start()
    {
        m_trailRenderer = this.GetComponent<TrailRenderer>();
        m_Collider = this.GetComponent<CapsuleCollider>();
        m_fMoveSpeed = 10f;
        m_fRotSpeed = 1000f;
        m_fSkillSize = 5.0f;
        m_fSkillMaxStay = 3.0f;
        m_fSkillAttackDelay = 0.2f;
        m_fSkillStay = m_fSkillMaxStay;
        m_vOriPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSkill)
        {
            Skill();
            m_fCurSkillAttack += Time.deltaTime;
        }
        else
        {
            Basic();
        }

        m_tfRotChild.Rotate(m_tfRotChild.up * Time.deltaTime * m_fRotSpeed, Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            if (!m_bSkill)
            {
                OnFeverUp?.Invoke();
                other.GetComponent<Monster>()?.Hit((int)m_fDamage, new Color(0, 0, 0, 1));
                CreateHitEffect(true, other.transform);
                if(!m_bTurn) m_bTurn = true;
                else Destroy(this.gameObject);
            }
        }
        else if (other.tag == "Obstacle" || other.tag == "Wall" || other.tag == "Player")
        {
            if (other.tag != "Player") CreateHitEffect(false, other.transform);

            if (m_bTurn)
            {
                if (m_fSkillStay >= m_fSkillMaxStay) Destroy(this.gameObject);
            }
            else
            {
                m_bTurn = true;
                if (m_bSkill) m_fSkillStay = 0.0f;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster" && m_bSkill)
        {
            if (m_fCurSkillAttack >= m_fSkillAttackDelay)
            {

                OnFeverUp?.Invoke();
                CreateHitEffect(true, other.transform);
                m_fCurSkillAttack = 0.0f;
            }
        }
        else if(other.tag == "Obstacle" || other.tag == "Wall")
        {
            if (m_fCurSkillAttack >= m_fSkillAttackDelay)
            {
                CreateHitEffect(false, other.transform);
                m_fCurSkillAttack = 0.0f;
            }
        }
    }
    void Basic()
    {
        if (m_bTurn)
        {
            Vector3 dir = (m_tfHero.position - this.transform.position).normalized;
            dir.y = 0;
            this.transform.position += dir * Time.deltaTime * m_fMoveSpeed;
        }
        else
        {
            this.transform.position += this.transform.forward * Time.deltaTime * m_fMoveSpeed;
            float d = Vector3.Distance(m_vOriPos, this.transform.position);
            if (m_fDistance < d) m_bTurn = true;
        }
    }

    void Skill()
    {
        if(m_fSkillStay < m_fSkillMaxStay)
        {
            m_fSkillStay += Time.deltaTime;
        }
        else if (m_bTurn)
        {
            if(m_tfRotChild.localScale.x > 1.0f)
            {
                m_tfRotChild.localScale -= Vector3.one * Time.deltaTime * m_fSkillSize;
                m_Collider.radius -= (Time.deltaTime * 1.5f);
                m_trailRenderer.startWidth -= Time.deltaTime * m_fSkillSize;
                m_trailRenderer.time -= Time.deltaTime / m_fSkillSize;
            }

            Vector3 dir = (m_tfHero.position - this.transform.position).normalized;
            dir.y = 0;
            this.transform.position += dir * Time.deltaTime * m_fMoveSpeed;
        }
        else
        {
            m_tfRotChild.localScale += Vector3.one * Time.deltaTime * m_fSkillSize;
            m_Collider.radius += (Time.deltaTime * 1.5f);
            m_trailRenderer.startWidth += Time.deltaTime * m_fSkillSize;
            m_trailRenderer.time += Time.deltaTime / m_fSkillSize;

            this.transform.position += this.transform.forward * Time.deltaTime * m_fMoveSpeed;

            float d = Vector3.Distance(m_vOriPos, this.transform.position);
            if (m_fDistance < d)
            {
                m_bTurn = true;
                m_fSkillStay = 0.0f;
            }
        }
    }

    void CreateHitEffect(bool isMonster, Transform other)
    {
        Vector3 dir = (other.position - this.transform.position).normalized;
        dir *= m_Collider.radius;

        if (isMonster) Instantiate(m_objHitMonsterEffect, this.transform.position + dir, this.transform.rotation);
        else Instantiate(m_objHitObstacleEffect, this.transform.position + dir, this.transform.rotation);
    }
}
