using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject PlayStart;
    public GameObject Brawl;
    public GameObject Playing;
    public GameObject Playing_Angry;

    public GameObject Portal;
    public GameObject PortalMove;
    public GameObject PortalEnd;
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
    public GameObject m_BoomSound;
    public GameObject m_JesterSkill_knife;

    public GameObject m_ItemGet;
    public GameObject m_Ghost;
    public GameObject m_InviciblePotion;
    public GameObject m_AgetInvicible;

    public GameObject m_Win;
    public GameObject m_Lose;

    public GameObject m_BossPunch;
    public GameObject m_BossFirePunch;
    public GameObject m_BossHit;
    public GameObject m_BossSkill;
    public GameObject m_MonsterFollow;


    GameObject BGM;
    GameObject SoundEffect;
    void Start()
    {
        PlayStart = GameObject.Find("Start");
        Brawl = GameObject.Find("Brawl");
        Playing = GameObject.Find("Playing");
        Portal = GameObject.Find("PortalSound");
        PortalMove = GameObject.Find("PortalStart");
        PortalEnd = GameObject.Find("PortalEnd");
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
        m_JesterSkill_knife = GameObject.Find("knifeSound");
        EndJump = GameObject.Find("EndJump");
        m_ItemGet = GameObject.Find("ItemGet");
        m_Ghost = GameObject.Find("GhostSound");
        m_InviciblePotion = GameObject.Find("Shield");
        m_AgetInvicible = GameObject.Find("m_AgetInvicible");
        m_BoomSound = GameObject.Find("JesterBoom");
        m_Win = GameObject.Find("Win");
        m_Lose = GameObject.Find("Lose");
        m_BossPunch = GameObject.Find("BossPunch");
        m_BossFirePunch = GameObject.Find("BossFirePunch");
        m_BossHit = GameObject.Find("BossHit");
        m_BossSkill = GameObject.Find("BossSkill");
        m_MonsterFollow = GameObject.Find("MonsterFollow");

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
