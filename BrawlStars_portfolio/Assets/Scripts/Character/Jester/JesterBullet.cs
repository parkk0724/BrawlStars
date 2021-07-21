using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    JesterWeapon jwaepon;
    Monster monster;
    private float m_fDamage = 0;
    void Start()
    {
        SetDamage(10f);
        Destroy(this.gameObject, 2f);
        monster = GetComponent<Monster>();
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
            other.GetComponent<Monster>()?.Hit((int)m_fDamage, new Color(0, 0, 0, 1));
          

        }
        if(other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
