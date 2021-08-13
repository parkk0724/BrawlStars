using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    GameObject m_objMonster;
    [SerializeField] float m_fCreateTime = 0.0f;
    Transform[] m_tfPoint = null;
    private void Awake()
    {
        m_objMonster = Resources.Load<GameObject>("Prefabs/Character/MonsterChicken");
    }
    void Start()
    {
        m_tfPoint = new Transform[transform.childCount];
        for (int i = 0; i < m_tfPoint.Length; i++) m_tfPoint[i] = transform.GetChild(i);
        StartCoroutine(Create());
        m_fCreateTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Create()
    {
        int length = m_tfPoint.Length;
        while(true)
        {
            yield return new WaitForSeconds(m_fCreateTime);
            if(m_tfPoint[0].childCount < 6)
            {
                int rnd = Random.Range(0, length - 2);
                GameObject obj = Instantiate(m_objMonster, m_tfPoint[rnd].position, Quaternion.identity, m_tfPoint[0]);
                obj.SetActive(true);
            }
        }
    }
}
