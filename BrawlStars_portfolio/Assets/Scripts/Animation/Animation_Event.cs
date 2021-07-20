using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animation_Event : MonoBehaviour
{
    [Header("Bazooka")]    
    public UnityAction bazooka_basic_fire = null;
    public UnityAction bazooka_skill_fire = null;
    public GameObject bazooka_fire_effect;
    public Transform bazooka_fire_effect_pos;
    public GameObject run_effect;
    public Transform Lfoot_pos;
    public Transform Rfoot_pos;

    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;

    // Start is called before the first frame update

    private void Run_Effect_L()
    {
        GameObject fire_effect = Instantiate(run_effect, Lfoot_pos.position, Quaternion.identity);
    }
    private void Run_Effect_R()
    {
        GameObject fire_effect = Instantiate(run_effect, Rfoot_pos.position, Quaternion.identity);
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
}