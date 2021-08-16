using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum Start_State { NONE, START }
    [HideInInspector] public Start_State m_Start = Start_State.NONE;
    [HideInInspector] public enum ESC_State { NONE, ESC };
    [HideInInspector] public ESC_State m_ESC_state = ESC_State.NONE;      

    private bool m_bEnd = false;

    private GameObject PlayerStartPos;
    private GameObject Soldier;
    private GameObject BoxMan;
    private GameObject Bear;
    private GameObject Jester;
    private SoundManager m_soundManager;
    static private GameManager _instance;
    
    static public GameManager instance
    { 
        get
        {
           // if (_instance == null)
           // {
           //     GameObject obj = new GameObject("GameManager");
           //     _instance = obj.AddComponent<GameManager>();
           // }

            return _instance;
        }
    }

    float m_fCurDelayPortal;
    [SerializeField] float m_fMaxDelayPortal = 10.0f;

    public float m_fTime { get; private set; }
    public float m_fMaxTime { get; private set; }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerStartPos = GameObject.Find("PlayerStartPos");
        CreatePlayer();
        Soldier = GameObject.Find("Soldier");
        BoxMan = GameObject.Find("BoxMan");
        Bear = GameObject.Find("BearDefault");
        Jester = GameObject.Find("Jester");
        m_soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
    }
    void Start()
    {
        m_fCurDelayPortal = m_fMaxDelayPortal;
        m_fMaxTime = 300.0f;
        m_fTime = m_fMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        CursorVisivle(); 

        if (m_Start == Start_State.START)
        {
            switch (m_ESC_state)
            {
                case ESC_State.NONE:
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ESC_UI_Button click = GameObject.Find("ESC_UI").GetComponent<ESC_UI_Button>();
                            click.PlayClickSound();
                            ESC_UI.Instance.Print_UI();
                            m_ESC_state = ESC_State.ESC;
                            Time.timeScale = 0.0f; // 업데이트 돌아가는 시간을 멈추는 코드 (단점, 코르틴, 등 시간에 구애받지 않는 함수는 적용 x)
                        }
                    }
                    break;
                case ESC_State.ESC:
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ESC_UI_Button click = GameObject.Find("ESC_UI").GetComponent<ESC_UI_Button>();
                            click.PlayClickSound();
                            ESC_UI.Instance.Exit_UI();
                            m_ESC_state = ESC_State.NONE;
                            Time.timeScale = 1.0f;
                        }
                    }
                    break;
            }

            if (m_fTime > 0)
            {
                m_fCurDelayPortal += Time.deltaTime;
                m_fTime -= Time.deltaTime;
            }
            else
            {
                if(!m_bEnd) EndGame("Lose");
            }
        }
        
    }

    public float GetCurDelayPortal() { return m_fCurDelayPortal; }
    public void SetCurDelayPortal(float t) { m_fCurDelayPortal = t; }
    public float GetMaxDelayPortal() { return m_fMaxDelayPortal; }
    public void EndGame(string s)
    {
        m_bEnd = true;
        ESC_UI.Instance.SE_Bar.GetComponent<Slider>().value = 0;

        m_soundManager.Playing.SetActive(false);
        m_soundManager.Playing_Angry.SetActive(false);

        if (s == "Win") m_soundManager.PlaySound(m_soundManager.m_Win);
        else m_soundManager.PlaySound(m_soundManager.m_Lose);

        StartCoroutine(GameOver(s));
    }
    public IEnumerator GameOver(string s)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/EndText"));
        obj.GetComponentInChildren<TMPro.TMP_Text>().text = s;
        Text t = obj.GetComponentInChildren<Text>();

        Color c = new Color(1, 1, 1, 0);
        t.color = c;
        int flag = 1;

        AsyncOperation async_operation = SceneManager.LoadSceneAsync("EndLogunity");
        async_operation.allowSceneActivation = false;
        while (!async_operation.isDone)
        {
            if(async_operation.progress >= 0.9f)
            {
                c.a += Time.deltaTime * flag;
                t.color = c;
                if (c.a >= 1 || c.a <= 0) flag *= -1;

                if (Input.GetMouseButtonDown(0)) async_operation.allowSceneActivation = true;
            }

            yield return true;
        }
    }

    public void ChangeState()
    {
        if (m_Start == Start_State.NONE)
        {
            m_Start = Start_State.START;
        }
        else
        {
            m_Start = Start_State.NONE;
        }
    }
    
    private void CursorVisivle()
    {
        //if (m_ESC_state == ESC_State.NONE)
        //    Cursor.visible = false;
        //else
        //    Cursor.visible = true;

        //Cursor.lockState = CursorLockMode.Confined; //작업에 마우스 가 안잡혀서 잠깐 빼놓음
    }

    private void CreatePlayer()
    {
        GameObject prefabs = Resources.Load<GameObject>(DataManager.instance.select_character.charPrefab);
        GameObject Player = Instantiate(prefabs, PlayerStartPos.transform.position, PlayerStartPos.transform.rotation);

        Player.transform.parent = GameObject.Find("Player").transform;

        //if (DataManager.instance.select_character.index == 101)
        //{
        //    BoxMan.SetActive(false);
        //    Bear.SetActive(false);
        //    Jester.SetActive(false);
        //}
        //else if (DataManager.instance.select_character.index == 102)
        //{
        //    Soldier.SetActive(false);
        //    Bear.SetActive(false);
        //    Jester.SetActive(false);
        //}
        //else if (DataManager.instance.select_character.index == 103)
        //{
        //    Soldier.SetActive(false);
        //    BoxMan.SetActive(false);
        //    Jester.SetActive(false);
        //}
        //else
        //{
        //    Soldier.SetActive(false);
        //    BoxMan.SetActive(false);
        //    Bear.SetActive(false);
        //}
    }
}