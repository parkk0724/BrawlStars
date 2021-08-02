using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class JesterSkill : MonoBehaviour
{
    NavMeshAgent nav;
    Animator anim;
    Rigidbody rigid;
    Transform m_tfResultTarget;
    Renderer[] myRender;
    enum SkillState
    {
        CREATE, IDLE, RUN, PATROL, ATTACK, DESTROY ,Death , DiZZY
    }
    SkillState State = SkillState.CREATE;
    public GameObject m_objSkillEffect;
    
    float Dist;
    float DestroyTime;
    public float DistRange;
    public float DeathTime;
    public float m_fTargetRange;
    public BoxCollider Attackcollider;
    public LayerMask m_lmEnemyLayer = 0;
    public float f_Range = 5f;
    public UnityAction Animationevent = null;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponentsInChildren<Renderer>();
        Animationevent = () => StartCoroutine(skilShot());
        anim = GetComponent<Animator>();
        State = SkillState.IDLE;
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    //
    void Update()
    {
        DestroyTime += Time.deltaTime;
        SearchTarget();
        Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        chageSate();
    }
    void ChangeState(SkillState s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case SkillState.CREATE:
                break;
            case SkillState.IDLE:
                break;
            case SkillState.PATROL:
                break;
            case SkillState.ATTACK:
                break;
            case SkillState.DiZZY:
                break;
            case SkillState.DESTROY:
                break;
            case SkillState.Death:
                break;
        }
    }
    void StateProcess()
    {
        switch (State)
        {
            case SkillState.CREATE:
                break;
            case SkillState.IDLE:
                break;
            case SkillState.PATROL:
                break;
            case SkillState.ATTACK:
                break;
            case SkillState.DiZZY:
                break;
            case SkillState.DESTROY:
                break;
            case SkillState.Death:
                break;
        }
    }
    void chageSate()
    {
       
        switch (State)
        {
            case SkillState.CREATE:
                break;
            case SkillState.IDLE:
                if (m_tfResultTarget != null)
                {
                    State = SkillState.RUN;
                }
                else
                {
                    return;
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
                        State = SkillState.DiZZY;
                    }
                }
                break;
            case SkillState.DiZZY:
                StartCoroutine(Dizzy());
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
    IEnumerator Dizzy()
    {
        Vector3 thisScale = new Vector3(2f, 2f, 2f);
        float thisgob = this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
        float thisgob_2 = thisScale.x * thisScale.y * thisScale.z;
        this.transform.localScale += new Vector3(0.6f, 0.3f, 0.6f) * Time.deltaTime;
        this.transform.Rotate(0, 3f, 0);
        if (thisgob_2 > thisgob)
        {
            //float Gobdelta = thisgob_2 - thisgob;
            anim.SetTrigger("Dizzy");
            for (int i = 0; i < 5; i++)
            {
                myRender[0].material.color = Color.white;
                myRender[1].material.color = Color.white;
                yield return new WaitForSeconds(0.3f);
                myRender[0].material.color = Color.red;
                myRender[1].material.color = Color.red;
            }
        }

        if (thisgob > thisgob_2)
            State = SkillState.Death;
    }
}

