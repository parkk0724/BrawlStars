using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterBullet : MonoBehaviour
{
    float m_fMoveSpeed = 10.0f;
    void Update()
    {
        this.transform.Translate(transform.forward * m_fMoveSpeed * Time.deltaTime);
    }
}
