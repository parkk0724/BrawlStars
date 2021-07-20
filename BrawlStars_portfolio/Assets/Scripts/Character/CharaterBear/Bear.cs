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
        myWeapon = GetComponentInChildren<Weapon>(); // 다른 스크립트 저장
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
    210720 작업
         오브젝트 공격하게 만드는 것 체크
         코드 단일화 작업  
*/