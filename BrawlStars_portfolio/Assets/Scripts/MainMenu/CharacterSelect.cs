using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    GameObject Soldier;
    GameObject BoxMan;
    GameObject Bear;
    GameObject Jester;
    // Start is called before the first frame update
    void Start()
    {
        Soldier = GameObject.Find("Soldier_Select");
        BoxMan = GameObject.Find("BoxMan_Select");
        Bear = GameObject.Find("Bear_Select");
        Jester = GameObject.Find("Jester_Select");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Selected()
    {
        
    }
}
