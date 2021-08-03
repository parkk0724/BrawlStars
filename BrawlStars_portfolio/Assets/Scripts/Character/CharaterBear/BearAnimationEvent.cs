using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BearAnimationEvent : MonoBehaviour
{
    public UnityAction OnShoot = null;
    public UnityAction OnSkillShoot = null;

    private void Shoot()
    {
        // weapon.cs: OnShoot�� �Լ� �־���
        OnShoot?.Invoke();
    }
    private void SkillShoot() 
    {
        // weapon.cs: OnSkillShoot�� �Լ� �־���
        OnSkillShoot?.Invoke();
    }
}