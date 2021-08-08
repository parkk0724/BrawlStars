using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject PlayStart;
    public GameObject Playing;
    public GameObject Playing_Angry;

    public GameObject Portal;
    public GameObject PortalMove;
    public GameObject JumpMove;

    public GameObject FootWalk;

    //Bazooka

    public GameObject BazookaFire;
    public GameObject BazookaExplosion;
    public GameObject SkillBazooka;


    void Start()
    {
        PlayStart = GameObject.Find("Start");
        Playing = GameObject.Find("Playing");
        Portal = GameObject.Find("Portal");
        PortalMove = GameObject.Find("PortalStart");
        JumpMove = GameObject.Find("JumpStart");
        Playing_Angry = GameObject.Find("Playing(angry)");
        FootWalk = GameObject.Find("FootWalk");
        BazookaFire = GameObject.Find("BazookaFire");
        BazookaExplosion = GameObject.Find("BazookaExplosion");
        SkillBazooka = GameObject.Find("SkillBazooka");
    }

    public void PlaySound(GameObject obj)
    {
        obj.GetComponent<AudioSource>().Play();
    }    
}
