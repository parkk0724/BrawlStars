using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bush : MonoBehaviour
{
    public enum BlendMode { Opaque, Transparent }

    MeshRenderer m_meshRenderer;

    AudioSource m_audioSource;
    float m_fVolume = 0.0f;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_fVolume = 0.1f;
        m_audioSource.volume = m_fVolume;
        m_audioSource.clip = Resources.Load<AudioClip>("Prefabs/Sound/Bush");

        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        m_audioSource.volume = m_fVolume * ESC_UI.Instance.SE_Bar.GetComponent<Slider>().value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            m_audioSource.Play();
            SetupMaterialWithBlendMode(m_meshRenderer.material, BlendMode.Transparent);
            //m_meshRenderer.material.shader = Shader.Find("Legacy Shaders/Transparent/BumpedDiffuse");
            m_meshRenderer.material.color = new Color(1, 1, 1, 0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            SetupMaterialWithBlendMode(m_meshRenderer.material, BlendMode.Opaque);
            //m_meshRenderer.material.shader = Shader.Find("Standard");
            m_meshRenderer.material.color = new Color(1, 1, 1, 1);
        }
    }

	public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
	{
		switch (blendMode)
		{
			case BlendMode.Opaque:
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				material.SetInt("_ZWrite", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = -1;
				break;
			case BlendMode.Transparent:
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
				break;
		}
	}
}
