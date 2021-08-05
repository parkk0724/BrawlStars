using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Hero m_Hero = null;
    [SerializeField] TMPro.TMP_Text m_tFeverGauge = null;
    [SerializeField] Image m_FeverGuage = null;
    [SerializeField] Image m_imgFullGauge = null;
    [SerializeField] Slider m_HP = null;
    [SerializeField] Slider m_ST = null;
    void Start()
    {
        m_Hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFeverGauge();
        Update_HP_Gauge();
    }

    void UpdateFeverGauge()
    {

        float feverGauge = m_Hero.GetFever() / m_Hero.GetMaxFever();
        m_FeverGuage.fillAmount = feverGauge;

       //Color c = m_FeverGuage.color;
       //c.a = 0.5f;
       //if (feverGauge <= 0) 
       //{
       //    if (m_imgFullGauge.gameObject.activeSelf) m_imgFullGauge.gameObject.SetActive(false);
       //}
       //else if (feverGauge >= 1)
       //{
       //    if (!m_imgFullGauge.gameObject.activeSelf) m_imgFullGauge.gameObject.SetActive(true);
       //    c.a = 1.0f;
       //}
       //m_FeverGuage.color = c;
    }

    void Update_HP_Gauge()
    {
        float HP_Guage = m_Hero.GetHp() / m_Hero.GetMaxHp();
        m_HP.value = HP_Guage;
        Debug.Log(HP_Guage);
    }

    void UpdateText()
    {
        //float feverGauge = (m_Character.GetFever() / m_Character.GetMaxFever() * 100);
        //m_tFeverGauge.text = feverGauge.ToString();

        //Vector3 pos = m_tFeverGauge.transform.localPosition;
        //Color c = m_tFeverGauge.color;
        //c.a = 0.5f;
        //if (feverGauge <= 0)
        //{
        //    if (m_rimgFullGauge.gameObject.activeSelf) m_rimgFullGauge.gameObject.SetActive(false);
        //    pos.x = 70;
        //}
        //else if (feverGauge >= 100)
        //{
        //    if (!m_rimgFullGauge.gameObject.activeSelf) m_rimgFullGauge.gameObject.SetActive(true);
        //    pos.x = 46;
        //    c.a = 1.0f;
        //}
        //else pos.x = 59;
        //m_tFeverGauge.color = c;
        //m_tFeverGauge.transform.localPosition = pos;
    }
}