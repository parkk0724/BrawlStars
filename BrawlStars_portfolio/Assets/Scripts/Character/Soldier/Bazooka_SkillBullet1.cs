using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet1 : MonoBehaviour
{
    public float bullet_up_speed;
    public float bullet__speed;

    Transform SkillBullt_Pos;
    Transform SkillBullt_Destination;

    Coroutine skillbulletpos = null;
    Coroutine skillbulletdestination = null;
    void Start()
    {
        SkillBullt_Destination = GameObject.Find("skillbullet_destination").transform;
        SkillBullt_Pos = GameObject.Find("skillbullet_pos1").transform;
        skillbulletpos = StartCoroutine(Bazooka_SkillBullet_Pos(SkillBullt_Pos));
    }
    void Update()
    {
        if (skillbulletpos == null)
        {
            if (skillbulletdestination != null) StopCoroutine(skillbulletdestination);
            skillbulletdestination = StartCoroutine(Bazooka_SkillBullet_Destination(SkillBullt_Destination));

        }
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
    }

    IEnumerator Bazooka_SkillBullet_Destination(Transform skillbullet_pos)
    {
        Vector3 dir = skillbullet_pos.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = bullet__speed * Time.deltaTime;

            if (dist - delta < 0.0f)
            {
                delta = dist;
            }

            //dist -= delta;

            this.transform.Translate(dir * delta);

            yield return null;
        }
        skillbulletdestination = null;
        float d = Vector3.Dot(this.transform.forward, (SkillBullt_Destination.position - this.transform.position).normalized);
        float r = Mathf.Acos(d);
        float e = 180.0f * (r / Mathf.PI);
        this.transform.Rotate(Vector3.right * (e - 18.0f));// 정확한 각도가 구해지지 않아서 일단 -20도로 보정해줌..
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
