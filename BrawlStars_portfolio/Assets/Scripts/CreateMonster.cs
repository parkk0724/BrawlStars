using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    [SerializeField] GameObject m_objMonster;
    Transform[] m_tfPoint;

    void Start()
    {
        m_tfPoint = this.GetComponentsInChildren<Transform>();
        StartCoroutine(Create());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Create()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            int rnd = Random.Range(0, m_tfPoint.Length-1);
            GameObject obj = Instantiate(m_objMonster, m_tfPoint[rnd].position, Quaternion.identity);
            obj.SetActive(true);
        }
    }
}
