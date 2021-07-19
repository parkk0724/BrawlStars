using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JesterSkill : MonoBehaviour
{
    enum SkillState
    {
        CREATE,IDE,RUN,ATTACK,DESTROY
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
    // Start is called before the first frame update
    void Start()
    {
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
        //StartCoroutine(DestEffect());
        Dist = Vector3.Distance(this.transform.position, m_tTarget.position);
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
                if (m_tTarget != null)
                {
                    State = SkillState.RUN;
                }
                break;
            case SkillState.RUN:
                {
                    nav.speed = 7;
                    nav.SetDestination(m_tTarget.position);
                    anim.SetBool("bMove", true);
                   
                    if (DistRange > Dist)
                    {
                        State = SkillState.ATTACK;
                    }
                }
                break;
            case SkillState.ATTACK:
                {
                    Vector3 distrot = m_tTarget.position -this.transform.position;
                    Vector3 distbojung = new Vector3(0, distrot.y, 0);
                    Vector3 rate = new Vector3(0, m_tTarget.position.y,0);
                    if (DistRange > Dist)
                    {
                        //this.transform.rotation = Quaternion.Euler(distbojung.normalized);
                        this.transform.LookAt(distrot);
                        anim.SetBool("bMove", false);
                        anim.SetTrigger("tBAttack");
                        nav.speed = 0;
                    }
                    else if(DistRange <= Dist)
                    {
                        State = SkillState.RUN;
                    }
                    if(DestroyTime > DeathTime)
                    {
                        State = SkillState.DESTROY;
                    }
                }
                break;
            case SkillState.DESTROY:
                //DestEffect();
                StartCoroutine(DestEffect());
                Destroy(this.gameObject);
                break;
        }
    }

    IEnumerator DestEffect()
    {
        Instantiate(m_objSkillEffect, this.transform.position, this.transform.rotation);
        yield return null;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            anim.SetTrigger("tBAttack");
        }
    }

}
