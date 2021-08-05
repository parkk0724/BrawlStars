using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class JumpEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    GameObject player;
    Hero hero;
    ParticleSystem particle;
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        //particle = GetComponent<ParticleSystem>();
        //pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Hero>())
        {
            if (!player.GetComponent<Hero>().GetJump())
            {
                this.transform.position = player.transform.position;
                particle.gameObject.SetActive(true);
                
            }
            else if (player.GetComponent<Hero>().GetJump())
            {
                particle.gameObject.SetActive(false);
                //particle.Stop();
            }
        }
    }
}