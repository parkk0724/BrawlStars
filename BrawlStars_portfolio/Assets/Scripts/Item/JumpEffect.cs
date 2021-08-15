using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class JumpEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    GameObject player;
    public ParticleSystem particle;
    public ParticleSystem DestinationParticle;
    public AudioSource m_aJumpDestination;
    SoundManager soundManager;
    float curtime;
    float jumpdelay;
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        m_aJumpDestination = soundManager.EndJump.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        curtime = 0;
    }
    
    void Update()
    {
        if (DestinationParticle.gameObject.activeSelf)
        {
            curtime += Time.deltaTime;
            if(curtime >1)
            {
                DestinationParticle.gameObject.SetActive(false);
                curtime = 0;
            }
        }
        if(player.activeSelf)
        {
            if (player.GetComponent<Hero>())
            {
                if (!player.GetComponent<Hero>().GetJump())
                {
                    this.transform.position = player.transform.position;
                    particle.gameObject.SetActive(true);
                    jumpdelay += Time.deltaTime;
                }
                else if (player.GetComponent<Hero>().GetJump())
                {
                    particle.gameObject.SetActive(false);

                    //DestinationParticle.gameObject.SetActive(true);
                    //particle.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player.GetComponent<Hero>())
        {
            if (!player.GetComponent<Hero>().GetJump())
            {
                if (particle.gameObject.activeSelf)
                {
                    if (other.gameObject.CompareTag("Ground"))
                    {
                        if (Jumpdelaytime())
                        {
                            m_aJumpDestination.Play();
                            DestinationParticle.gameObject.SetActive(true);
                            jumpdelay = 0;
                        }
                    }
                }
            }
        }
    }
    bool Jumpdelaytime()
    {
        if(jumpdelay > 0.3f)
        {
            return true;
        }
        return false;
    }
}