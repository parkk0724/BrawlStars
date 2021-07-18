using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform BulletPos; // ������ ���� ��ġ
    public GameObject Bullet;   // ���� ������
    public int CurAmmo = 3;

    public void Shot()
    {
        // ---------------------------------------- �Ѿ˹߻� ----------------------------------------
        GameObject InstantBullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation); // �Ѿ��� �ν��Ͻ�ȭ �Ѵ�
        Rigidbody BulletRigid = InstantBullet.GetComponent<Rigidbody>();                        // �ν��Ͻ��� �Ѿ��� ������ٵ� ���� �´�
        BulletRigid.velocity = BulletPos.forward * 50;                                          // �Ѿ��� �����Ǹ鼭 �ӵ� 50�� �ٴ´�

        CurAmmo--;
    }

    public void Reload()
    {
        
    }
    
}
