using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform BulletPos; // 프리팹 생성 위치
    public GameObject Bullet;   // 담을 프리팹
    public int CurAmmo = 3;

    public void Shot()
    {
        // ---------------------------------------- 총알발사 ----------------------------------------
        GameObject InstantBullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation); // 총알을 인스턴스화 한다
        Rigidbody BulletRigid = InstantBullet.GetComponent<Rigidbody>();                        // 인스턴스된 총알의 리지드바디 갖고 온다
        BulletRigid.velocity = BulletPos.forward * 50;                                          // 총알이 생성되면서 속도 50이 붙는다

        CurAmmo--;
    }

    public void Reload()
    {
        
    }
    
}
