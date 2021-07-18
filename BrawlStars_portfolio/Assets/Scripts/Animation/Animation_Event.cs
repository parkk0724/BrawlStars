using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animation_Event : MonoBehaviour
{
    [Header("Bazooka")]
    public GameObject bazooka_Basic_bullet;
    public GameObject bazooka_Skill_bullet1;
    public GameObject bazooka_Skill_bullet2;
    public GameObject bazooka_Skill_bullet3;
    public GameObject bazooka_Skill_bullet4;
    public Transform bazooka_bullet_pos;
    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;

    Coroutine skillbullet = null;
    // Start is called before the first frame update
    private void Bazooka_Basic_Fire()
    {
        Bazooka_Bullet_Initiate(bazooka_Basic_bullet);
    }

    private void Bazooka_Skill_Fire()
    {
        skillbullet = StartCoroutine(Bazooka_SkillBullet_Initiate(0.5f));
        if (skillbullet == null) StopCoroutine(skillbullet);
    }

    void Bazooka_Bullet_Initiate(GameObject bullet)
    {
        GameObject skillbullet = Instantiate(bullet, bazooka_bullet_pos.position, bazooka_bullet_pos.rotation);
    }

    IEnumerator Bazooka_SkillBullet_Initiate(float t)
    {
       Bazooka_Bullet_Initiate(bazooka_Skill_bullet1);
       yield return new WaitForSeconds(t / 3.0f);
       Bazooka_Bullet_Initiate(bazooka_Skill_bullet4);
       yield return new WaitForSeconds(t);
       Bazooka_Bullet_Initiate(bazooka_Skill_bullet2);
       yield return new WaitForSeconds(t / 3.0f);
       Bazooka_Bullet_Initiate(bazooka_Skill_bullet3);
       yield return null;
       skillbullet = null;
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }
    private void SkillShoot()
    {
        OnSkillShoot?.Invoke();
    }
}
