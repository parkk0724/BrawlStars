using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet : MonoBehaviour
{
    public float bullet_up_speed;
    public float bullet_speed;

    Transform SkillBullt_Pos = null;
    Transform SkillBullt_Destination = null;

    Coroutine skillbulletpos = null;
    Coroutine skillbulletdestination = null;
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
        
            this.transform.Translate(dir * delta);
        
            yield return null;
        }
        skillbulletpos = null;

        Rotate(SkillBullt_Destination, SkillBullt_Pos);
        skillbulletdestination = StartCoroutine(Bazooka_SkillBullet_Destination(SkillBullt_Destination));
    }

    IEnumerator Bazooka_SkillBullet_Destination(Transform skillbullet_pos)
    {                   
        while (true)
        {
            float delta = bullet_speed * Time.deltaTime;

            this.transform.Translate(this.transform.forward * delta);

            yield return null;
        }
        skillbulletdestination = null;
    }

    private void Rotate(Transform destination, Transform bulletpos)
    {
        Vector3 pos1 = destination.position;
        pos1.y = bulletpos.position.y;
        Vector3 dir1 = pos1 - bulletpos.position;
        dir1.Normalize();
        float d1 = Vector3.Dot(bulletpos.forward, dir1);
        float r1 = Mathf.Acos(d1);
        float e1 = 180.0f * (r1 / Mathf.PI);
        float rightd1 = Vector3.Dot(-bulletpos.right, dir1);

        if (rightd1 >= 0.0F)
            this.transform.Rotate(-this.transform.up * (e1 - 5.0f));
        else
            this.transform.Rotate(this.transform.up * (e1 - 5.0f));

        Vector3 pos2 = destination.position;
        Vector3 dir2 = pos2 - bulletpos.position;
        dir2.Normalize();
        float d2 = Vector3.Dot(this.transform.forward, dir2);
        float r2 = Mathf.Acos(d2);
        float e2 = 180.0f * (r2 / Mathf.PI);
        float rightd2 = Vector3.Dot(this.transform.up, dir2);

        if (rightd2 >= 0.0f)
            this.transform.Rotate(-this.transform.right * (e1- 5.0f));
        else
            this.transform.Rotate(this.transform.right * (e1 - 5.0f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("dd");
            Destroy(this.gameObject);
        }
    }

}
