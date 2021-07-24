using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform BulletPos; // 프리팹 생성 위치
    public GameObject Bullet;   // 담을 프리팹
    public Animator myAnimator;
    float fRange = 0.0f;
    float fATK = 0.0f;

    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
        this.GetComponentInParent<BearAnimationEvent>().OnShoot = Shoot;
        this.GetComponentInParent<BearAnimationEvent>().OnSkillShoot = SkillShoot;
    }
    public void Shoot()
    {
        // ---------------------------------------- 총알발사 ----------------------------------------
        //myAnimator.SetTrigger("tBAttack");
        GameObject InstantBullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation); // 총알을 인스턴스화 한다
        Rigidbody BulletRigid = InstantBullet.GetComponent<Rigidbody>();                        // 인스턴스된 총알의 리지드바디 갖고 온다
        //BulletRigid.velocity = BulletPos.forward * 10;                                          // 총알이 생성되면서 속도 50이 붙는다
        //GetComponentInParent<Bear>().SetRotStart(false);
    }

    public void SkillShoot()
    {
        //myAnimator.SetTrigger("tBAttack");

    }

    public void SetRange(float f) { fRange = f; }
    public void SetATK(float f) { fATK = f; }
}