using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    public ParticleSystem m_ptsPortalIn;
    public ParticleSystem m_ptsPortalOut;
    public Transform[] m_tfPortals;
    float m_fCurTime = 0.0f;
    float m_fMaxTime = 1.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            m_ptsPortalIn.transform.position = this.transform.position;
            m_ptsPortalIn.Play();
            m_fCurTime = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Stay : " + m_fCurTime);
            if (m_fCurTime >= m_fMaxTime)
            {
                int rnd = Random.Range(0, m_tfPortals.Length);
                StartCoroutine(MovePortal(other, rnd));
                
            }
            else
            {
                m_fCurTime += Time.deltaTime;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
        }
    }

    IEnumerator MovePortal(Collider other, int index)
    {
        other.gameObject.SetActive(false);
        Vector3 pos = m_tfPortals[index].transform.position;
        pos.y += 5.0f;
        m_ptsPortalOut.transform.position = pos;
        m_ptsPortalOut.Play();
        yield return new WaitForSeconds(1);
        other.transform.position = m_tfPortals[index].transform.position;
        other.gameObject.SetActive(true);
    }
}
