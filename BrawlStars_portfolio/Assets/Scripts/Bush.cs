using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    MeshRenderer m_meshRenderer;

    AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.volume = 0.1f;
        m_audioSource.clip = Resources.Load<AudioClip>("Prefabs/Sound/Bush");

        m_meshRenderer = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Monster") || other.CompareTag("Bullet"))
        {
            m_audioSource.Play();
            m_meshRenderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            m_meshRenderer.material.color = new Color(1, 1, 1, 0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster") || other.CompareTag("Bullet"))
        {
            m_meshRenderer.material.shader = Shader.Find("Standard");
            m_meshRenderer.material.color = new Color(1, 1, 1, 1);
        }
    }
}
