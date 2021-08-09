using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUICtrl : MonoBehaviour
{
    private Transform[] objs;
    private AudioClip StarAudio;
    private TextMeshProUGUI TimeTxt;

    private void Start()
    {
        SetStars(60f);
        SetClearTime(90f);
    }

    void SetStars(float fTime)
    {    
        // ��Ȱ�� ������Ʈ ���
        objs = GetComponentsInChildren<Transform>(true);

        // -------------------------------------- ��ü ���� ���� --------------------------------------
        foreach (Transform obj in objs)
        {
            if (obj.name == "Star1") { objs[0] = obj; }
            else if (obj.name == "Star2") { objs[1] = obj; }
            else if (obj.name == "Star3") { objs[2] = obj; }
            
            if(obj.name == "TxtClearTimeNum") 
            {
                TimeTxt = obj.GetComponent<TextMeshProUGUI>();
            }
        }

        
        //foreach (TextMeshProUGUI txt in GetComponentsInChildren<TextMeshProUGUI>(true))
        //{
        //    if (txt.name == "TxtClearTimeNum") TimeTxt = txt;
        //}

        // ---------------------------------------- ����ó��(���� Ŭ���� Ÿ��) ------------------------------
        int nGrade = 0;
        while (fTime >= 30.0f && nGrade < 4)
        {
            fTime -= 30.0f;                     // 30�� ���� �� �� ���� ����.
            nGrade++;
        }

        // -------------------------------------- ������ ���� �� ��� --------------------------------------
        for (int i = 0; i < nGrade; i++)         
        {
            objs[i].gameObject.SetActive(true);
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
        // 
    }

    void Ld()
    {
        // �����ؾ� �� ��
        // button, Onclick�� �Լ��߰�
        SceneManager.LoadScene("CharacterSelect");
    }
}

