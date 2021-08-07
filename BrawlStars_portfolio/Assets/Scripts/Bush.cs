using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    MeshRenderer m_meshRenderer;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        m_meshRenderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        m_meshRenderer.material.color = new Color(1, 1, 1, 0.2f);
    }

    private void OnTriggerExit(Collider other)
    {
        m_meshRenderer.material.shader = Shader.Find("Standard");
        m_meshRenderer.material.color = new Color(1, 1, 1, 1);
    }
}
