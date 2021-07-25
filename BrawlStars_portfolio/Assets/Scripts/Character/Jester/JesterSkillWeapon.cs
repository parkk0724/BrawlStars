using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterSkillWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;

    void Start()
    {
        damage = 10f;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Monster>())
        {
            other.GetComponent<Monster>().Hit((int)damage, Color.red);
        }
    }
}
