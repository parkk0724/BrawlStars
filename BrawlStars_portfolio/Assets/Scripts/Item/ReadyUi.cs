using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Image m_iRe_image;
    public Image m_iad_yimage;
    public Image m_iGo;
    public float m_fendPos = 150;
    private RectTransform m_rRTransform;
    private RectTransform m_rATransform;
    private RectTransform m_rGTransform;
    Coroutine m_cReady;
    private float m_fCurtime = 0;
    float m_fgotime = 0;
    Vector3 OriginPos;
    Vector3 OriginPos_1;
    Vector3 endPos;
    Vector3 endPos_1;
    bool m_bcoActive = true;
    bool m_bStop = true;
    float x1;
    float x2;
    /// <summary>
    /// 
    /// </summary>
    Color alpha;
    float F_time = 20f;
    void Start()
    {
        OriginPos = new Vector3(-600, 0);
        OriginPos_1 = new Vector3(600, 0);
        m_rRTransform = m_iRe_image.GetComponent<RectTransform>();
        m_rATransform = m_iad_yimage.GetComponent<RectTransform>();
        m_rGTransform = m_iGo.GetComponent<RectTransform>();
        endPos = new Vector3(-m_fendPos, 0); //��ġ�������⼭
        endPos_1 = new Vector3(m_fendPos, 0);
        alpha = m_iGo.color;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_bcoActive)
        {
            m_fCurtime += Time.deltaTime;
            m_cReady = StartCoroutine(Ready(m_fCurtime));
        }
        if (m_fCurtime >7)
        {
            m_bcoActive = false;
            if(m_cReady != null)StopCoroutine(m_cReady);
        }
        #region update��������
        //m_fCurtime += Time.deltaTime;
        //if (m_fCurtime > 2)
        //{
        //    x1 += 10f;
        //    m_rRTransform.anchoredPosition = OriginPos + new Vector3(x1, 0);
        //    m_rATransform.anchoredPosition = OriginPos_1 + new Vector3(-x1, 0);
        //    if (m_rRTransform.anchoredPosition.x > endPos.x && m_rATransform.anchoredPosition.x < endPos_1.x)
        //    {
        //        //m_fCurtime = 0;
        //        m_rRTransform.anchoredPosition = endPos;
        //        m_rATransform.anchoredPosition = endPos_1;
        //            curtime += Time.deltaTime;
        //        if (curtime > 2)
        //        {
        //            x2 += 10f;
        //            m_rRTransform.anchoredPosition = endPos + new Vector3(-x2, 0);
        //            m_rATransform.anchoredPosition = endPos_1 + new Vector3(x2, 0);
        //        }
        //    }
        //}
        #endregion

    }
    IEnumerator Ready(float time)
    {
        if (time > 2)
        {
            x1 += 10f;
            m_rRTransform.anchoredPosition = OriginPos + new Vector3(x1, 0);
            m_rATransform.anchoredPosition = OriginPos_1 + new Vector3(-x1, 0);
            if (m_rRTransform.anchoredPosition.x > endPos.x && m_rATransform.anchoredPosition.x < endPos_1.x && m_bStop)
            {
                //m_fCurtime = 0;
                m_rRTransform.anchoredPosition = endPos;
                m_rATransform.anchoredPosition = endPos_1;
                
                yield return new WaitForSeconds(2);
                m_bStop = false;
                x1 = 0;
            }
            if (!m_bStop)
            {
                x2 += 10f;
                m_rRTransform.anchoredPosition = endPos + new Vector3(-x2, 0);
                m_rATransform.anchoredPosition = endPos_1 + new Vector3(x2, 0);
            }
            yield return new WaitForSeconds(2);
            while (alpha .a< 1f)
            {
                m_fgotime += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, m_fgotime);
                m_iGo.color = alpha;

                yield return null;
            }
            m_fgotime = 0;
            yield return new WaitForSeconds(1);
            while (alpha.a > 0)
            {
                m_fgotime += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, m_fgotime);
                m_iGo.color = alpha;

                yield return null;
            }
            m_iGo.gameObject.SetActive(false);
            yield return null;
        }
    }
}
