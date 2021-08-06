using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JesterSKill_Ui : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image m_DeathTime;
    [SerializeField] private GameObject jesterskill;
    [SerializeField] Gradient graident;
    private Canvas m_mycanvas;
    void Start()
    {
        m_mycanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jesterskill == null)
        {
            m_DeathTime.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            m_DeathTime.fillAmount = jesterskill.GetComponent<JesterSkill>().GetCurtime() / jesterskill.GetComponent<JesterSkill>().GetDeathtime();
            if (m_DeathTime.fillAmount / 1 == 1)
            {
                m_DeathTime.color = graident.Evaluate(1f);
            }
            Vector3 Pos = jesterskill.transform.position;
            Pos.y += 1f;
            m_mycanvas.transform.position = Pos;
            m_mycanvas.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

    }
}