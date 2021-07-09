using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Crash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) //콜리젼에 충돌이 발생했을 때
    {        
        Rigidbody rig = this.GetComponent<Rigidbody>();
        rig.MovePosition(this.transform.position + this.transform.forward * this.GetComponent<Picking>().Move_Speed * Time.deltaTime); // 충돌시 이동 안되게해주는 처리

        //this.GetComponentInChildren<Animator>().SetTrigger("MoveCrash");
    }
}
