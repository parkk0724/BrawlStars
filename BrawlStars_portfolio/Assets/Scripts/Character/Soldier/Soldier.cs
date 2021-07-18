using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Hero
{
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
    }
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Animator.SetTrigger("tBAttack");
        }
    }

    public override void SkillAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_Animator.SetTrigger("tSAttack");
        }
    }

    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
    }
}
