using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Hero
{
    public LayerMask pickingmask;
    public GameObject bazooka_Basic_bullet;
    public GameObject bazooka_Skill_bullet1;
    public GameObject bazooka_Skill_bullet2;
    public GameObject bazooka_Skill_bullet3;
    public GameObject bazooka_Skill_bullet4;
    public Transform bazooka_bullet_pos;

    public float Jump_Speed = 0.0f;
    public float Jump_Height = 0.0f;

    Animation_Event animation_event;
    protected override void Start()
    {
        base.Start();
        this.GetComponent<Animation_Event>().bazooka_basic_fire = Basic_Fire;
        this.GetComponent<Animation_Event>().bazooka_skill_fire = Skill_Fire;
    }
    public override void Attack()
    {
        if (m_fStamina >= 1.0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_fStamina -= 1.0f; 
                m_Animator.SetTrigger("tBAttack");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetRotStart(false);
        }
        

        if (Input.GetMouseButton(1))
        {
            m_Animator.SetTrigger("tSAttack");
            SetRotStart(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            SetRotStart(false);
        }
    }

    private void Basic_Fire()
    {
        Bazooka_Bullet_Initiate(bazooka_Basic_bullet);
    }
    private void Skill_Fire()
    {
        StartCoroutine(Bazooka_SkillBullet_Initiate(0.5f));
    }
    void Bazooka_Bullet_Initiate(GameObject bullet)
    {
        GameObject skillbullet = Instantiate(bullet, bazooka_bullet_pos.position, bazooka_bullet_pos.rotation);
    }

    IEnumerator Bazooka_SkillBullet_Initiate(float t)
    {
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet1);
        yield return new WaitForSeconds(t / 3.0f);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet4);
        yield return new WaitForSeconds(t);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet2);
        yield return new WaitForSeconds(t / 3.0f);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet3);
        yield return null;
    }
    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
    }
}
