using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : CharacterUI
{ 
    // Start is called before the first frame update
    [SerializeField] Image m_imgStaminaBar;
    Hero m_Hero;
    void Start()
    {
        m_Character = this.transform.parent.GetComponentInChildren<Character>();
        m_Hero = (Hero)m_Character;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_imgStaminaBar.fillAmount = (float)m_Hero.GetStamina() / (float)m_Hero.GetMaxStamina();
    }
}
