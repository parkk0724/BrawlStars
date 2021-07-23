using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Hero
{
    Animator myAnimator;
    public Weapon myWeapon;

    protected override void Start()
    {
        base.Start();
        myWeapon = GetComponentInChildren<Weapon>();
        myWeapon.SetRange(m_fRange = 10.0f);
        myWeapon.SetATK(m_nATK);
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0)) myWeapon.Shoot();
        if (Input.GetMouseButtonDown(1)) myWeapon.SkillShoot();
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