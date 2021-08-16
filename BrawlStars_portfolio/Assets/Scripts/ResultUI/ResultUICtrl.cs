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

    private AudioSource win_lose_Sound;
    private AudioSource Click_Sound;

    private Canvas Quit;
    private Button Setting;

    private Transform CharacterPos;

    private void Start()
    {
        InitTrans();
        SetStars(GameManager.instance.m_fTime);     // ���󱸵� �ҷ��� �Ķ���� GameManager.instance.m_fTime �� �ٲ� ��
        SetClearTime(GameManager.instance.m_fTime); // ���󱸵� �ҷ��� �Ķ���� GameManager.instance.m_fTime �� �ٲ� ��

        //���õ� ĳ���� ���� �ؽ��ķ� ���̰� �ϱ� ���� ������ ��ȯ
        CharacterPos = GameObject.Find("CharacterPos").transform;
        Quit = GameObject.Find("Touch").GetComponent<Canvas>();
        Setting = GameObject.Find("Setting").GetComponent<Button>();
        Setting.onClick.AddListener(OnClick_Setting);
        CreateCharacter();

        Sound(); // ����� ���� ���� �ٸ��� ��� -����-
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

       // BtnRetry.onClick.AddListener(Retry);
       // BtnMainMenu.onClick.AddListener(LoadMainMenu);
    }
    void SetStars(float fTime)
    { 
        // ---------------------------------------- ����ó��(���� Ŭ���� Ÿ��) ------------------------------
        
        while (fTime >= 30.0f && nGrade < 3)
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

        int Sec = (int)(fTime % 60);

        TimeTxt.text = Min + " : " + Sec;
    }
    
    void Retry()
    {
        Click_Sound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        Click_Sound.Play();
        SceneManager.LoadScene("Browl_Stars");
    }

    void LoadMainMenu()
    {
        Click_Sound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        Click_Sound.Play();
        SceneManager.LoadScene("MainMenu");        
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
    }

    void OnClick_Setting()
    {
        BtnRetry.onClick.AddListener(Retry);
        BtnMainMenu.onClick.AddListener(LoadMainMenu);
        Click_Sound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        Click_Sound.Play();
        Quit.enabled = true;
    }
}

