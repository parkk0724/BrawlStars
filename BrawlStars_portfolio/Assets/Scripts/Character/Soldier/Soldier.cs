using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Soldier : Hero
{
    public LayerMask pickingmask;
    public GameObject bazooka_Basic_bullet;    
    public GameObject bazooka_Skill_bullet1;
    public GameObject bazooka_Skill_bullet2;
    public GameObject bazooka_Skill_bullet3;
    public GameObject bazooka_Skill_bullet4;
    public Transform bazooka_bullet_pos;
    public Transform bazooka_Skill_Destination;

    GameObject Bullet;
    GameObject Fire_Sound;

    public float Jump_Speed = 0.0f;
    public float Jump_Height = 0.0f;

    Animation_Event animation_event;
    protected override void Start()
    {
        base.Start();
        this.GetComponentInChildren<Animation_Event>().bazooka_basic_fire = Basic_Fire;
        this.GetComponentInChildren<Animation_Event>().bazooka_skill_fire = Skill_Fire;
        Fire_Sound = GameObject.Find("BazookaFire");
    }
    public override void Attack()
    {
         if (m_fStamina >= 1.0f)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 m_fStamina -= 1.0f;
                 m_Animator.SetTrigger("tBAttack");
             }
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 Roll();
             }
         }
          if (Input.GetMouseButtonUp(0))
          {
              SetRotStart(false);
          }

         if (m_fFever >= m_fMaxFever)
         {
              if (Input.GetMouseButton(1))
              {
                  bazooka_Skill_Destination = GameObject.Find("skillbullet_destination").transform;
                  m_Animator.SetTrigger("tSAttack");
                  SetRotStart(false);
                  m_fFever = 0.0f;
              }
          }        
         if (Input.GetMouseButtonUp(1))
           {
               SetRotStart(false);
           }        
    }

    private void Basic_Fire()
    {
        Bazooka_Bullet_Initiate(bazooka_Basic_bullet);
        Fire_Sound.GetComponent<AudioSource>().Play();
        BazookaBullet basic_bullet = Bullet.GetComponent<BazookaBullet>();
        basic_bullet.Fever_up = FeverUp;
    }
    private void Skill_Fire()
    {
        Fire_Sound.GetComponent<AudioSource>().Play();
        StartCoroutine(Bazooka_SkillBullet_Initiate(0.5f));
    }
    void Bazooka_Bullet_Initiate(GameObject bullet)
    {
        Bullet = Instantiate(bullet, bazooka_bullet_pos.position, bazooka_bullet_pos.rotation);
    }

    IEnumerator Bazooka_SkillBullet_Initiate(float t)
    {
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet1);
        yield return new WaitForSeconds(t / 3.0f);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet4);
        yield return new WaitForSeconds(t / 3.0f);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet2);
        yield return new WaitForSeconds(t / 3.0f);
        Bazooka_Bullet_Initiate(bazooka_Skill_bullet3);
        yield return null;
    }
    public void SetRotStart(bool b)
    {
        m_bRotStart = b;
    }

    private void Roll()
    {
        Vector3 rot = this.transform.eulerAngles;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = 0.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = 180.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = -90.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = 90.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = -45.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = 45.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = -135.0f;
            StartCoroutine(Roll_Move(rot));
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            m_Animator.SetTrigger("tRoll");
            rot.y = 135.0f;
            StartCoroutine(Roll_Move(rot));
        }                     
    }

    IEnumerator Roll_Move(Vector3 Rot)
    {
        this.transform.rotation = Quaternion.Euler(Rot);//Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(Rot), 40 * Time.deltaTime);
        Vector3 dir = this.transform.forward;
        float dist = 0.0f;

        while(dist <= 4.0f)
        {
            float delta = 5.0f * Time.deltaTime;
            dist += delta;

            if (dist > 4.0f)
            {
                delta = 4.0f - (dist - delta);
            }

            this.transform.Translate(dir * delta, Space.World);

            yield return null;
        }
    }
}
