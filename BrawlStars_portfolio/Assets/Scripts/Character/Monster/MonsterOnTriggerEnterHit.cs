using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnTriggerEnterHit : MonoBehaviour
{
    float m_fCurHitDelay = 0.0f;
    float m_fMaxHitDelay = 1.0f;
    Monster m_monster = null;

    private void Awake()
    {
        m_monster = this.GetComponentInParent<Monster>();
    }
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
            other.GetComponent<Character>().Hit(m_monster.GetATK(), Color.red);
            m_fCurHitDelay = 0.0f;
        }
    }
}
