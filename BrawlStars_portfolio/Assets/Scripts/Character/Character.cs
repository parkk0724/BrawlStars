using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StandardShaderRenderingMode { Opaque, Cutout, Fade, Transparent }
public abstract class Character : MonoBehaviour
{
    // Start is called before the first frame update
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
        m_nHP -= damage;

        if (m_nHP <= 0)
        {
            m_nHP = 0;
            m_Animator.SetBool("bDie", true);
        }
    }
}
