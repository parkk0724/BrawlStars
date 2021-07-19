using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JesterAnimationEv : MonoBehaviour
{
    // Start is called before the first frame update
    public event UnityAction OnShot_0 = null;
    public event UnityAction OnShot_1 = null;
    public event UnityAction OnShot_2 = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void evShoot()
    {
        if(OnShot_0 != null)
        {
            evShoot();
        }
    }
    void evShoot_1()
    {
        if (OnShot_1 != null)
        {
            evShoot_1();
        }
    }
    void evShoot_2()
    {
        if (OnShot_2 != null)
        {
            evShoot_2();
        }
    }
}
