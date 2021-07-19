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

    void Start()
    {
        
    }

    // Update is called once per frame
    void evShoot()
    {
        OnShot_0?.Invoke();
        ///if (OnShot_0 != null)
        ///{
        ///    evShoot();
        ///}
    }
    void evShoot_1()
    {
        OnShot_1?.Invoke();
        //if (OnShot_1 != null)
        //{
        //    evShoot_1();
        //}
    }
    void evShoot_2()
    {
        OnShot_2?.Invoke();
        //if (OnShot_2 != null)
        //{
        //    evShoot_2();
        //}
    }
    void AnimEnd()
    {
        Anim_end?.Invoke();

    }
}
