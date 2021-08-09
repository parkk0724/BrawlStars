using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC_UI : MonoBehaviour
{
    static private ESC_UI _Instance;
    static public ESC_UI Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject esc = GameObject.Find("ESC_UI");//Instantiate (Resources.Load<GameObject>("Prefabs/UI/ESC_UI"), Vector3.zero, Quaternion.identity);
                //esc.transform.parent = GameObject.Find("UI").transform;
                _Instance = esc.GetComponent<ESC_UI>();
            }
    
            return _Instance;
        }
    }

    public void Print_UI()
    {
        GameObject ESC_canvas = GameObject.Find("ESC_UI");
        ESC_canvas.GetComponent<Canvas>().enabled = true;
    }
    public void Exit_UI()
    {
        GameObject ESC_canvas = GameObject.Find("ESC_UI");
        ESC_canvas.GetComponent<Canvas>().enabled = false;
    }
}
