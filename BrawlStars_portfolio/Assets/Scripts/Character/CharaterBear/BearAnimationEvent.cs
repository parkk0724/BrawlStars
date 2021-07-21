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
        OnShoot?.Invoke();
    }
    private void SkillShoot()
    {
        OnSkillShoot?.Invoke();
    }
}