using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet : MonoBehaviour
{
    public Transform SkillBullt_Pos1;
    // Start is called before the first frame update
    void Start()
    {
        //SkillBullt_Pos1 = p
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Bazooka_SkillBullet_Pos(GameObject skillbullet_pos)
    {
        Vector3 dir = skillbullet_pos.transform.position - this.transform.position;
        float dist = dir.magnitude;

        while (dist > 0.0f)
        {
            float delta = 3.0f * Time.deltaTime;

            if (dist - delta < 0.0f)
            {
                delta = dist;
            }

            dist -= delta;

            this.transform.Translate(dir * delta);

            yield return null;
        }
    }
}
