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
    [SerializeField] GameObject m_objSkill;
    [SerializeField] Transform m_tTarget;
    NavMeshAgent nav;
    SkillState State = SkillState.CREATE;
    Animator anim;
    float Dist;
    public float DistRange;
    float DestroyTime;
    public float DeathTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        State = SkillState.IDE;
        nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    //
    void Update()
    {
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
                if(m_tTarget != null)
                {
                    State = SkillState.RUN;
                }
                break;
            case SkillState.RUN:
                {
                    nav.speed = Random.Range(3, 10);
                    nav.SetDestination(m_tTarget.position);
                    anim.SetBool("bMove", true);
                    Dist = Vector3.Distance(this.transform.position, m_tTarget.position);
                    if (DistRange > Dist)
                    {
                        State = SkillState.ATTACK;
                    }
                }
                break;
            case SkillState.ATTACK:
                {
                    if (DistRange > Dist)
                    {
                        anim.SetBool("bMove", false);
                        anim.SetTrigger("tBAttack");
                    }
                    else 
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
                //DestEffect(2);
                Destroy(this.gameObject);
                break;
        }
    }

    IEnumerator DestEffect(float time)
    {
        m_objSkill.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        m_objSkill.gameObject.SetActive(false);
    }
}
