using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    private void OnAnimatorMove()
    {
        this.transform.parent.Translate(this.GetComponent<Animator>().deltaPosition, Space.World); // �ִϸ��̼� ��ġ ��                                                                                                   
        this.transform.parent.Rotate(this.GetComponent<Animator>().deltaRotation.eulerAngles, Space.World); // �ִϸ��̼� ȸ�� ��    
    }
}
