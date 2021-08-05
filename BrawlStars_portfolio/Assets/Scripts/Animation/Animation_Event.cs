using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void BossMonsterFire(int n);
public class Animation_Event : MonoBehaviour
{
    [Header("Sound")]
    AudioSource WalkSound = null;

    [Header("Bazooka")]
    public UnityAction bazooka_basic_fire = null;
    public UnityAction bazooka_skill_fire = null;
    public GameObject bazooka_fire_effect;
    public Transform bazooka_fire_effect_pos;
    public GameObject run_effect;
    public Transform Lfoot_pos;
    public Transform Rfoot_pos;

    [Header("BoxMan")]
    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;

    [Header("BossMonster")]
    public UnityAction endAttack = null;
    public UnityAction skill_attack2 = null;
    public UnityAction basicAttack = null;
    public BossMonsterFire bossMonFire = null;
    // Start is called before the first frame update

    Animator myAnimator;

    private void Awake()
    {
        WalkSound = GameObject.Find("FootWalk").GetComponent<AudioSource>();
    }
    private void Run_Effect_L()
    {
        GameObject fire_effect = Instantiate(run_effect, Lfoot_pos.position, Quaternion.identity);
        WalkSound.Play();
    }
    private void Run_Effect_R()
    {
        GameObject fire_effect = Instantiate(run_effect, Rfoot_pos.position, Quaternion.identity);
        WalkSound.Play();
    }
    private void Bazooka_Effect()
    {
        GameObject fire_effect = Instantiate(bazooka_fire_effect, bazooka_fire_effect_pos.position, bazooka_fire_effect_pos.rotation);
    }
    private void Bazooka_Basic_Fire()
    {
        bazooka_basic_fire?.Invoke();
    }

    private void Bazooka_Skill_Fire()
    {
        bazooka_skill_fire?.Invoke();
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }
    private void SkillShoot()
    {
        OnSkillShoot?.Invoke();
    }

    private void EndAttack()
    {
        endAttack?.Invoke();
    }

    private void Skill_Attack2()
    {
        skill_attack2?.Invoke();
    }

    private void PlaySpeed(float speed)
    {
        myAnimator = this.GetComponent<Animator>();

        myAnimator.SetFloat("PlaySpeed", speed);
    }
    private void BossMonFire(int n)
    {
        bossMonFire?.Invoke(n);
    }

    private void BasicAttack()
    {
        basicAttack?.Invoke();
    }

    private void Sound_SetActive_True(GameObject sound)
    {
        sound.SetActive(true);
    }

    static private void Sound_SetActive_False(GameObject sound)
    {
        sound.SetActive(false);
    }
}