using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    // Start is called before the first frame update
    bool[] m_bPhase = new bool[2] { false, false };
    protected override void Start()
    {
        base.Start();

        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;
        m_nATK = 10;
        m_nDEF = 5;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        CheckPhase();
    }

    void CheckPhase()
    {
        if (m_nHP < m_nMaxHP / 2 && !m_bPhase[0]) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 첫 페이즈가 바뀔때)
        {
            m_bPhase[0] = true;
            // 여기서 상태값 조절
        }
        else if (m_nHP < m_nMaxHP / 4 && !m_bPhase[1]) // HP가 절반 이하이고 1페이즈에 들어가지 않았을경우 (처음 두번째 페이즈가 바뀔때)
        {
            m_bPhase[1] = true;
        }
    }
    public override void Attack()
    {
        int rnd = Random.Range(1, 100);
        if (rnd < 5 && m_bPhase[1])
        {
            SkillAttack2();
        }
        else if ( rnd < 25 && m_bPhase[0])
        {
            SkillAttack1();
        }
        else
        {
            BasicAttack();
        }
    }

    void BasicAttack()
    {

    }
    void SkillAttack1()
    {

    }
    void SkillAttack2()
    {

    }
}
