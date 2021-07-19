using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Hero
{
    public GameObject bazooka_Basic_bullet;
    public GameObject bazooka_Skill_bullet1;
    public GameObject bazooka_Skill_bullet2;
    public GameObject bazooka_Skill_bullet3;
    public GameObject bazooka_Skill_bullet4;
    public Transform bazooka_bullet_pos;

    Coroutine skillbullet = null;
    protected override void Start()
    {
        base.Start();
        this.GetComponent<Animation_Event>().bazooka_basic_fire += () => Bazooka_Basic_Fire();
        this.GetComponent<Animation_Event>().bazooka_skill_fire += () => Bazooka_Skill_Fire();
    }
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Animator.SetTrigger("tBAttack");            
        }
        if (Input.GetMouseButtonDown(1))
        {
            m_Animator.SetTrigger("tSAttack");           
        }
    }

    private void Bazooka_Basic_Fire()
    {
        Bazooka_Bullet_Initiate(bazooka_Basic_bullet);
    }
    private void Bazooka_Skill_Fire()
    {
        skillbullet = StartCoroutine(Bazooka_SkillBullet_Initiate(0.5f));
        if (skillbullet == null) StopCoroutine(skillbullet);
    }

    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
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
        skillbullet = null;
    }
}
