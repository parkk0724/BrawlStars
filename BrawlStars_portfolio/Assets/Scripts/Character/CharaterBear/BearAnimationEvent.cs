using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BearAnimationEvent : MonoBehaviour
{
    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;
    //public GameObject run_effect;
    //public Transform Lfoot_pos;
    //public Transform Rfoot_pos;

    //[Header("Sound")]
    //AudioSource WalkSound = null;

    //private void Awake()
    //{
    //    WalkSound = GameObject.Find("FootWalk").GetComponent<AudioSource>();
    //}
    private void Shoot()
    {
        // weapon.cs: OnShoot에 함수 넣어줌
        OnShoot?.Invoke();
    }
    private void SkillShoot() 
    {
        // weapon.cs: OnSkillShoot에 함수 넣어줌
        OnSkillShoot?.Invoke();
    }
    //private void Run_Effect_L()
    //{
    //    GameObject fire_effect = Instantiate(run_effect, Lfoot_pos.position, Quaternion.identity);
    //    WalkSound.Play();
    //}
    //private void Run_Effect_R()
    //{
    //    GameObject fire_effect = Instantiate(run_effect, Rfoot_pos.position, Quaternion.identity);
    //    WalkSound.Play();
    //}
}