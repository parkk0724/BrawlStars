using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    // Start is called before the first frame update
    public Image m_iGo;
    private RectTransform m_rGTransform;
    private float m_fCurtime = 0;
    Color alpha;
    float m_fgotime = 0;
    float F_time = 30;
    void Start()
    {
        m_rGTransform = m_iGo.GetComponent<RectTransform>();
        alpha = m_iGo.color;
    }

    // Update is called once per frame
    void Update()
    {
        m_fCurtime += Time.deltaTime;
        StartCoroutine(Ready(m_fCurtime));
    }

    IEnumerator Ready(float time)
    {
        if(time > 3)
        {
            while (alpha.a < 1f)
            {
                
                m_fgotime += Time.deltaTime / F_time;
                m_rGTransform.sizeDelta = m_rGTransform.sizeDelta * 1.00005f ;
                alpha.a = Mathf.Lerp(0, 1, m_fgotime);
                m_iGo.color = alpha;

                yield return null;
            }
            m_fgotime = 0;
            yield return new WaitForSeconds(1);

            while (alpha.a >0 )
            {
                F_time = 60;
                m_fgotime += Time.deltaTime / F_time ;
                m_rGTransform.sizeDelta = m_rGTransform.sizeDelta  * 0.99990f;
                alpha.a = Mathf.Lerp(1, 0, m_fgotime);
                m_iGo.color = alpha;

                yield return null;
                if(alpha.a ==0)
                    m_iGo.gameObject.SetActive(false);
            }
            
        } 
    }
}
