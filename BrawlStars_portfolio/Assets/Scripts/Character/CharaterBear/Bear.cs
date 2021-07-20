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
        myWeapon = GetComponentInChildren<Weapon>(); // �ٸ� ��ũ��Ʈ ����
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myWeapon.Shot();
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
    } 
}
/*
    GetComponentInChildren<Weapon>().Shot();
    210720 �۾�
         ������Ʈ �����ϰ� ����� �� üũ
         �ڵ� ����ȭ �۾�  
*/