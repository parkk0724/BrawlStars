using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterBullet : MonoBehaviour
{
    float m_fMoveSpeed = 10.0f;
    void Update()
    {
        this.transform.Translate(this.transform.forward * m_fMoveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<Hero>())
            other.GetComponent<Hero>().Hit(10, Color.red);
        }

        if(other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
