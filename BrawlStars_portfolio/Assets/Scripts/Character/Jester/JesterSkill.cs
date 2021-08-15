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
        CREATE, IDLE, RUN, PATROL, ATTACK, DESTROY, Death, DiZZY
    }
    SkillState State = SkillState.CREATE;
    public GameObject m_objSkillEffect;

    float Dist;
    float DestroyTime;
    private float DistRange = 2f;
    private float DeathTime = 10f;
    public float m_fTargetRange;
    public BoxCollider Attackcollider;
    public LayerMask m_lmEnemyLayer = 0;
    public float f_Range = 5f;
    public UnityAction Animationevent = null;
    float Curtime;
    public float GetCurtime() { return DestroyTime; }
    public float GetDeathtime() { return DeathTime; }
    void Start()
    {
        Curtime = 0;
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
        SkillSearchTarget();
        //Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        chageSate();
        //StateProcess();
    }
    void ChangeState(SkillState s)
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case SkillState.IDLE:
                StartCoroutine(RandomRange(Random.Range(0, 2), SkillState.PATROL));
                break;
            case SkillState.PATROL:
                {
                    anim.SetBool("bMove", true);
                    RandomPatrol();
                }
                break;
            case SkillState.RUN:
                {
                    nav.speed = 5;
                    nav.SetDestination(m_tfResultTarget.position);
                    anim.SetBool("bMove", true);
                }
                break;
            case SkillState.ATTACK:
                {
                    //Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
                    //if (DistRange > Dist)
                    //{
                    //    State = SkillState.ATTACK;
                    //}
                    if (DistRange <= Dist)
                    {
                        ChangeState(SkillState.IDLE);
                    }
                }
                break;
            case SkillState.DiZZY:
                State = SkillState.DiZZY;
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
            case SkillState.IDLE:
                if (m_tfResultTarget != null)
                {
                    ChangeState(SkillState.RUN);
                }
                else
                {
                    ChangeState(SkillState.PATROL);
                }
                if (DestroyTime > DeathTime)
                {
                    ChangeState(SkillState.DiZZY);
                }
                break;
            case SkillState.PATROL:
                if (m_tfResultTarget != null)
                {
                    ChangeState(SkillState.RUN);
                }
                else
                {
                    ChangeState(SkillState.IDLE);
                }
                if (DestroyTime > DeathTime)
                {
                    ChangeState(SkillState.DiZZY);
                }
                break;
            case SkillState.RUN:
                Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
                if (m_tfResultTarget != null && DistRange > Dist)
                {
                    ChangeState(SkillState.ATTACK);
                }
                else
                {
                    ChangeState(SkillState.IDLE);
                }
                if (DestroyTime > DeathTime)
                {
                    ChangeState(SkillState.DiZZY);
                }
                break;

            case SkillState.ATTACK:
                if (m_tfResultTarget == null)
                {
                    ChangeState(SkillState.IDLE);
                }
                if (m_tfResultTarget != null)
                {
                    Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
                    if (DistRange > Dist)
                    {
                        Vector3 resultYtarget = new Vector3(m_tfResultTarget.position.x, this.transform.position.y, m_tfResultTarget.position.z);
                        this.transform.LookAt(resultYtarget);
                        anim.SetBool("bMove", false);
                        anim.SetTrigger("tBAttack");
                        nav.speed = 2;
                    }
                }
                if (DestroyTime > DeathTime)
                {
                    ChangeState(SkillState.DiZZY);
                }
                break;
            case SkillState.DiZZY:
                StartCoroutine(Dizzy());
                break;
            case SkillState.Death:
                DeathAttack();
                DestEffect();
                State = SkillState.DESTROY;
                break;
            case SkillState.DESTROY:
                Destroy(this.gameObject);
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
                if (DestroyTime > DeathTime)
                {
                    State = SkillState.DiZZY;
                }
                if (m_tfResultTarget != null)
                {
                    State = SkillState.RUN;
                }
                else
                {
                    RandomPatrol();
                }
                break;
            case SkillState.PATROL:
                if (DestroyTime > DeathTime)
                {
                    State = SkillState.DiZZY;
                }
                if (m_tfResultTarget != null)
                {
                    State = SkillState.RUN;
                }
                else
                {
                    RandomPatrol();
                }
                break;
            case SkillState.RUN:
                {
                    if (m_tfResultTarget == null)
                    {
                        if (DestroyTime > DeathTime)
                        {
                            State = SkillState.DiZZY;
                        }
                        State = SkillState.PATROL;
                    }
                    else if (m_tfResultTarget != null)
                    {
                        if (DestroyTime > DeathTime)
                        {
                            State = SkillState.DiZZY;
                        }
                        nav.speed = 5;
                        nav.SetDestination(m_tfResultTarget.position);
                        anim.SetBool("bMove", true);
                        Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
                    }
                    if (DistRange > Dist)
                    {
                        State = SkillState.ATTACK;
                    }
                    else
                    {
                        State = SkillState.IDLE;
                    }
                }
                break;
            case SkillState.ATTACK:
                {
                    if (DestroyTime > DeathTime)
                    {
                        State = SkillState.DiZZY;
                    }
                    if (m_tfResultTarget == null)
                    {
                        State = SkillState.PATROL;
                    }
                    if (m_tfResultTarget != null)
                    {
                        Dist = Vector3.Distance(this.transform.position, m_tfResultTarget.position);
                        if (DistRange > Dist)
                        {
                            Vector3 resultYtarget = new Vector3(m_tfResultTarget.position.x, this.transform.position.y, m_tfResultTarget.position.z);
                            this.transform.LookAt(resultYtarget);
                            anim.SetBool("bMove", false);
                            anim.SetTrigger("tBAttack");
                            nav.speed = 2;
                            if (m_tfResultTarget == null)
                            {
                                State = SkillState.PATROL;
                            }
                            if (DestroyTime > DeathTime)
                            {
                                State = SkillState.DiZZY;
                            }
                        }
                        else
                        {
                            State = SkillState.RUN;
                        }
                    }
                }
                break;
            case SkillState.DiZZY:
                StartCoroutine(Dizzy());
                break;
            case SkillState.Death:
                DeathAttack();
                DestEffect();

                State = SkillState.DESTROY;
                break;

            case SkillState.DESTROY:
                Destroy(this.transform.parent.gameObject);
                break;
        }
    }
    void RandomPatrol()
    {
        Curtime += Time.deltaTime;
        Vector3 OriginPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        float xpos = Random.Range(-10, 10);
        float zpos = Random.Range(-10, 10);
        Vector3 posPlus = new Vector3(this.transform.position.x + xpos, this.transform.position.y, this.transform.position.z + zpos);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(posPlus, out hit, 1.0f, NavMesh.AllAreas)) // 랜덤한 위치가 NavMesh로 이동할 수 있는지 확인
        {
            if (Curtime > 1)
            {
                nav.speed = 3;
                posPlus = hit.position; // 가능하면 그 위치값 내보냄
                nav.SetDestination(posPlus);
                Curtime = 0;
                anim.SetBool("bMove", true);
            } 
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
    void SkillSearchTarget()
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
        m_tfResultTarget = shorTarget; // 최종값
    }
    void onAnimationEv() //공격에니메이션
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
    IEnumerator RandomRange(float time, SkillState s) //다음 상태로 넘김
    {
        yield return new WaitForSeconds(time);
        ChangeState(s);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, f_Range);
    }
    IEnumerator Dizzy()
    {
        nav.speed = 0;
        Vector3 thisScale = new Vector3(2.4f, 1.2f, 2.4f);
        float thisgob = this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
        float thisgob_2 = thisScale.x * thisScale.y * thisScale.z;
        float Waittime = 0;
        this.transform.localScale += new Vector3(0.6f, 0.3f, 0.6f) * Time.deltaTime;
        this.transform.Rotate(0, 3f, 0);
        if (thisgob_2 > thisgob)
        {
            anim.SetTrigger("Dizzy");
            Waittime = (thisgob_2 - thisgob) / thisgob_2;
            if(Waittime <0.3f)
            {
                Waittime = 0.3f; //빠르게 돌려주기위해 for문 사용
                for (int i = 0; i < 10; i++)
                {
                    myRender[0].material.color = Color.white;
                    myRender[1].material.color = Color.white;
                    yield return new WaitForSeconds(Waittime);
                    myRender[0].material.color = Color.red;
                    myRender[1].material.color = Color.red;
                }
            }
            
            myRender[0].material.color = Color.white;
            myRender[1].material.color = Color.white;
            yield return new WaitForSeconds(Waittime);
            myRender[0].material.color = Color.red;
            myRender[1].material.color = Color.red;
        }
        if (thisgob > thisgob_2)
            State = SkillState.Death;
    }
}

