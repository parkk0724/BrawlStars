using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform BulletPos; // ������ ���� ��ġ
    public GameObject Bullet;   // ���� ������
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
        // ---------------------------------------- �Ѿ˹߻� ----------------------------------------
        //myAnimator.SetTrigger("tBAttack");
        GameObject InstantBullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation); // �Ѿ��� �ν��Ͻ�ȭ �Ѵ�
        Rigidbody BulletRigid = InstantBullet.GetComponent<Rigidbody>();                        // �ν��Ͻ��� �Ѿ��� ������ٵ� ���� �´�
        //BulletRigid.velocity = BulletPos.forward * 10;                                          // �Ѿ��� �����Ǹ鼭 �ӵ� 50�� �ٴ´�
        //GetComponentInParent<Bear>().SetRotStart(false);
    }

    public void SkillShoot()
    {
        //myAnimator.SetTrigger("tBAttack");

    }

    public void SetRange(float f) { fRange = f; }
    public void SetATK(float f) { fATK = f; }
}