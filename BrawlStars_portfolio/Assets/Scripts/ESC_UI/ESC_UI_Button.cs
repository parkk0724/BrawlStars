using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC_UI_Button : MonoBehaviour
{
    public void OnClick_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClick_Continue()
    {
        Time.timeScale = 1.0f;
        ESC_UI.Instance.Exit_UI();
    }
}
