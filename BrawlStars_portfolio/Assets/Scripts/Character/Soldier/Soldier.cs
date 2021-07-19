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

    Animation_Event animation_event;
    protected override void Start()
    {
        base.Start();
        this.GetComponent<Animation_Event>().bazooka_basic_fire = Basic_Fire;
        this.GetComponent<Animation_Event>().bazooka_skill_fire = Skill_Fire;
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

    private void Basic_Fire()
    {
        Debug.Log("ff");
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
