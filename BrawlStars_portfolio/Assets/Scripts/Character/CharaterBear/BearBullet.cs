using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall") 
            Destroy(gameObject);
    }
}
