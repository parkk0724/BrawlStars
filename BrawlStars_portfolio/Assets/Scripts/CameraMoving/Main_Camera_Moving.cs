using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Moving : MonoBehaviour
{
    public GameObject playercamera;
    public GameObject player;
    public GameObject Boss;

    public float speed = 0.0f;
    public bool startmove = true;
    Coroutine cameramove = null;

    float forwarddist = 0.0f;
    float backwarddist = 0.0f;

    GameObject PlayerUI;
    GameObject StartUI;

    GameManager m_myGameManager;
    Hero m_myHero;
    BossMonster m_Boss;
    SoundManager m_Sound;

    private void Awake()
    {
        m_Sound = GameObject.Find("Sound").GetComponent<SoundManager>();
        PlayerUI = GameObject.Find("UI");
        StartUI = GameObject.Find("StartText");
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 캐릭바꿀때마다 변경해야되서 알아서 찾아서 넣어주게 함. -금환
        m_myHero = player.GetComponent<Hero>();
        m_Boss = Boss.GetComponent<BossMonster>();

        if (startmove == true)
            cameramove = StartCoroutine(StartCameraMoving());
        else
        {
            this.transform.position = playercamera.transform.position;
        }
        forwarddist = Mathf.Abs(player.transform.position.z - playercamera.transform.position.z) + 2.0f;
        backwarddist = forwarddist + 4.5f;
    }
    void Update()
    {
        Vector3 pos = this.transform.position;

        if (Vector3.Dot(Vector3.forward, player.transform.forward) >= -0.2f)
            pos.z = player.transform.position.z - forwarddist;        
        else
            pos.z = player.transform.position.z - backwarddist;

        pos.z = Mathf.Clamp(pos.z, -9.0f, 6.0f);

        if (cameramove == null)
        {          
            this.transform.position = Vector3.Lerp(this.transform.position, pos, 3.0f * Time.deltaTime);
        }
    }

    IEnumerator StartCameraMoving()
    {
        m_Sound.PlaySound(m_Sound.PlayStart);

        Vector3 dir = playercamera.transform.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = speed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                StartCoroutine(StartText());
                delta = dist;
                m_Sound.PlaySound(m_Sound.Playing);
            }
            dist -= delta;
            this.transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        cameramove = null;

        m_myHero.m_Start = Hero.Start_State.START; // 도착했을 때 히어로의 업데이트 돌 수 있도록 enum state 바꿈
        m_Boss.m_Start = BossMonster.Start_State.START;
        GameManager.instance.ChangeState();

        m_Sound.PlaySound(m_Sound.Portal);
    }

    IEnumerator StartText()
    {
        StartUI.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(2.0f);
        StartUI.GetComponent<Canvas>().enabled = false;
        PlayerUI.GetComponent<Canvas>().enabled = true;
    }
}
