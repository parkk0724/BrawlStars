using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : CharacterUI
{ 
    // Start is called before the first frame update
    [SerializeField] Image m_imgStaminaBar;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_imgStaminaBar.fillAmount = (float)m_Character.GetStamina() / (float)m_Character.GetMaxStamina();
    }
}
