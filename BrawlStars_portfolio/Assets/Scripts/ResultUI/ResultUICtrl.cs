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
    private TextMeshProUGUI PlayerTxt;
    private TextMeshProUGUI BossTxt;

    private AudioSource win_lose_Sound;
    private AudioSource Click_Sound;

    private Transform CharacterPos;
    private Animator Player_Anim;

    private void Start()
    {
        InitTrans();
        //SetStars(GameManager.instance.m_fClearTime);     // 정상구동 할려면 파라미터 GameManager.instance.m_fTime 로 바꿀 것
        SetClearTime(GameManager.instance.m_fClearTime); // 정상구동 할려면 파라미터 GameManager.instance.m_fTime 로 바꿀 것
        SetWinLostText();

        //선택된 캐릭터 렌더 텍스쳐로 보이게 하기 위해 프리팹 소환
        CharacterPos = GameObject.Find("CharacterPos").transform;
        CreateCharacter();

        Sound(); // 결과에 따라 사운드 다르게 재생 -유석-
    }
    private void Update()
    {
        /*
        for (int i = 0; i < nGrade; i++)
        {
            arrObjs[i].transform.Rotate(Vector3.up * Time.deltaTime * 100f);
        }
        */
    }

    private void InitTrans()
    {
        // -------------------------------------- 개체 갖고 오기 --------------------------------------
        Transform[] TempObjs = GetComponentsInChildren<Transform>(true);

        foreach (Transform trs in TempObjs)
        {
            if (trs.name == "Star1") { arrObjs[0] = trs; }
            else if (trs.name == "Star2") { arrObjs[1] = trs; }
            else if (trs.name == "Star3") { arrObjs[2] = trs; }
            else if (trs.name == "TxtClearTimeNum") { TimeTxt = trs.GetComponent<TextMeshProUGUI>(); }
            else if (trs.name == "BtnRetry") { BtnRetry = trs.GetComponent<Button>(); }
            else if (trs.name == "BtnMainMenu") { BtnMainMenu = trs.GetComponent<Button>(); }
            else if (trs.name == "Win/Lose_Text(character)") { PlayerTxt = trs.GetComponent<TextMeshProUGUI>(); }
            else if (trs.name == "Win/Lose_Text(Boss)") { BossTxt = trs.GetComponent<TextMeshProUGUI>(); }
        }

        // BtnRetry.onClick.AddListener(Retry);
        // BtnMainMenu.onClick.AddListener(LoadMainMenu);
    }
    void SetWinLostText()
    {
        if (GameManager.instance.m_fClearTime <= 0)
        {
            PlayerTxt.text = "Lose";
            BossTxt.text = "Win";
        }
    }
    void SetStars(float fTime)
    {
        // ---------------------------------------- 점수처리(남은 클리어 타임) ------------------------------

        while (fTime >= 30.0f && nGrade < 3)
        {
            fTime -= 30.0f;                     // 30초 마다 별 한 개씩 생성.
            nGrade++;
        }

        // -------------------------------------- 점수에 따른 별 출력 --------------------------------------
        for (int i = 0; i < nGrade; i++)
        {
            arrObjs[i].gameObject.SetActive(true);
        }
    }


    void SetClearTime(float fTime) // 파라미터의 float 값을 남은 시간으로 출력해주는 함수
    {
        // How to 소수점 아래 버림
        float fMin = fTime / 60;
        int Min = (int)fMin;

        int Sec = (int)(fTime % 60);

        TimeTxt.text = Min + " : " + Sec;
    }


    void Sound()
    {
        if (GameManager.instance.m_fTime <= 0.0f)
        {
            win_lose_Sound = GameObject.Find("Lose").GetComponent<AudioSource>();
        }
        else
        {
            win_lose_Sound = GameObject.Find("Win").GetComponent<AudioSource>();
        }
        win_lose_Sound.Play();
    }

    void CreateCharacter()
    {
        GameObject obj = Resources.Load<GameObject>(DataManager.instance.select_character.charPrefab);
        GameObject Character = Instantiate(obj, CharacterPos.position, CharacterPos.rotation);
        Player_Anim = Character.GetComponentInChildren<Animator>();

        if (GameManager.instance.m_fTime >0.0f)
            Player_Anim.SetTrigger("tVictory");
    }
}

