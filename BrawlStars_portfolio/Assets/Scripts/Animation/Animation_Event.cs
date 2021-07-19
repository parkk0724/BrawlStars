using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animation_Event : MonoBehaviour
{
    [Header("Bazooka")]    
    public UnityAction bazooka_basic_fire = null;
    public UnityAction bazooka_skill_fire = null;

    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;
           
    // Start is called before the first frame update
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
