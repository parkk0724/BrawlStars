using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUICtrl : MonoBehaviour
{
    private Transform[] arrObjs = new Transform[3];
    private AudioClip StarAudio;
    private TextMeshProUGUI TimeTxt;
    private Button BtnRetry;
    private Button BtnMainMenu;
    private AudioSource BackgroundMuisc;
    private int nGrade = 0;

    private void Start()
    {
        InitTrans();
        SetStars(GameManager.instance.m_fTime);     // ���󱸵� �ҷ��� �Ķ���� GameManager.instance.m_fTime �� �ٲ� ��
        SetClearTime(GameManager.instance.m_fTime); // ���󱸵� �ҷ��� �Ķ���� GameManager.instance.m_fTime �� �ٲ� ��
    }
    private void Update()
    {
        for(int i = 0; i < nGrade; i++)
        {
            arrObjs[i].transform.Rotate(Vector3.up * Time.deltaTime * 100f);
        }
    }

    private void InitTrans()
    {
        // -------------------------------------- ��ü ���� ���� --------------------------------------
        Transform[] TempObjs = GetComponentsInChildren<Transform>(true);

        foreach (Transform trs in TempObjs)
        {
            if (trs.name == "Star1")                { arrObjs[0] = trs; }
            else if (trs.name == "Star2")           { arrObjs[1] = trs; }
            else if (trs.name == "Star3")           { arrObjs[2] = trs; }
            else if (trs.name == "TxtClearTimeNum") { TimeTxt = trs.GetComponent<TextMeshProUGUI>(); }
            else if (trs.name == "BtnRetry")        { BtnRetry = trs.GetComponent<Button>(); }
            else if (trs.name == "BtnMainMenu")     { BtnMainMenu = trs.GetComponent<Button>(); }
        }

        BtnRetry.onClick.AddListener(Retry);
        BtnMainMenu.onClick.AddListener(LoadMainMenu);
    }
    void SetStars(float fTime)
    { 
        // ---------------------------------------- ����ó��(���� Ŭ���� Ÿ��) ------------------------------
        
        while (fTime >= 30.0f && nGrade < 4)
        {
            fTime -= 30.0f;                     // 30�� ���� �� �� ���� ����.
            nGrade++;
        }

        // -------------------------------------- ������ ���� �� ��� --------------------------------------
        for (int i = 0; i < nGrade; i++)         
        {
            arrObjs[i].gameObject.SetActive(true);
            
        }
    }


    void SetClearTime(float fTime) // �Ķ������ float ���� ���� �ð����� ������ִ� �Լ�
    {
        // How to �Ҽ��� �Ʒ� ����
        float fMin = fTime / 60;
        int Min = (int)fMin;

        float Sec = fTime % 60;

        TimeTxt.text = Min + " : " + Sec;
    }
    
    void Retry()
    {
        SceneManager.LoadScene("Browl_Stars");
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }
}

