using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] m_tBulletPos;
    public  GameObject m_objbullet;
    public Transform m_tBulletPosCase;
    public GameObject m_objBulletCase;
    public ParticleSystem Shooteffect;
    Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        this.GetComponentInParent<JesterAnimationEv>().OnShot_0 = Shoot_0;
        this.GetComponentInParent<JesterAnimationEv>().OnShot_1 = Shoot_1;
        this.GetComponentInParent<JesterAnimationEv>().OnShot_2 = Shoot_2;
        this.GetComponentInParent<JesterAnimationEv>().Anim_end = Anim_end;
        this.GetComponentInParent<JesterAnimationEv>().FireEffect = () => StartCoroutine(Fire());
    }
    void Shoot_0()
    {
        anim.SetTrigger("doShoot");
        Instantiate(m_objbullet, m_tBulletPos[0].position, m_tBulletPos[0].rotation);
        BullutCaseInit(2);
    }
    void Shoot_1()
    {
        anim.SetTrigger("doShoot");
        Instantiate(m_objbullet, m_tBulletPos[1].position, m_tBulletPos[1].rotation);
        BullutCaseInit(2);
    }
    void Shoot_2()
    {
        anim.SetTrigger("doShoot");
        Instantiate(m_objbullet, m_tBulletPos[2].position, m_tBulletPos[2].rotation);
        BullutCaseInit(2);
    }
    void Anim_end()
    {
        this.GetComponentInParent<Jester>().SetRot_flase(false);
    }
    IEnumerator Fire()
    {
        Shooteffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Shooteffect.gameObject.SetActive(false);
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