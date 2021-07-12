using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalControl : MonoBehaviour
{
    public GameManager m_GameManager;
    public ParticleSystem m_ptsPortalIn;
    public ParticleSystem m_ptsPortalOut;
    public Image m_BarImage;
    public Transform[] m_tfPortals;

    float m_fCurTime = 0.0f;
    float m_fMaxTime = 1.0f;
    bool m_bPortalOn = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            ResetCurTime();
            m_bPortalOn = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (m_GameManager.GetCurDelayPortal() >= m_GameManager.GetMaxDelayPortal())
            {
                if(m_bPortalOn)
                {
                    if (m_fCurTime >= m_fMaxTime)
                    {
                        int rnd = Random.Range(0, m_tfPortals.Length);
                        StartCoroutine(MovePortal(other, rnd));
                    }
                    else
                    {
                        m_fCurTime += Time.deltaTime;
                        m_BarImage.fillAmount = m_fCurTime / m_fMaxTime;
                    }
                }
                else
                {
                    m_ptsPortalIn.transform.position = this.transform.position;
                    m_ptsPortalIn.Play();
                    ResetCurTime();
                    m_bPortalOn = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
            m_ptsPortalIn.Stop();
            ResetCurTime();
        }
    }

    IEnumerator MovePortal(Collider other, int index)
    {
        ResetCurTime();
        other.gameObject.SetActive(false);
        Vector3 pos = m_tfPortals[index].transform.position;
        pos.y += 5.0f;
        m_ptsPortalOut.transform.position = pos;
        m_ptsPortalOut.Play();
        yield return new WaitForSeconds(1);
        other.transform.position = m_tfPortals[index].transform.position;
        other.gameObject.SetActive(true);
        m_GameManager.SetCurDelayPortal(0.0f);
        m_bPortalOn = false;
    }

    private void ResetCurTime()
    {
        m_fCurTime = 0.0f;
        m_BarImage.fillAmount = m_fCurTime / m_fMaxTime;
    }
}
