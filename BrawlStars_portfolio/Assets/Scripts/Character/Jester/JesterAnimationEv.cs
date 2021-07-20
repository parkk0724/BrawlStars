using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JesterAnimationEv : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityAction OnShot_0 = null;
    public UnityAction OnShot_1 = null;
    public UnityAction OnShot_2 = null;
    public UnityAction Anim_end = null;
    public UnityAction FireEffect = null;
    public GameObject Effect;
    public Transform EffectPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void evShoot()
    {
        OnShot_0?.Invoke();
        ///if (OnShot_0 != null)
        ///{
        ///    OnShot_0();
        ///}
    }
    void evShoot_1()
    {
        OnShot_1?.Invoke();
        //if (OnShot_1 != null)
        //{
        //    OnShot_1();
        //}
    }
    void evShoot_2()
    {
        OnShot_2?.Invoke();
        //if (OnShot_2 != null)
        //{
        //    OnShot_2();
        //}
    }
    void AnimEnd()
    {
        Anim_end?.Invoke();

    }
    void Fireeffect()
    {
        if (FireEffect != null)
            FireEffect();
    }
}
