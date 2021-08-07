using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet2 : MonoBehaviour
{
    public GameObject explosion_effect;
    public GameObject StartSound;
    public GameObject Explosion_Sound;
    public float bullet_up_speed;
    public float bullet_speed;

    float m_fDamage = 50.0f;

    Transform SkillBullt_Pos = null;
    Transform SkillBullt_Destination = null;

    Coroutine skillbulletpos = null;
    Coroutine skillbulletdestination = null;

    float dist = 0.0f;
    void Start()
    {
        StartSound = GameObject.Find("SkillBazooka");
        StartSound.SetActive(false);
        SkillBullt_Destination = GameObject.Find("skillbullet_destination").transform;
        SkillBullt_Pos = GameObject.Find("skillbullet_pos2").transform;
        skillbulletpos = StartCoroutine(Bazooka_SkillBullet_Pos(SkillBullt_Pos));
    }
    IEnumerator Bazooka_SkillBullet_Pos(Transform skillbullet_pos)
    {
        if (StartSound.activeSelf)
            StartSound.GetComponent<AudioSource>().Play();
        else
            StartSound.SetActive(true);

        Vector3 dir = skillbullet_pos.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = bullet_up_speed * Time.deltaTime;

            if (dist - delta < 0.0f)
            {
                delta = dist;
            }

            dist -= delta;

            this.transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        skillbulletpos = null;

        this.transform.LookAt(SkillBullt_Destination.position);
        skillbulletdestination = StartCoroutine(Bazooka_SkillBullet_Destination());
    }

    IEnumerator Bazooka_SkillBullet_Destination()
    {
        while (dist < 20.0f)
        {
            float delta = bullet_speed * Time.deltaTime;
            dist += delta;

            this.transform.Translate(this.transform.forward * delta, Space.World);

            yield return null;
        }
        dist = 0.0f;
        skillbulletdestination = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 4.0f);

        if (other.tag == "Ground" || other.tag == "Wall" || other.tag == "Player")
        {
            GameObject Explosion_Effect = Instantiate(explosion_effect, this.transform.position, Quaternion.identity);
             Explosion_Sound = GameObject.Find("BazookaExplosion");
            Explosion_Sound.GetComponent<AudioSource>().Play();

            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].tag == "Monster")
                    colls[i].GetComponent<Monster>()?.Hit((int)m_fDamage, new Color(0, 0, 0, 1));
            }

            Destroy(this.gameObject);
        }
    }

}
