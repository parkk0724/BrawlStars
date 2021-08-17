using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    private AudioSource Click_Sound;

    Canvas Menu;
    private void Start()
    {
        Click_Sound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        Menu = GameObject.Find("Menu").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Menu.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            Click_Sound.Play();
            Menu.enabled = false;
        }
    }
    public void onClickSetting()
    {
        if (!Menu.enabled)
            Click_Sound.Play();
        Menu.enabled = true;
    }

    public void onClickMenu()
    {
        Click_Sound.Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void onClickRetry()
    {
        Click_Sound.Play();
        SceneManager.LoadScene("Loading");
    }

}
