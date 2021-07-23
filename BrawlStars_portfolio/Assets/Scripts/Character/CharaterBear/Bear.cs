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
        Delegate 공부

            쉽게말하면 함수를 보관하는 통을 만들고(대리자선언) 그 통안에 함수를 넣고
            나중에 통을 가져와서 함수를 실행시키는 방식입니다.
            
            delegate void Del(); // 대리자 생성
                                 // 매개변수 없고, void 메서드만 참조 가능

            Del myDel       // myDel 대리자 객체 생성
            myDel = Print; // Print 메서드 참조
            


        코드 단일화 작업
*/