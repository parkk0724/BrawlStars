using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESC_UI : MonoBehaviour
{    
    [HideInInspector]
    public GameObject BGM_Bar;
    [HideInInspector]
    public GameObject SE_Bar;

    [SerializeField]
    private TMPro.TMP_Text BGM_Text;
    [SerializeField]
    private TMPro.TMP_Text SE_Text;

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

    private void Awake()
    {
        BGM_Bar = GameObject.Find("BGM_Bar");
        BGM_Bar.GetComponent<Slider>().value = 1.0f;
        SE_Bar = GameObject.Find("SideSound_Bar");
        SE_Bar.GetComponent<Slider>().value = 0.3f;
    }

    private void Update()
    {
        BGM_ValueText();
        SE_ValueText();
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

    private void BGM_ValueText()
    {
        float Value = BGM_Bar.GetComponent<Slider>().value;
        Value *= 100.0f;
        int ShowValue = (int)Value;
        BGM_Text.text = ShowValue.ToString();
    }

    private void SE_ValueText()
    {
        float Value = SE_Bar.GetComponent<Slider>().value;
        Value *= 100.0f;
        int ShowValue = (int)Value;
        SE_Text.text = ShowValue.ToString();
    }
}
