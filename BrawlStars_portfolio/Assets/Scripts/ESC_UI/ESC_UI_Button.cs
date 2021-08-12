using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC_UI_Button : MonoBehaviour
{
    private GameObject ClickSound;

    private void Awake()
    {
        ClickSound = GameObject.Find("ClickSound");
    }
    public void OnClick_Quit()
    {
        PlayClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClick_Continue()
    {
        PlayClickSound();
        Time.timeScale = 1.0f;
        ESC_UI.Instance.Exit_UI();
    }

    public void PlayClickSound()
    {
        ClickSound.GetComponent<AudioSource>().Play();
    }
}
