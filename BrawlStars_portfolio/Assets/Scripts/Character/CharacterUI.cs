using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Image m_imgHpBar;
    [SerializeField] protected Character m_Character;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 pos = m_Character.transform.position;
        pos.z += 1.0f;
        this.transform.position = pos;

        m_imgHpBar.fillAmount = (float)m_Character.GetHp() / (float)m_Character.GetMaxHp();
    }
}
