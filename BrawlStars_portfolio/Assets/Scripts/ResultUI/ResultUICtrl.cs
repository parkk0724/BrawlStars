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
        // 비활성 오브젝트 출력
        objs = GetComponentsInChildren<Transform>(true);

        // -------------------------------------- 개체 갖고 오기 --------------------------------------
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

        // ---------------------------------------- 점수처리(남은 클리어 타임) ------------------------------
        int nGrade = 0;
        while (fTime >= 30.0f && nGrade < 4)
        {
            fTime -= 30.0f;                     // 30초 마다 별 한 개씩 생성.
            nGrade++;
        }

        // -------------------------------------- 점수에 따른 별 출력 --------------------------------------
        for (int i = 0; i < nGrade; i++)         
        {
            objs[i].gameObject.SetActive(true);
        }
    }

    void SetClearTime(float fTime) // 파라미터의 float 값을 남은 시간으로 출력해주는 함수
    {
        // How to 소수점 아래 버림
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
        // 구현해야 할 것
        // button, Onclick에 함수추가
        SceneManager.LoadScene("CharacterSelect");
    }
}

