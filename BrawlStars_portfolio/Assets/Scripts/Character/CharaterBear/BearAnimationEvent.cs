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
        // weapon.cs: OnShoot에 함수 넣어줌
        OnShoot?.Invoke();
    }
    private void SkillShoot() 
    {
        // weapon.cs: OnSkillShoot에 함수 넣어줌
        OnSkillShoot?.Invoke();
    }
}