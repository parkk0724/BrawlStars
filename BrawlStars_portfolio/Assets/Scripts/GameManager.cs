using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
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

    [SerializeField] public float m_fTime { get; private set; }
    void Start()
    {
        m_fCurDelayPortal = m_fMaxDelayPortal;
        m_fTime = 300.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_fCurDelayPortal += Time.deltaTime;
        m_fTime -= Time.deltaTime;
    }

    public float GetCurDelayPortal() { return m_fCurDelayPortal; }
    public void SetCurDelayPortal(float t) { m_fCurDelayPortal = t; }
    public float GetMaxDelayPortal() { return m_fMaxDelayPortal; }
}
