using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxManWeapon : MonoBehaviour
{
    [SerializeField] GameObject m_objBullet = null;
    [SerializeField] GameObject m_objShootPos = null;
    Animator m_Animator = null;
    Transform m_tfHero = null;
    float m_fRange = 0.0f;
    float m_fATK = 0.0f;
    public UnityAction OnFeverUp = null;

    public void SetATK(float f) { m_fATK = f; }
    public void SetRange(float f) { m_fRange = f; }
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        this.GetComponentInParent<Animation_Event>().OnShoot = Shoot;
        this.GetComponentInParent<Animation_Event>().OnSkillShoot = SkillShoot;
        m_tfHero = GetComponentInParent<BoxMan>().transform;
    }
    void Shoot()
    {
        m_Animator.SetTrigger("Shoot");
        GameObject obj = Instantiate(m_objBullet, m_objShootPos.transform.position, m_objShootPos.transform.rotation);
        BoxManBullet bullet = obj.GetComponent<BoxManBullet>();
        bullet.SetDistance(m_fRange - Vector3.Distance(this.transform.position, m_tfHero.position));
        bullet.SetPosParent(m_tfHero);
        bullet.OnFeverUp = OnFeverUp;
        bullet.SetDamage(m_fATK);

        GetComponentInParent<BoxMan>().SetRotStart(false);
    }

    void SkillShoot()
    {
        m_Animator.SetTrigger("Shoot");
        GameObject obj = Instantiate(m_objBullet, m_objShootPos.transform.position, m_objShootPos.transform.rotation);
        BoxManBullet bullet = obj.GetComponent<BoxManBullet>();
        bullet.SetDistance(m_fRange - Vector3.Distance(this.transform.position, m_tfHero.position));
        bullet.SetPosParent(m_tfHero);
        bullet.OnSkill();
        bullet.OnFeverUp = OnFeverUp;
        bullet.SetDamage(m_fATK);

        GetComponentInParent<BoxMan>().SetRotStart(false);
    }
}
