using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinMenu : MonoBehaviour
{
    public GameObject Selected_Effect;

    GameObject Soldier;
    GameObject BoxMan;
    GameObject Bear;
    GameObject Jester;       
    private void Start()
    {
        Soldier = GameObject.Find("Soldier_Select");
        BoxMan = GameObject.Find("BoxMan_Select");
        Bear = GameObject.Find("Bear_Select");
        Jester = GameObject.Find("Jester_Select");
    }
    public void OnClick_CharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void OnClick_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# else
        Application.Quit();
#endif
    }

    public void OnClick_Start()
    {
        SceneManager.LoadScene("Browl_Stars");
    }

    public void Soldier_Select()
    {
        Soldier.GetComponent<Animator>().SetBool("bSelect", true);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);
    }

    public void BoxMan_Select()
    {
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", true);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);
    }

    public void Bear_Select()
    {
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", true);
        Jester.GetComponent<Animator>().SetBool("bSelect", false);
    }

    public void Jester_Select()
    {
        Soldier.GetComponent<Animator>().SetBool("bSelect", false);
        BoxMan.GetComponent<Animator>().SetBool("bSelect", false);
        Bear.GetComponent<Animator>().SetBool("bSelect", false);
        Jester.GetComponent<Animator>().SetBool("bSelect", true);
    }
}
