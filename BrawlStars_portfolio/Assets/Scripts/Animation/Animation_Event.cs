using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animation_Event : MonoBehaviour
{
    public GameObject player;
    public GameObject bazooka_Basic_bullet;
    public GameObject bazooka_Skill_bullet;
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
        skillbullet = StartCoroutine(Bazooka_SkillBullet_Initiate(1.0f));
        if (skillbullet == null) StopCoroutine(skillbullet);
    }

    void Bazooka_Bullet_Initiate(GameObject bullet)
    {
        GameObject skillbullet = Instantiate(bullet, bazooka_bullet_pos.position, Quaternion.identity);
        float dot = Vector3.Dot(skillbullet.transform.forward, player.transform.forward);
        float r = Mathf.Acos(dot);
        float e = 180.0f * (r / Mathf.PI);

        if (Vector3.Dot(player.transform.forward, bullet.transform.right) >= 0)
            skillbullet.transform.Rotate(skillbullet.transform.up * e);
        else
            skillbullet.transform.Rotate(-skillbullet.transform.up * e);
    }

    IEnumerator Bazooka_SkillBullet_Initiate(float t)
    {
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet);
        yield return new WaitForSeconds(t);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet);
        yield return new WaitForSeconds(t);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet);
        yield return new WaitForSeconds(t);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet);
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
