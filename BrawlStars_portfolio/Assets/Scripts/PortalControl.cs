using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalControl : MonoBehaviour
{
    public ParticleSystem m_ptsPortalIn;
    public ParticleSystem m_ptsPortalOut;
    private ParticleSystem[] m_ptsPortalEffect;
    public Image m_BarImage;
    public Transform[] m_tfPortals;

    float m_fCurTime = 0.0f;
    float m_fMaxTime = 0.7f;
    bool m_bPortalOn = false;
    bool m_bPortalEffectOn = false;
    private void Awake()
    {
        m_ptsPortalEffect = this.GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (!m_bPortalEffectOn && GameManager.instance.GetCurDelayPortal() >= GameManager.instance.GetMaxDelayPortal())
        {
            PlayPortalEffect();
        }
        else if (m_bPortalEffectOn && GameManager.instance.GetCurDelayPortal() < GameManager.instance.GetMaxDelayPortal())
        {
            StopPortalEffect();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetCurTime();
            m_bPortalOn = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (GameManager.instance.GetCurDelayPortal() >= GameManager.instance.GetMaxDelayPortal())
            {
                if (m_bPortalOn)
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
            m_ptsPortalIn.Stop();
            ResetCurTime();
        }
    }

    IEnumerator MovePortal(Collider other, int index)
    {
        ResetCurTime();
        Vector3 pos = m_tfPortals[index].transform.position;
        other.transform.position = pos;
        other.gameObject.SetActive(false);
        pos.y += 5.0f;
        m_ptsPortalOut.transform.position = pos;
        m_ptsPortalOut.Play();
        GameManager.instance.SetCurDelayPortal(0.0f);
        yield return new WaitForSeconds(1);
        other.gameObject.SetActive(true);
        m_bPortalOn = false;
        StopPortalEffect();
    }

    private void ResetCurTime()
    {
        m_fCurTime = 0.0f;
        m_BarImage.fillAmount = m_fCurTime / m_fMaxTime;
    }

    private void PlayPortalEffect()
    {
        foreach (ParticleSystem pts in m_ptsPortalEffect) pts.Play();
        m_bPortalEffectOn = true;
    }
    private void StopPortalEffect()
    {
        foreach (ParticleSystem pts in m_ptsPortalEffect) pts.Stop();
        m_bPortalEffectOn = false;
    }
}
