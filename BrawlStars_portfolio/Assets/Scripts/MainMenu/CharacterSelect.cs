using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject Selected_Effect;

    GameObject Selected_Character = null;    
    GameObject[] Characters;
 
    // Start is called before the first frame update
    void Start()
    {
        Characters[0] = GameObject.Find("Soldier_Select");
        Characters[1] = GameObject.Find("BoxMan_Select");
        Characters[2] = GameObject.Find("Bear_Select");
        Characters[3] = GameObject.Find("Jester_Select");
    }

    // Update is called once per frame
    void Update()
    {
        //Selected();
    }

    private void Selected(GameObject obj)
    {        
        Instantiate(Selected_Effect, obj.transform.position, Quaternion.identity);
    }
}
