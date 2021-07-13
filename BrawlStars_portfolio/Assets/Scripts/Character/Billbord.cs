using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billbord : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + cam.position); //  수정할 것: 카메라 앞방향으로 하니까 기울어서 문제 생김.
    }
}