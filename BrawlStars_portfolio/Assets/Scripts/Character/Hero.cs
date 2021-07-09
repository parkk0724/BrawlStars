using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : Character
{
    // Start is called before the first frame update
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
    void Start()
    {

        m_Animator = this.GetComponentInChildren<Animator>();
        m_vOriginPos = this.transform.position;
        m_vOriginRot = this.transform.rotation.eulerAngles;
        m_nMaxHP = 100;
        m_nHP = m_nMaxHP;
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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!m_bDie && m_nHP <= 0) StartCoroutine(Die());
    }

    public override void Move()
    {
        float delta = Move_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot(playerDir.transform.forward, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot(playerDir.transform.right, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate(playerDir.transform.forward * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate(playerDir.transform.forward * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot(-playerDir.transform.forward, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot(playerDir.transform.right, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate(-playerDir.transform.forward * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate(-playerDir.transform.forward * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot(-playerDir.transform.right, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot(playerDir.transform.forward, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate(-playerDir.transform.right * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate(-playerDir.transform.right * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot(playerDir.transform.right, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot(-playerDir.transform.forward, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate(playerDir.transform.right * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate(playerDir.transform.right * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot((playerDir.transform.forward - playerDir.transform.right).normalized, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot((playerDir.transform.forward + playerDir.transform.right).normalized, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate((playerDir.transform.forward - playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate((playerDir.transform.forward - playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot((playerDir.transform.forward + playerDir.transform.right).normalized, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot((-playerDir.transform.forward + playerDir.transform.right).normalized, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate((playerDir.transform.forward + playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate((playerDir.transform.forward + playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot((-playerDir.transform.forward - playerDir.transform.right).normalized, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot((playerDir.transform.forward - playerDir.transform.right).normalized, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate((-playerDir.transform.forward - playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate((-playerDir.transform.forward - playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
            }
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            m_Animator.SetBool("bMove", true);
            float dot = Vector3.Dot((-playerDir.transform.forward + playerDir.transform.right).normalized, this.transform.forward);
            if (dot == 1.0f)
            {
                this.transform.Translate(this.transform.forward * delta, Space.World);
            }
            else
            {
                float dot1 = Vector3.Dot((playerDir.transform.forward + playerDir.transform.right).normalized, this.transform.forward);
                float rdelta = Rotate_Speed * Time.deltaTime;
                float eurler = 180.0f * (Mathf.Acos(dot) / Mathf.PI);

                if (eurler - rdelta < 0.0f)
                    rdelta = eurler;

                if (dot1 >= 0.0f)
                {
                    this.transform.Translate((-playerDir.transform.forward + playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(Vector3.up * rdelta, Space.World);
                }
                else
                {
                    this.transform.Translate((-playerDir.transform.forward + playerDir.transform.right).normalized * delta, Space.World);
                    this.transform.Rotate(-Vector3.up * rdelta, Space.World);
                }
            }
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
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
        //Rigidbody rig = this.GetComponent<Rigidbody>();
        //rig.MovePosition(this.transform.position + this.transform.forward * this.GetComponent<Picking>().Move_Speed * Time.deltaTime); // 충돌시 이동 안되게해주는 처리
        if (move != null) StopCoroutine(move);
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
}
