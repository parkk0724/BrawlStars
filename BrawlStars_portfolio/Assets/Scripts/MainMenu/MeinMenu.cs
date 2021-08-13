using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeinMenu : MonoBehaviour
{
    public GameObject MainSound;
    public GameObject ClickSound;

    public GameObject Selected_Effect;

    private bool selected_character = false;

    GameObject Warnning;
    GameObject Soldier;
    GameObject BoxMan;
    GameObject Bear;
    GameObject Jester;

    GameObject Soldier_Arrow;
    GameObject BoxMan_Arrow;
    GameObject Bear_Arrow;
    GameObject Jester_Arrow;
    private void Start()
    {
        MainSound.SetActive(true);
        Soldier = GameObject.Find("Soldier_Select");
        BoxMan = GameObject.Find("BoxMan_Select");
        Bear = GameObject.Find("Bear_Select");
        Jester = GameObject.Find("Jester_Select");
        Warnning = GameObject.Find("Warn");

        Soldier_Arrow = GameObject.Find("SoldierArrow");
        BoxMan_Arrow = GameObject.Find("BoxManArrow");
        Bear_Arrow = GameObject.Find("BearArrow");
        Jester_Arrow = GameObject.Find("JesterArrow");
    }
    public void OnClick_CharacterSelect()
    {
        ClickSound.SetActive(true);
        SceneManager.LoadScene("CharacterSelect");
    }
    public void OnClick_Quit()
    {
        ClickSound.SetActive(true);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# else
        Application.Quit();
#endif
    }

    public void OnClick_Start()
    {
        ClickSound.GetComponent<AudioSource>().Play();

        if (selected_character)
            SceneManager.LoadScene("Loading");
        else
        {
            //Warnning_UI.instance.UI_Play();
            Warnning.GetComponent<Canvas>().enabled = true;
        }
    }
    public void OnClick_Close()
    {
        ClickSound.GetComponent<AudioSource>().Play();
        Warnning.GetComponent<Canvas>().enabled = false;
    }

    public void Soldier_Select()
    {
        ClickSound.GetComponent<AudioSource>().Play();
        selected_character = true;
        DataManager.instance.select_character = DataManager.instance.Characters[101];
        Soldier.GetComponent<Animator>().SetBool("bSelect", true);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);

        Soldier_Arrow.GetComponent<RawImage>().enabled = true;
        BoxMan_Arrow.GetComponent<RawImage>().enabled = false;
        Bear_Arrow.GetComponent<RawImage>().enabled = false;
        Jester_Arrow.GetComponent<RawImage>().enabled = false;
    }

    public void BoxMan_Select()
    {
        ClickSound.GetComponent<AudioSource>().Play();
        selected_character = true;
        DataManager.instance.select_character = DataManager.instance.Characters[102];
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", true);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);

        Soldier_Arrow.GetComponent<RawImage>().enabled = false;
        BoxMan_Arrow.GetComponent<RawImage>().enabled = true;
        Bear_Arrow.GetComponent<RawImage>().enabled = false;
        Jester_Arrow.GetComponent<RawImage>().enabled = false;
    }

    public void Bear_Select()
    {
        ClickSound.GetComponent<AudioSource>().Play();
        selected_character = true;
        DataManager.instance.select_character = DataManager.instance.Characters[103];
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", true);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);

        Soldier_Arrow.GetComponent<RawImage>().enabled = false;
        BoxMan_Arrow.GetComponent<RawImage>().enabled = false;
        Bear_Arrow.GetComponent<RawImage>().enabled = true;
        Jester_Arrow.GetComponent<RawImage>().enabled = false;
    }

    public void Jester_Select()
    {
        ClickSound.GetComponent<AudioSource>().Play();
        selected_character = true;
        DataManager.instance.select_character = DataManager.instance.Characters[104];
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", true);

        Soldier_Arrow.GetComponent<RawImage>().enabled = false;
        BoxMan_Arrow.GetComponent<RawImage>().enabled = false;
        Bear_Arrow.GetComponent<RawImage>().enabled = false;
        Jester_Arrow.GetComponent<RawImage>().enabled = true;
    }
}
