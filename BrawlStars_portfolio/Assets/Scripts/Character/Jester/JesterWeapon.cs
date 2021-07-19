using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] m_tBulletPos;
    [SerializeField] GameObject m_objbullet;
    [SerializeField] Transform m_tBulletPosCase;
    [SerializeField] GameObject m_objBulletCase;
    Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        //this.GetComponentInParent<JesterAnimationEv>.Shoot

    }
    void Shoot()
    {
        anim.SetTrigger("Shoot");
        Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);

    }

    void BullutCaseInit(float time)
    {
        GameObject instanBulletCase = Instantiate(m_objBulletCase, m_tBulletPosCase.position, m_tBulletPosCase.rotation);
        Rigidbody bulletcaseRigid = instanBulletCase.GetComponent<Rigidbody>();
        Vector3 pos = m_tBulletPosCase.forward * Random.Range(-3, -2) + m_tBulletPosCase.up * Random.Range(2, 5);
        bulletcaseRigid.AddForce(pos, ForceMode.Impulse);
        bulletcaseRigid.AddTorque(Vector3.up * Random.Range(-10, 10));
        Destroy(instanBulletCase, time);
    }
}
