using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float fRange = 100f;
    public LayerMask LayerMask;

    public Transform tfGunBody = null;
    public Transform tfTarget = null;
    void Start()
    {
        InvokeRepeating("SearchEnemy", 0f, 0.5f);
    }

    void Update()
    {
        if(tfTarget != null)
        {
            Debug.Log("탐색완료");
        }
    }

    void SearchEnemy()
    {
        // 중점: position 거리: fRange
        Collider[] Cols = Physics.OverlapSphere(this.transform.position, fRange, LayerMask);
        Transform NearbyEnemy = null;

        if(Cols.Length > 0)
        {
            float fShortTestValue = Mathf.Infinity;

            foreach(Collider ColTarget in Cols)
            {
                float fDistance = Vector3.SqrMagnitude(transform.position - ColTarget.transform.position);
                
                if(fDistance <  fShortTestValue)
                {
                    fShortTestValue = fDistance;
                    NearbyEnemy = ColTarget.transform;
                }
            }
        }
        tfTarget = NearbyEnemy;
    }
}

/*
void SearchTarget()
{
    float Shortdist = 10;
    Transform shorTarget = null;
    Collider[] EnemyCollider = Physics.OverlapSphere(this.transform.position, m_fTargetRange, m_lmEnemyLayer);
    if (EnemyCollider.Length > 0)
    {
        for (int i = 0; i < EnemyCollider.Length; i++)
        {
            float Dist = Vector3.Distance(this.transform.position, EnemyCollider[i].transform.position);
            if (Shortdist > Dist)
            {
                Shortdist = Dist;
                shorTarget = EnemyCollider[i].transform;
            }
        }
    }
    m_tfResultTarget = shorTarget; // 최종값
}

void LookatEnemy()
{
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    this.transform.LookAt(m_tfResultTarget);
}
*/