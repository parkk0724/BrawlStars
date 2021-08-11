using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetEffectVolume : MonoBehaviour
{
    AudioSource m_audioSource = null;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_audioSource.volume = ESC_UI.Instance.SE_Bar.GetComponent<Slider>().value;
    }
}
