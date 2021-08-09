using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class BoxManWeapon : MonoBehaviour
{
    [SerializeField] GameObject m_objBullet = null;
    [SerializeField] GameObject m_objShootPos = null;
    Animator m_Animator = null;
    Transform m_tfHero = null;
    float m_fRange = 0.0f;
    float m_fATK = 0.0f;
    public UnityAction OnFeverUp = null;
    public UnityAction OnStaminaUp = null;
    public UnityAction OnRotStartFalse = null;

    [SerializeField] Dictionary<string, AudioClip> m_dirAudioClips = new Dictionary<string, AudioClip>();
    AudioSource m_audioSource = null;
    public void SetATK(float f) { m_fATK = f; }
    public void SetRange(float f) { m_fRange = f; }
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        foreach (AudioClip clip in Resources.LoadAll<AudioClip>("Prefabs/Sound/BoxMan/BoxManWeapon")) m_dirAudioClips.Add(clip.name, clip);
        m_audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

        this.GetComponentInParent<Animation_Event>().OnShoot = Shoot;
        this.GetComponentInParent<Animation_Event>().OnSkillShoot = SkillShoot;
        m_tfHero = GetComponentInParent<BoxMan>().transform;

    }
    void Shoot()
    {
        InitShoot(false);

        m_audioSource.clip = m_dirAudioClips["BoxManBasic"];
        m_audioSource.Play();
    }

    void SkillShoot()
    {
        InitShoot(true);

        m_audioSource.clip = m_dirAudioClips["BoxManSkill"];
        m_audioSource.Play();
    }

    void InitShoot(bool isSkill)
    {
        m_Animator.SetTrigger("Shoot");
        Vector3 shootPos = m_objShootPos.transform.position;
        shootPos.y = 0.5f;
        GameObject obj = Instantiate(m_objBullet, shootPos, m_objShootPos.transform.rotation);
        BoxManBullet bullet = obj.GetComponent<BoxManBullet>();
        bullet.SetDistance(m_fRange - Vector3.Distance(this.transform.position, m_tfHero.position));
        bullet.SetPosParent(m_tfHero);
        bullet.OnFeverUp = OnFeverUp;
        bullet.OnStaminaUp = OnStaminaUp;
        bullet.SetDamage(m_fATK);
        if(isSkill) bullet.OnSkill();

        OnRotStartFalse?.Invoke();
    }
}
