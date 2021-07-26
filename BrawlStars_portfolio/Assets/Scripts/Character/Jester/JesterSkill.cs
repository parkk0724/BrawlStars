using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class JesterSkill : MonoBehaviour
{
    enum SkillState
    {
        CREATE, IDE, RUN, PATROL ,ATTACK, DESTROY ,Death
    }
    SkillState State = SkillState.CREATE;
    public GameObject m_objSkillEffect;
    NavMeshAgent nav;
    Animator anim;
    float Dist;
    float DestroyTime;
    public float DistRange;
    public float DeathTime;
    Rigidbody rigid;
    Transform m_tfResultTarget;
    public float m_fTargetRange;
    public BoxCollider Attackcollider;
    public LayerMask m_lmEnemyLayer = 0;
    public float f_Range = 5f;
    public UnityAction Animationevent = null;
    // Start is called before the first frame update
    void Start()
    {
        Animationevent = () => StartCoroutine(skilShot());
        //Attackcollider = GetComponentInChildren<BoxCollider>();
        //m_objSkillEffect = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        State = SkillState.IDE;
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    //
    void Update()
    {
        SearchTarget();
        Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        chageSate();
    }
    void chageSate()
    {
        DestroyTime += Time.deltaTime;
        switch (State)
        {
            case SkillState.CREATE:
                break;
            case SkillState.IDE:
                if (m_tfResultTarget != null)
                {
                    State = SkillState.RUN;
                }
                else
                {
                    State = SkillState.RUN;
                }
                break;
            case SkillState.RUN:
                {
                    nav.speed = 5;
                    nav.SetDestination(m_tfResultTarget.position);
                    anim.SetBool("bMove", true);

                    if (DistRange > Dist)
                    {
                        State = SkillState.ATTACK;
                    }
                }
                break;
            case SkillState.ATTACK:
                {
                    Vector3 resultYtarget = new Vector3(m_tfResultTarget.position.x, this.transform.position.y, m_tfResultTarget.position.z);
                    if (DistRange > Dist)
                    {
                        this.transform.LookAt(resultYtarget);
                        anim.SetBool("bMove", false);
                        anim.SetTrigger("tBAttack");
                        nav.speed = 2;
                    }
                    else if (DistRange <= Dist)
                    {
                        State = SkillState.RUN;
                    }
                    if (DestroyTime > DeathTime)
                    {
                        State = SkillState.Death;
                    }
                }
                break;
            case SkillState.Death:

                DeathAttack();
                DestEffect();
                DestEffect();
                State = SkillState.DESTROY;
                break;

            case SkillState.DESTROY:
                Destroy(this.gameObject);
                break;
        }
    }
    void DeathAttack()
    {
        GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, f_Range, m_lmEnemyLayer);
        for (int i = 0; i < monster.Length; i++)
        {
            for (int j = 0; j < EnemyCollider.Length; j++)
            {
                if (monster[i].GetComponent<Collider>() == EnemyCollider[j])
                {
                    monster[i].GetComponent<Monster>()?.Hit(20, Color.red);
                }
            }
        }
    }
    void DestEffect()
    {
        GameObject obj = Instantiate(m_objSkillEffect, this.transform.position, this.transform.rotation);
    }
    void SearchTarget()
    {
        float Shortdist = 15;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer);
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
                if (Shortdist > Dist)
                {
                    Shortdist = Dist;
                    shorTarget = EnemyCollider[i].transform;
                }
            }
        }
        m_tfResultTarget = shorTarget; // ÃÖÁ¾°ª
    }
    void onAnimationEv()
    {
        if (Animationevent != null)
            Animationevent();
    }
    IEnumerator skilShot()
    {
        Attackcollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Attackcollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, f_Range);
    }
}

