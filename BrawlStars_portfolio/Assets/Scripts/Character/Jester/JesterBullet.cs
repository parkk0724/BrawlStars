using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JesterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    JesterWeapon jwaepon;
    private float m_fDamage = 0;
    public UnityAction Fever = null;
    void Start()
    {
        
        SetDamage(10f);
        Destroy(this.gameObject, 2f);
        rigid = GetComponent<Rigidbody>();
        jwaepon = FindObjectOfType<JesterWeapon>();
    }
    public void SetDamage(float f) { m_fDamage = f; }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * 30f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Hero chaar = GameObject.FindWithTag("Player").GetComponent<Hero>();
            chaar.FeverUp();
            // other.GetComponent<Monster>()?.Hit((int)m_fDamage + Random.Range(-5,5), new Color(0, 0, 0, 1));
            if (other.GetComponent<Monster>())
            {
                other.GetComponent<Monster>().Hit((int)m_fDamage + Random.Range(-5, 5), Color.red);
               

            }
        }
        if(other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
    public void Feverup()
    {
        if (Fever != null)
        {
            Fever();
        }
    }
}
