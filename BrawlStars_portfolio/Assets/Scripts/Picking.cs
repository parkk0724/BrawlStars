using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    public LayerMask Picking_Mask;
    public float Move_Speed = 5.0f;

    Coroutine move = null;
    Coroutine rotate = null;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Picking_Mask))
            {               
               if (move != null) StopCoroutine(move);
               move = StartCoroutine(Move(hit.point, Move_Speed));

               if (rotate != null) StopCoroutine(rotate);
               rotate = StartCoroutine(Rotate(hit.point, 360.0f));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponentInChildren<Animator>().SetTrigger("Jump");
            this.GetComponentInChildren<Animator>().SetBool("Air_Attack", true);
        }
    }

    private void OnCollisionEnter(Collision collision) //콜리젼에 충돌이 발생했을 때
    {
        //Rigidbody rig = this.GetComponent<Rigidbody>();
        //rig.MovePosition(this.transform.position + this.transform.forward * this.GetComponent<Picking>().Move_Speed * Time.deltaTime); // 충돌시 이동 안되게해주는 처리
        Debug.Log("yes");
        if(move != null) StopCoroutine(move);

        this.GetComponentInChildren<Animator>().SetTrigger("MoveCrash");
        this.GetComponentInChildren<Animator>().SetBool("isMove", false);
        this.GetComponentInChildren<Animator>().SetBool("isRun", false);
    }

    IEnumerator Move(Vector3 point, float speed)
    {
        this.GetComponentInChildren<Animator>().SetBool("isMove", true);
        Vector3 dir = point - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
           

        while (dist > 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                this.GetComponentInChildren<Animator>().SetBool("isRun", true);
                speed = 1.5f * speed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))            
                this.GetComponentInChildren<Animator>().SetBool("isRun", false);                
              
            if (Input.GetKeyDown(KeyCode.Space))
                this.GetComponentInChildren<Animator>().SetBool("isJump", true);

            if (Input.GetKeyUp(KeyCode.Space))
                this.GetComponentInChildren<Animator>().SetBool("isJump", false);

            float delta = speed * Time.deltaTime;
            dist -= delta;

            if (dist < 0.0f)
            {
                delta = dist;
                dist = 0.0f;
            }

            this.transform.Translate(dir * delta, Space.World);                       

            yield return null;
        }

        move = null;
        this.GetComponentInChildren<Animator>().SetBool("isMove", false);
        this.GetComponentInChildren<Animator>().SetBool("isRun", false);
    }
    IEnumerator Rotate(Vector3 point, float speed)
    {
        Vector3 dir = point - this.transform.position;
        dir.Normalize();
        float dot = Vector3.Dot(this.transform.forward, dir);
        float rdot = Vector3.Dot(this.transform.right, dir);
        float r = Mathf.Acos(dot);
        float o = 180.0f * r / Mathf.PI;
        float angle = o;
        float rotdir = 1.0f;

        if (rdot < 0.0f) // 플레이어 기준 왼쪽으로 돌아야함
            rotdir = -1.0f;
        
        while (angle > 0.0f)
        {
            float delta = Time.deltaTime * speed;
           
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;

            this.transform.Rotate(this.transform.up * delta * rotdir, Space.World);

            yield return null;
        }

        rotate = null;
    }
}
