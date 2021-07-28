using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnTriggerEnterHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Character>().Hit(10, Color.red);
        }
    }
}
