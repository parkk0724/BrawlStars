using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform BulletPos;         // ������ ���� ��ġ
    public GameObject Bullet;           // ���� ������
    public GameObject TurretPrefab;     // �ͷ� ������
    public Animator myAnimator;         // �ִϸ�����
    public BearBullet bearbullet;       // �Ѿ� ������
    private GameObject InstantBullet;   // ������ �ν���Ʈ ���� ��
    private Bear bear;
    float fRange = 0.0f;
    float fATK = 0.0f;

    void Start()
    {
        TurretPrefab = Resources.Load("Prefabs/Turret/Turret.prefab") as GameObject;
        myAnimator = GetComponentInParent<Animator>();
        bear = GetComponentInParent<Bear>();
        this.GetComponentInParent<BearAnimationEvent>().OnShoot = Shoot;
        this.GetComponentInParent<BearAnimationEvent>().OnSkillShoot = SkillShoot;
    }
    public void Shoot()
    {
        // ---------------------------------------- �Ѿ˹߻� ----------------------------------------
        InstantBullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation); // �Ѿ��� �ν��Ͻ�ȭ �Ѵ�
        bearbullet = InstantBullet.GetComponent<BearBullet>();
        bearbullet.OnFeverUp = bear.FeverUp;
        //Rigidbody BulletRigid = InstantBullet.GetComponent<Rigidbody>();                      // �ν��Ͻ��� �Ѿ��� ������ٵ� ���� �´�
        //BulletRigid.velocity = BulletPos.forward * 10;                                        // �Ѿ��� �����Ǹ鼭 �ӵ� 50�� �ٴ´�
        //GetComponentInParent<Bear>().SetRotStart(false);
        GetComponentInParent<Bear>().SetRotStart(false);
    }

    public void SkillShoot()
    {
        //  ------------------------ �ͷ���ȯ ------------------------------
        //myAnimator.SetTrigger("tBAttack");
        GetComponentInParent<Bear>().SetRotStart(false);
        GameObject GmaeObj = Instantiate(TurretPrefab, this.transform.position + this.transform.forward, this.transform.rotation);
    }

    public void SetRange(float f) { fRange = f; }
    public void SetATK(float f) { fATK = f; }

}