using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JesterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    JesterWeapon jwaepon;
    //Hero hero;
    private float m_fDamage = 0;
    public UnityAction Fever = null;
    public GameObject HitEffect = null;
    float Speed = 20f;
    Vector3 Originpos;
    void Start()
    {
        //hero = GetComponent<Hero>();
        Destroy(this.gameObject, 2f);
        rigid = GetComponent<Rigidbody>();
        jwaepon = FindObjectOfType<JesterWeapon>();
        Originpos = this.transform.position;
    }
    public void SetDamage(float f) { m_fDamage = f; }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = transform.forward * 20f;
        Vector3 curPos = this.transform.position;
        Vector3 nextPos = curPos + this.transform.forward * Speed * Time.deltaTime;
        this.transform.position = nextPos;
        float dist = Vector3.Distance(Originpos, nextPos);
        if(dist > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {

            //Jester jester = GameObject.FindWithTag("Player").GetComponent<Jester>();
            GameObject obj = GameObject.Find("Jester");
            Jester jester = obj.GetComponent<Jester>();
            jester.FeverUp();
            if (other.GetComponent<Monster>())
            {
                other.GetComponent<Monster>().Hit(jester.GetATK() + Random.Range(-5, 5), Color.red);

            }
        }
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            GameObject obj = Instantiate(HitEffect, other.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            Destroy(obj.gameObject, 0.2f);
        }
    }
}
