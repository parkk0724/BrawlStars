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

    private void OnCollisionEnter(Collision collision) //�ݸ����� �浹�� �߻����� ��
    {        
        Rigidbody rig = this.GetComponent<Rigidbody>();
        rig.MovePosition(this.transform.position + this.transform.forward * this.GetComponent<Picking>().Move_Speed * Time.deltaTime); // �浹�� �̵� �ȵǰ����ִ� ó��

        //this.GetComponentInChildren<Animator>().SetTrigger("MoveCrash");
    }
}
