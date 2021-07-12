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

    public HealthBar HealthBar; // UI, ü�¹�, Hit�� ���⼭ ó���ż� ���⿡ ��ġ
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
    public virtual void Hit(int damage) // ���ݶ��̴����� ȣ���ϴ°� ���� �� ����
    {
        // �Ѿ��� ������, Bullet ��ũ��Ʈ���� Hit �Լ� �߻�.
        int DefDamage =  damage - m_nDEF;

        if(DefDamage > 0)
        {
            m_UITextDamage.SetDamage(DefDamage, this.transform.position);
            m_nHP = m_nHP - DefDamage;      // ������ ���
            m_Animator.SetTrigger("tHit");  // ��Ʈ���
            HealthBar.SetHealth(m_nHP);     // UI, ü�¹� ����ü������ ����
        }
        Debug.Log(m_nHP);

        // 210710.0451: �÷��̾�� ������ٵ� �ָ� ���� ���ܼ� �Ѿ˿� ������ٵ� �ִ� �������� ���߿� ������ ���� ������� �ذ��ؾ� �� ��.
        if (m_nHP <= 0)
        {
            m_nHP = 0;
            m_Animator.SetBool("bDie", true);
        }
    }
}
