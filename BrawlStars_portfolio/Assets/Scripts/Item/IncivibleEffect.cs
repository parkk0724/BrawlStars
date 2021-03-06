using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncivibleEffect : MonoBehaviour
{
    // Start is called before the first frame update 
    [SerializeField] ParticleSystem m_pProtectDir;
    [SerializeField] ParticleSystem m_pProtectBoom;
    [SerializeField] AudioSource m_aShield;
    SoundManager soundManager;
    // Update is called once per frame
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        m_aShield = soundManager.m_InviciblePotion.GetComponent<AudioSource>();
    }
    void Update()
    {
        GameObject Boss = GameObject.Find("BossMonster");
        if (Boss.GetComponentInChildren<BossMonster>())
        {
            if (Boss.GetComponentInChildren<BossMonster>().Getactive())
            {
                initParticle();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossBullet"))
        {
            Destroy(other.gameObject);
            initParticle();
        }
        if(other.gameObject.CompareTag("Monster") && !other.gameObject.GetComponent<BossMonster>())
        {
            if(other.GetComponent<Monster>())
            {
                initParticle();
            }
        }
        if(other.gameObject.name== "BasicAttackPos")
        {
            initParticle();
        }
       
    }
    void initParticle()
    {
        GameObject Dirparticel = Instantiate(m_pProtectDir.gameObject, this.transform.position, this.transform.rotation);
        m_aShield.Play();
        Destroy(Dirparticel, 2f);
    }
}
