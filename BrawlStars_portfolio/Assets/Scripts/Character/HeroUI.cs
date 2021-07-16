using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image m_imgHpBar;
    [SerializeField] Image m_imgStaminaBar;
    [SerializeField] Character m_Character;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_Character.transform.position;
        this.transform.position = pos;

        m_imgHpBar.fillAmount = (float)m_Character.GetHp() / (float)m_Character.GetMaxHp();
        m_imgStaminaBar.fillAmount = (float)m_Character.GetStamina() / (float)m_Character.GetMaxStamina();
    }
}
