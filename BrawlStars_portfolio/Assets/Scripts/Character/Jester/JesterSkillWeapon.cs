using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterSkillWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    public GameObject effect;
    JesterSkill jesterskill;
    AudioSource m_knifeSound;
    void Start()
    {
        m_knifeSound = GameObject.Find("knifeSound").GetComponent<AudioSource>();
        jesterskill = GetComponentInParent<JesterSkill>();
        damage = 10f;
    }
    private void Update()
    {

        if (jesterskill.Attackcollider.enabled)
        {
            effect.gameObject.SetActive(true);
            
        }
        else
        {
            effect.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Monster>())
        {
            other.GetComponent<Monster>().Hit((int)damage, Color.red);
            if(effect.activeSelf)
            {
                effect.transform.position = this.transform.position;
                m_knifeSound.Play();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {

    }

}
