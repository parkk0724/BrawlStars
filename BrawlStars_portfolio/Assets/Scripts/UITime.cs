using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITime : MonoBehaviour
{
    TMPro.TMP_Text m_text;

    private void Awake()
    {
        m_text = GetComponent<TMPro.TMP_Text>();
    }

    private void Update()
    {
        int min = (int)GameManager.instance.m_fTime / 60;
        int sec = (int)GameManager.instance.m_fTime % 60;

        if (sec < 10) m_text.text = min.ToString() + " : 0" + sec.ToString();
        else m_text.text = min.ToString() + " : " + sec.ToString();
    }
}
