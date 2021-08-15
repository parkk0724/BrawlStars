using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject PlayStart;
    public GameObject Playing;
    public GameObject Playing_Angry;

    public GameObject Portal;
    public GameObject PortalMove;
    public GameObject JumpMove;
    public GameObject EndJump;
    public GameObject FootWalk;

    //Bazooka

    public GameObject BazookaFire;
    public GameObject BazookaExplosion;
    public GameObject SkillBazooka;

    public GameObject Jestershot;
    public GameObject Jestershoteffect;
    public GameObject Jestershoteffect_2;
    public GameObject Jestershoteffect_3;
   


    GameObject BGM;
    GameObject SoundEffect;
    void Start()
    {
        PlayStart = GameObject.Find("Start");
        Playing = GameObject.Find("Playing");
        Portal = GameObject.Find("PortalSound");
        PortalMove = GameObject.Find("PortalStart");
        JumpMove = GameObject.Find("JumpStart");
        Playing_Angry = GameObject.Find("Playing(angry)");
        FootWalk = GameObject.Find("FootWalk");
        BazookaFire = GameObject.Find("BazookaFire");
        BazookaExplosion = GameObject.Find("BazookaExplosion");
        SkillBazooka = GameObject.Find("SkillBazooka");
        Jestershot = GameObject.Find("JesterShot");
        Jestershoteffect = GameObject.Find("JesterShoteffect");
        Jestershoteffect_2 = GameObject.Find("JesterShoteffect_2");
        Jestershoteffect_3 = GameObject.Find("JesterShoteffect_3");
        EndJump = GameObject.Find("EndJump");

        BGM = GameObject.Find("BGM");
        SoundEffect = GameObject.Find("SoundEffect");
    }

    private void Update()
    {
        BGM_VolumeUpdate();
        SoundEffect_VolumeUpdate();
    }

    public void PlaySound(GameObject obj)
    {
        obj.GetComponent<AudioSource>().Play();
    }
    
    private void BGM_VolumeUpdate()
    {
        BGM.GetComponentInChildren<AudioSource>().volume = ESC_UI.Instance.BGM_Bar.GetComponent<Slider>().value;
    }

    private void SoundEffect_VolumeUpdate()
    {
        AudioSource[] SE;
        SE = SoundEffect.GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < SE.Length; i++)
        {
            SE[i].volume = ESC_UI.Instance.SE_Bar.GetComponent<Slider>().value;
        }
    }
}
