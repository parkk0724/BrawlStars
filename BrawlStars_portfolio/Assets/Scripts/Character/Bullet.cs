using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int nBulletDamage = 0;
    public float fBulletSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //------------------------- Test용 코드 지워야함 -------------------------
        this.transform.Translate(-this.transform.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) //리지드바디가 있는 게 들어왔을 때
    {
        if(other.tag.Equals("Player"))
        { 
        other.GetComponentInChildren<Hero>().Hit(nBulletDamage);
        Destroy(this.gameObject);
        }
    }
}
