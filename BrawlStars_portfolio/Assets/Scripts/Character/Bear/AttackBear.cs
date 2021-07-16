using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBear : Hero
{
    public GameObject BulletPrepab;

    protected override void Start()
    {
        m_Animator = this.GetComponent<Animator>();
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject myInstance = Instantiate(BulletPrepab);
            myInstance.transform.position = this.transform.position;
        }
    }

    public override void SkillAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("GetMouseButtonDown(1)");
            m_Animator.SetTrigger("tBAttack");
        }
    }
}
