using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JesterSkill : MonoBehaviour
{
    enum SkillState
    {
        CREATE, IDE, RUN, PATROL ,ATTACK, DESTROY ,Death
    }
    public GameObject m_objSkillEffect;
    [SerializeField] Transform m_tTarget;
    NavMeshAgent nav;
    SkillState State = SkillState.CREATE;
    Animator anim;
    float Dist;
    public float DistRange;
    float DestroyTime;
    public float DeathTime;
    Rigidbody rigid;
    Transform m_tfResultTarget;
    public float m_fTargetRange;
    private SphereCollider Boomattck;
    public BoxCollider Attackcollider;
    public LayerMask m_lmEnemyLayer = 0;
    float Range = 10f;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        //Attackcollider = GetComponentInChildren<BoxCollider>();
        //m_objSkillEffect = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        State = SkillState.IDE;
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        Boomattck = GetComponentInChildren<SphereCollider>();
    }
    // Update is called once per frame
    //
    void Update()
    {
        SearchTarget();
        //StartCoroutine(DestEffect());
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
                //nav.speed = Random.Range(3, 10);
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
                    Vector3 distrot = m_tfResultTarget.position - this.transform.position;
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
                        State = SkillState.DESTROY;
                    }
                }
                break;
            case SkillState.DESTROY:
                test();
                DestEffect();
                DestEffect();
                //StartCoroutine(dess());
                Destroy(this.gameObject,0.2f);
                break;
        }
    }
    void test()
    {
        float test_range = 10f;
        GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position,test_range, m_lmEnemyLayer);
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
    IEnumerator dess()
    {
        start = true;
        Boomattck.enabled = true;
        yield return new WaitForSeconds(0.05f);
        start = false;
        Boomattck.enabled = false;
        DestEffect();
        yield return new WaitForSeconds(0.05f);
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
    void JesterSkillShot()
    {
        StartCoroutine(skilShot());
    }
    IEnumerator skilShot()
    {
        Attackcollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Attackcollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(start)
        {
            if (other.GetComponent<Monster>())
            {
                
                other.GetComponent<Monster>().Hit(20, Color.red);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!start)
        {
            if (other.GetComponent<Monster>())
            {
                
            }
        }
    }
}

