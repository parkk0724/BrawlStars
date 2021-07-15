using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka_SkillBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Bazooka_SkillBullet_Pos(GameObject skillbullet, GameObject skillbullet_pos)
    {
        Vector3 dir = skillbullet_pos.transform.position - skillbullet.transform.position;
        float dist = dir.magnitude;

        while (dist > 0.0f)
        {
            float delta = 3.0f * Time.deltaTime;

            if (dist - delta < 0.0f)
            {
                delta = dist;
            }

            dist -= delta;

            skillbullet.transform.Translate(dir * delta);

            yield return null;
        }
    }
}
