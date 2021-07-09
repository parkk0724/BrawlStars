using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    private void OnAnimatorMove()
    {
        this.transform.parent.Translate(this.GetComponent<Animator>().deltaPosition, Space.World); // 애니메이션 위치 값                                                                                                   
        this.transform.parent.Rotate(this.GetComponent<Animator>().deltaRotation.eulerAngles, Space.World); // 애니메이션 회전 값    
    }
}
