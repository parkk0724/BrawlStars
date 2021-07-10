using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Moving : MonoBehaviour
{
    public GameObject playercamera;
    public float speed = 0.0f;
    public bool startmove = true;
    Coroutine cameramove = null;

    float xAxis;
    float yAxis;
    void Start()
    {
        if (startmove == true)
            cameramove = StartCoroutine(StartCameraMoving());
        else
        {
            this.transform.position = playercamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        float power = yAxis / Mathf.Sqrt(Mathf.Pow(yAxis, 2) + Mathf.Pow(xAxis, 2));
        
        if (cameramove == null)
        {
            this.transform.Translate(Vector3.forward * power * 4.0f * Time.deltaTime);
        }
    }

    IEnumerator StartCameraMoving()
    {
        Vector3 TargetPos = this.transform.position;
        Vector3 dir = playercamera.transform.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while(dist > 0.0f)
        {
            float delta = speed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            this.transform.Translate(dir * delta);
            //TargetPos += dir * delta;
            //this.transform.position = Vector3.Lerp(this.transform.position, TargetPos, Time.deltaTime * 1.0f);
            //
            //if (Vector3.Distance(this.transform.position, TargetPos) < 0.1f)
            //{
            //    this.transform.position = TargetPos;
            //}
            yield return null;
        }
        cameramove = null;
    }
}
