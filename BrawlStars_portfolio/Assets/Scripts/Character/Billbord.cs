using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billbord : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + cam.position); //  ������ ��: ī�޶� �չ������� �ϴϱ� ��� ���� ����.
    }
}