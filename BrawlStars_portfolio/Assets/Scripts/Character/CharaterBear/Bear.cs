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
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }

        if (Input.GetMouseButtonDown(1))
        {
        }
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