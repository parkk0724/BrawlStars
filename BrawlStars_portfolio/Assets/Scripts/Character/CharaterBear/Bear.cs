using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Hero
{
    public Animator myAnimator;
    public Weapon myWeapon;
    float m_fAttackStamina = 0.0f;

    protected override void Start()
    {
        base.Start();
        myWeapon = GetComponentInChildren<Weapon>();
        myWeapon.SetRange(m_fRange = 10.0f);
        myWeapon.SetATK(m_nATK);
        m_fAttackStamina = 1.0f;
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0) && m_fStamina > m_fAttackStamina) 
        { 
           myAnimator.SetTrigger("tBAttack");
           m_fStamina -= m_fAttackStamina;
            m_bRotStart = true;
            //myWeapon.Shoot();
        }
        if (Input.GetMouseButtonDown(1) && m_fFever >= m_fMaxFever)
        {
            myAnimator.SetTrigger("tSAttack");
            m_fFever = 0;
            //myWeapon.SkillShoot();
        }
        
    }

    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
    }
}

/*
    GetComponentInChildren<Weapon>().Shot();
    210721
        Delegate ����

            ���Ը��ϸ� �Լ��� �����ϴ� ���� �����(�븮�ڼ���) �� ��ȿ� �Լ��� �ְ�
            ���߿� ���� �����ͼ� �Լ��� �����Ű�� ����Դϴ�.
            
            delegate void Del(); // �븮�� ����
                                 // �Ű����� ����, void �޼��常 ���� ����

            Del myDel       // myDel �븮�� ��ü ����
            myDel = Print; // Print �޼��� ����
            


        �ڵ� ����ȭ �۾�
*/