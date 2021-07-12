using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected UITextDamage m_UITextDamage;
    protected Animator m_Animator;
    protected Vector3 m_vOriginPos;
    protected Vector3 m_vOriginRot;
    [SerializeField] protected int m_nHP;
    protected int m_nMaxHP;
    protected float m_fStamina;
    protected float m_fMaxStamina;
    protected int m_nATK;
    protected int m_nDEF;
    protected int m_nSkillDamage;
    protected float m_fMoveSpeed;
    protected float m_fAttackSpeed;
    protected float m_fFever;
    protected float m_fMaxFever;
    protected float m_fRange;
    protected bool m_bDie;

    public HealthBar HealthBar; // UI, 체력바, Hit가 여기서 처리돼서 여기에 배치
    protected virtual void Update()
    {
        if (m_bDie)
        {
            Die();
        }
        else
        {
            Move();
            Attack();
        }
    }
    public abstract void Attack(); 
    public abstract void SkillAttack();
    public abstract void Move();
    public abstract void Revival();
    public abstract IEnumerator Die();
    public virtual void Hit(int damage) // 온콜라이더에서 호출하는게 좋을 것 같음
    {
        // 총알이 닿으면, Bullet 스크립트에서 Hit 함수 발생.
        int DefDamage =  damage - m_nDEF;

        if(DefDamage > 0)
        {
            m_UITextDamage.SetDamage(DefDamage, this.transform.position);
            m_nHP = m_nHP - DefDamage;      // 데미지 계산
            m_Animator.SetTrigger("tHit");  // 히트모션
            HealthBar.SetHealth(m_nHP);     // UI, 체력바 현재체력으로 증감
        }
        Debug.Log(m_nHP);

        // 210710.0451: 플레이어에게 리지드바디 주면 버그 생겨서 총알에 리지드바디 주니 괜찮은데 나중에 가볍게 게임 만들려면 해결해야 할 듯.
        if (m_nHP <= 0)
        {
            m_nHP = 0;
            m_Animator.SetBool("bDie", true);
        }
    }
}
