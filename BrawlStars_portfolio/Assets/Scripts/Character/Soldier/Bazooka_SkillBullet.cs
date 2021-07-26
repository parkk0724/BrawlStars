using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet : MonoBehaviour
{
    public GameObject explosion_effect;
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
        SkillBullt_Destination = GameObject.Find("skillbullet_destination").transform;
        SkillBullt_Pos = GameObject.Find("skillbullet_pos").transform;
        skillbulletpos = StartCoroutine(Bazooka_SkillBullet_Pos(SkillBullt_Pos));
    }
   IEnumerator Bazooka_SkillBullet_Pos(Transform skillbullet_pos)
   {
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
        if (other.tag == "Ground" || other.tag == "Wall" || other.tag == "Player")
        {
            //collider_size.size = new Vector3(30.0f, 30.0f, 30.0f);
            GameObject Explosion_Effect = Instantiate(explosion_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.tag == "Monster")
        {
            other.GetComponent<Monster>()?.Hit((int)m_fDamage, new Color(0, 0, 0, 1));
            GameObject Explosion_Effect = Instantiate(explosion_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
