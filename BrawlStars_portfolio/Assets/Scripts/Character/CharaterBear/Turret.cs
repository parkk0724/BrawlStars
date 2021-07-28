using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float fRange = 10.0f;
    public float fRotSpeed = 20f;
    public LayerMask LayerSearchTarget;
    public Transform tBulletPos = null;
    public Transform tfGunBody = null;
    public Transform tfTarget = null;
    Transform ShortTransform = null;

    void Start()
    {
        InvokeRepeating("SearchEnemy", 0f, 0.5f);
    }

    void Update()
    {
        if (tfTarget != null)
        {
            this.transform.LookAt(tfTarget.position );
            // 해결방안
            //      1. LookAt + offset 추가
            //      2. 내적, dir, y축 회전

        }
        else
            this.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void SearchEnemy()
    {
        // 중점: position 거리: fRange
        Collider[] Cols = Physics.OverlapSphere(this.transform.position, fRange);

        if(Cols.Length > 0)
        {
            float fShortTestValue = Mathf.Infinity;
            
            foreach(Collider ColTarget in Cols)
            {
                if (ColTarget.CompareTag("Monster"))
                {
                    float fDistance = Vector3.SqrMagnitude(this.transform.position - ColTarget.transform.position);

                    if (fDistance < fShortTestValue)
                    {
                        fShortTestValue = fDistance;
                        ShortTransform = ColTarget.transform;
                    }
                }
            }
        }
        tfTarget = ShortTransform;
        Debug.Log("타겟위치: " + tfTarget.position);

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


    터렛 방향 변경시 야기되는 문제
        1. 일단 제대로 안 돌아간다. -> 값이 잘못됐다 or 추출된 값이 잘못 들어왔다 or 오브젝트문제.
                -> 오브젝트 시험


            // ------------------------ 제대로 안 돌아가네 왜 그럴까?? ---------------------
            // LookRotation: 특정 좌표를 바라보게 만드는 회전값 리턴
            //Quaternion LookRotation = Quaternion.LookRotation(tfTarget.position);

            // RotateTowards: A 지점에서 B 지점까지 C의 스피드로 회전
            //Vector3 VecterEulerValue = Quaternion.RotateTowards(this.transform.rotation, LookRotation, fRotSpeed).eulerAngles;           

            // 오일러값에서 y축만 반영되게 수정한 뒤 쿼터니온으로 변환
            //this.transform.rotation = Quaternion.Euler(0, VecterEulerValue.y, 0);

            //Debug.Log("LookRotation:" + LookRotation);
            //Debug.Log("오일러값:" + VecterEulerValue);
            //Debug.Log("현재 회전값:" + this.transform.rotation);
            // -------------------------- 까지 해결 중인 부분 ---------------------------------


        //if(tfTarget != null)
        //{

        //    ////Debug.Log(tfTarget.position);
        //    //Vector3 TargetDir = (tfTarget.position - this.transform.position).normalized;
        //    //float DotValue = Vector3.Dot(this.transform.forward, TargetDir);

        //    //float Angle = Mathf.Acos(DotValue);
        //    //float theta = Angle * Mathf.Rad2Deg;

        //    ////Vector3 dir = tfTarget.position;
        //    ////dir.y = this.transform.position.y;

        //    //this.transform.Rotate(0, theta, 0);
        //    //Debug.Log(theta);
        //}
*/