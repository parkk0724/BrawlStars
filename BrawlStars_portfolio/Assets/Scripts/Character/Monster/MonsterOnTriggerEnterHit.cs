using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnTriggerEnterHit : MonoBehaviour
{
    float m_fCurHitDelay = 0.0f;
    float m_fMaxHitDelay = 1.0f;

    private void FixedUpdate()
    {
        m_fCurHitDelay += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        m_fCurHitDelay = m_fMaxHitDelay;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && m_fCurHitDelay >= m_fMaxHitDelay)
        {
            other.GetComponent<Character>().Hit(10, Color.red);
            m_fCurHitDelay = 0.0f;
        }
    }
}
