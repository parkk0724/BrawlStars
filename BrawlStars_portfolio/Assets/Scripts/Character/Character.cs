using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected UITextDamage m_UITextDamage;
    protected Animator m_Animator;
    protected Vector3 m_vOriginPos;
    protected Vector3 m_vOriginRot;
    [SerializeField] protected int m_nHP;
    protected int m_nMaxHP;

    protected int m_nATK;
    protected int m_nDEF;
    protected float m_fMoveSpeed;
    protected float m_fAttackSpeed;
    protected float m_fRange;

    protected GameObject m_objIndicator;
    public float GetHp() { return m_nHP; }
    public float GetMaxHp() { return m_nMaxHP; }
    public int GetATK() { return m_nATK; }
    public abstract void Move();
    public abstract IEnumerator Die();

    public virtual void Hit(int damage, Color c) // ���ݶ��̴����� ȣ���ϴ°� ���� �� ���� *���������� ������ Ʋ�����ϱ����� color�� �߰�
    {
        // �Ѿ��� ������, Bullet ��ũ��Ʈ���� Hit �Լ� �߻�.
        int DefDamage =  damage - m_nDEF;

        if(DefDamage > 0)
        {
            m_UITextDamage = GameObject.Find("TextDamage").GetComponent<UITextDamage>();
            m_UITextDamage.SetDamage(DefDamage, this.transform.position, c);
            m_nHP = m_nHP - DefDamage;      // ������ ���
            if(m_nHP > 0) m_Animator.SetTrigger("tHit");  // ��Ʈ���
            //HealthBar.SetHealth(m_nHP);     // UI, ü�¹� ����ü������ ����
        }

        // 210710.0451: �÷��̾�� ������ٵ� �ָ� ���� ���ܼ� �Ѿ˿� ������ٵ� �ִ� �������� ���߿� ������ ���� ������� �ذ��ؾ� �� ��.
        if (m_nHP <= 0)
        {
            m_nHP = 0;
        }
    }
}
