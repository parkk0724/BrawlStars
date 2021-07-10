using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    Transform resultTarget;
    public GameObject Effect;
    public float range = 10f;
    LayerMask enemyLayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookEnemy();
    }
    void LookEnemy()
    {
        float Shortdist = 10;
        Transform shorTarget = null;
        Collider[] EnemyCollider = Physics.OverlapSphere(transform.position, range, enemyLayer);
        if (EnemyCollider.Length > 0)
        {
            for (int i = 0; i < EnemyCollider.Length; i++)
            {
                float Dist = Vector3.Distance(transform.localPosition, EnemyCollider[i].transform.localPosition);
                if (Shortdist > Dist)
                {
                    Shortdist = Dist;
                    shorTarget = EnemyCollider[i].transform;
                }
            }
        }
        resultTarget = shorTarget;
        if (resultTarget == null)
        {
            Debug.Log("목표물이 없습니다.");
        }
        else
        {
            Instantiate(Effect, resultTarget.position, Quaternion.identity);
            //Vector3 dir = resultTarget.position - transform.position;
            //transform.forward = dir.normalized;
        }
    }
}
