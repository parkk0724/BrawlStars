using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_1 : Character
{
    // 210709: Junseong Test String.
    // Start is called before the first frame update



    [Header("Moving")]
    public LayerMask Picking_Mask;
    public float Move_Speed = 5.0f;
    public float Rotate_Speed = 0.0f;
    Coroutine move = null;
    Coroutine rotate = null;
    public ParticleSystem m_ptsRevival;
    public GameObject playerDir;
    public GameObject m_objCharacter;
    public GameObject m_uiDie;

    public Text m_tDie;

    [Header("Target")]
    public float rotSpeed = 10.0f;
    public GameObject TargetEffect;
    Transform resultTarget;
    public bool Rotstart = false;
    public float range = 0f;
    public LayerMask enemyLayer = 0;
    private float SmoothSpeed = 100f;
    void Start()
    {
        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;   // Current Hp
        m_fMaxStamina = 3.0f;
        m_fStamina = m_fMaxStamina;
        m_fMaxFever = 10.0f;
        m_fFever = m_fMaxFever;
        m_nATK = 10;
        m_nDEF = 5;
        m_nSkillDamage = 20;
        m_fMoveSpeed = 5.0f;
        m_fAttackSpeed = 1.0f;
        m_fRange = 10.0f;

        // UI: HP, Max
        HealthBar.SetHealth(m_nMaxHP);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Rotstart = true;
        }
        if (Rotstart) LookEnemy();
        if (!m_bDie && m_nHP <= 0) StartCoroutine(Die());
    }

    public override void Move()
    {
        float delta = Move_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(playerDir.transform.forward, delta);
            if (!Rotstart)
                RotaeProcess(playerDir.transform.forward, delta, 1.0f, playerDir.transform.right);
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(-playerDir.transform.forward, delta);
            if (!Rotstart)
                RotaeProcess(-playerDir.transform.forward, delta, -1.0f, playerDir.transform.right);
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(-playerDir.transform.right, delta);
            if (!Rotstart)
                RotaeProcess(-playerDir.transform.right, delta, 1.0f, playerDir.transform.forward);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(playerDir.transform.right, delta);
            if (!Rotstart)
                RotaeProcess(playerDir.transform.right, delta, 1.0f, -playerDir.transform.forward);
        }

        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(((playerDir.transform.forward - playerDir.transform.right).normalized), delta);
            if (!Rotstart)
                RotaeProcess((playerDir.transform.forward - playerDir.transform.right).normalized, delta, 1.0f, (playerDir.transform.forward + playerDir.transform.right).normalized);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(((playerDir.transform.forward + playerDir.transform.right).normalized), delta);
            if (!Rotstart)
                RotaeProcess((playerDir.transform.forward + playerDir.transform.right).normalized, delta, 1.0f, (-playerDir.transform.forward + playerDir.transform.right).normalized);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero(((-playerDir.transform.forward - playerDir.transform.right).normalized), delta);
            if (!Rotstart)
                RotaeProcess((-playerDir.transform.forward - playerDir.transform.right).normalized, delta, 1.0f, (playerDir.transform.forward - playerDir.transform.right).normalized);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            m_Animator.SetBool("bMove", true);
            TransformHero((-playerDir.transform.forward + playerDir.transform.right).normalized, delta);
            if (!Rotstart)
                RotaeProcess((-playerDir.transform.forward + playerDir.transform.right).normalized, delta, -1.0f, (playerDir.transform.forward + playerDir.transform.right).normalized);
        }

        else
        {
            m_Animator.SetBool("bMove", false);
        }
    }


    public override void Attack()
    {

    }

    public override void SkillAttack()
    {

    }

    private void OnCollisionEnter(Collision collision) //콜리젼에 충돌이 발생했을 때
    {
        Debug.Log("CollisionEnter");
        Hit(30);
        //Rigidbody rig = this.GetComponent<Rigidbody>();
        //rig.MovePosition(this.transform.position + this.transform.forward * this.GetComponent<Picking>().Move_Speed * Time.deltaTime); // 충돌시 이동 안되게해주는 처리
        if (move != null) StopCoroutine(move);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();

            Destroy(other.gameObject);
        }
    }
    public override IEnumerator Die()
    {
        Debug.Log("Die");
        m_Animator.SetTrigger("tDie");
        m_bDie = true;
        yield return new WaitForSeconds(2);

        int count = 3;

        m_objCharacter.SetActive(false);
        m_uiDie.gameObject.SetActive(true);

        while (count > 0)
        {
            m_tDie.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        m_uiDie.gameObject.SetActive(false);

        Revival();
    }
    public override void Revival()
    {
        Debug.Log("Revival");
        Debug.Log("m_voriginPos : " + m_vOriginPos);
        StartCoroutine(RevivalEffect());
        this.transform.position = m_vOriginPos;
        this.transform.rotation = Quaternion.Euler(m_vOriginRot);
        m_nHP = m_nMaxHP;

        m_objCharacter.SetActive(true);
        m_bDie = false;
        m_Animator.SetTrigger("tRevival");
    }
    IEnumerator RevivalEffect()
    {
        m_ptsRevival.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        m_ptsRevival.gameObject.SetActive(false);
    }
    void TransformHero(Vector3 playerdir, float delta) // 움직임
    {
        this.transform.Translate(playerdir * delta, Space.World);
    }

    void RotaeProcess(Vector3 playerdir, float delta, float movedir, Vector3 dir) //로테이션
    {
        float dot = Vector3.Dot(playerdir, this.transform.forward);
        if (dot == 1.0f)
        {
            //this.transform.Translate(this.transform.forward * delta, Space.World);
        }
        else
        {
            float dot1 = Vector3.Dot(dir, this.transform.forward);
            float rdelta = Rotate_Speed * Time.deltaTime;
            float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

            if (eurler - rdelta < 0.0f)
                rdelta = eurler;

            if (dot1 >= 0.0f)
            {
                //this.transform.Translate(playerdir * delta, Space.World);
                this.transform.Rotate(-Vector3.up * movedir * rdelta, Space.World);
            }
            else
            {
                //this.transform.Translate(playerdir * delta, Space.World);
                this.transform.Rotate(Vector3.up * movedir * rdelta, Space.World);
            }
        }
    }
    void LookEnemy()
    {
        Vector3 tagetrot = this.transform.rotation.eulerAngles;
        float rotDir = 1.0f;
        float delta = rotSpeed * Time.deltaTime;
        float Shortdist = 10;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, range, enemyLayer);
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
        resultTarget = shorTarget; // 최종값

        if (resultTarget == null)
        {
            Debug.Log("목표물이 없습니다.");
        }
        else
        {
            Vector3 dir = resultTarget.position - this.transform.position;
            Vector3 Bot = new Vector3(dir.x, 0, dir.z);
            Bot.Normalize();
            dir.Normalize();
            float r = Mathf.Acos(Vector3.Dot(Bot, this.transform.forward)); //라디안
            float angle = 180.0f * r / Mathf.PI; //this
            Debug.Log(angle);
            float r2 = Vector3.Dot(Bot, this.transform.right);
            if (r2 < 0.0f) rotDir = -1.0f;
            if (angle <= 1)
                Rotstart = false;
            Instantiate(TargetEffect, resultTarget.position, Quaternion.Euler(resultTarget.rotation.x + 90, resultTarget.rotation.y, resultTarget.rotation.z));
            while (angle > 0.0f)
            {
                if (angle - delta < 0.0f)
                {
                    delta = angle;
                }
                angle -= delta;
            }
            tagetrot.y += delta * rotDir;
            this.transform.rotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(tagetrot), Time.smoothDeltaTime * Rotate_Speed);

        }
    }
}