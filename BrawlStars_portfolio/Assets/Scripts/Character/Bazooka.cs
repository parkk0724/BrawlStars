using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    public GameObject bazooka_bullet;
    public Transform bazooka_bullet_pos;
    public float distance = 0.0f;
    public float height = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator fire()
    {
        GameObject Bazooka_Bullet = Instantiate(bazooka_bullet, bazooka_bullet_pos.position, bazooka_bullet_pos.rotation);

        while (Vector3.Distance(this.transform.position, bazooka_bullet.transform.position) < 5.0f)
        {
            Bazooka_Bullet.transform.Translate(bazooka_bullet_pos.forward * 2.0f * Time.deltaTime, Space.World);

            yield return null;
        }              
    }
}
