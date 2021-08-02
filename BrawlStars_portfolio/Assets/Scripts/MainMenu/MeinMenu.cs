using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinMenu : MonoBehaviour
{
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
        Soldier.GetComponent<Animator>().SetTrigger("tSelect");
    }

    public void BoxMan_Select()
    {
        BoxMan.GetComponent<Animator>().SetTrigger("tSelect");
    }

    public void Bear_Select()
    {
        Bear.GetComponent<Animator>().SetTrigger("tSelect");
    }

    public void Jester_Select()
    {
        Jester.GetComponent<Animator>().SetTrigger("tSelect");
    }
}
