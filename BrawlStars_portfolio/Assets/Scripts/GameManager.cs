using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
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
        switch(m_ESC_state)
        {
            case ESC_State.NONE:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ESC_UI.Instance.Print_UI();
                        m_ESC_state = ESC_State.ESC;
                    }
                }
                break;
            case ESC_State.ESC:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ESC_UI.Instance.Exit_UI();
                        m_ESC_state = ESC_State.NONE;
                    }
                }
                break;
        }       

        if(m_fTime > 0)
        {
            m_fCurDelayPortal += Time.deltaTime;
            m_fTime -= Time.deltaTime;
        }
        else
        {
            LoadResultScene();  // ³Ê¹« ¾ÀÀÌ È®¹Ù²ñ;
        }
        
    }

    public float GetCurDelayPortal() { return m_fCurDelayPortal; }
    public void SetCurDelayPortal(float t) { m_fCurDelayPortal = t; }
    public float GetMaxDelayPortal() { return m_fMaxDelayPortal; }

    public void LoadResultScene() { SceneManager.LoadScene("EndLogunity"); }
}
