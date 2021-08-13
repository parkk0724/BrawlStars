using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Arrow : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Arrow_Rotation());
    }

    private IEnumerator Arrow_Rotation()
    {
        while(true)
        {
            this.transform.Rotate(-this.transform.up * Time.deltaTime * 300.0f);

            yield return null;
        }
    }
}
