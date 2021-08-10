using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public enum Start_State { NONE, START }
    public Start_State m_Start = Start_State.NONE;
    enum ESC_State { NONE, ESC };
    ESC_State m_ESC_state = ESC_State.NONE;

    static private GameManager _instance;
    static public GameManager instance
    { 
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    float m_fCurDelayPortal;
    [SerializeField] float m_fMaxDelayPortal = 10.0f;

    public float m_fTime { get; private set; }
    public float m_fMaxTime { get; private set; }
    void Start()
    {
        m_fCurDelayPortal = m_fMaxDelayPortal;
        m_fMaxTime = 300.0f;
        m_fTime = m_fMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Start == Start_State.START)
        {
            switch (m_ESC_state)
            {
                case ESC_State.NONE:
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
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
                LoadResultScene();  // 너무 씬이 확바뀜;
            }
        }
        
    }

    public float GetCurDelayPortal() { return m_fCurDelayPortal; }
    public void SetCurDelayPortal(float t) { m_fCurDelayPortal = t; }
    public float GetMaxDelayPortal() { return m_fMaxDelayPortal; }

    public void LoadResultScene() { SceneManager.LoadScene("EndLogunity"); }

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
}
