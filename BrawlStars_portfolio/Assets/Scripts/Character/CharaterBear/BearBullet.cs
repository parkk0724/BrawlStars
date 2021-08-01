using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BearBullet : MonoBehaviour
{
    public int damage = 20;
    public float curTime = 0.0f;
    public float BulletSpeed = 0.5f;
    public float BulletLiveMaxTime = 2.0f;
    public UnityAction OnFeverUp = null;
    public GameObject HitEffect;
    Vector3 TargetDirVector; 
    Coroutine Temp;

    private void Start()
    {
        StartCoroutine("DestoryBullet");
        /*
        Rigidbody ribody = GetComponent<Rigidbody>();
        ribody.velocity = this.transform.forward * BulletSpeed;
        */
        // OnFeverUp = GetComponent<>
        TargetDirVector = this.transform.forward * BulletSpeed * Time.deltaTime;
    }

    private void Update()
    {
        this.transform.Translate(TargetDirVector, Space.World);
        //Debug.Log(this.transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
            Destroy(gameObject);

        if (other.gameObject.tag == "Monster")
        { 
            Debug.Log("몬스터 맞춤");
            this.transform.position = other.transform.position;
            GameObject obj = Instantiate(HitEffect, this.transform.position, this.transform.rotation); // 이펙트 생성
            other.GetComponent<Monster>()?.Hit(damage, Color.red);
            Destroy(obj, 1.0f);
            SearchTarget();
        }
    }
    IEnumerator DestoryBullet()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }

    void SearchTarget() // 주변에 적을 검사해서 적을 향해 방향 트는 게 목표.
    {
        Debug.Log("Search Start");
        float Shortdist = Mathf.Infinity;                       // 최소값: 무한대
        //float SecondShortdist = Mathf.Infinity;               // 두 번째 최소값: 무한대 초기화
        float range = 10.0f;                                    // 탐색범위
        Transform ShortTarget = null;                           // 가까운 대상
        //Transform SecondShortTarget = null;                   // 가까운 대상
        Transform ResultTarget = null;                          // 최종대상
        LayerMask EnemyLayer = LayerMask.NameToLayer("Target"); // 적 레이어 설정

        Debug.Log("Target: " + EnemyLayer);

        Collider[] EnemyColliders = Physics.OverlapSphere(this.transform.position, range);

        if (EnemyColliders.Length > 0) // 검출된 게 있으면 진행.
        {
            foreach(Collider colTarget in EnemyColliders)
            {
                float Dist = Vector3.SqrMagnitude(this.transform.position - colTarget.transform.position);

                if (!colTarget.gameObject.tag.Equals("Monster")) // 몬스터 아니라면 스킵
                    continue;

                if (Dist < Shortdist && Dist > 0.7f)
                {
                    Shortdist = Dist;
                    ShortTarget = colTarget.transform;
                }

            }

        }

        ResultTarget = ShortTarget; // 최종값

        if (ResultTarget != null)
        {
            Debug.Log(ResultTarget.position);
            // 검출된 콜라이더를 향해 방향 잡고
            Vector3 dir = ResultTarget.position - this.transform.position;
            dir.y = ResultTarget.position.y / 2.0f;
            dir.Normalize();

            TargetDirVector = dir * BulletSpeed * Time.deltaTime ;

            Debug.Log("Search End");
        }
    }
}

// 현재 자신의 위치에서 EnemyLayer를 가진 충돌체를 찾는다.

/*
  만들 알고리즘
    1. 총알이 부딪친다. OnTriggerEnter 진입
    2. OnTrigeerEnter
        {
            collider[] arrCol = OverlapSphere(센터, 반경)

            foreach(collider col in arrCol)
             {
                float distance = sqrmagnitude(콜라이더.pos - 불렛위치.pos);

                if (distance < ShortDistance)
                {

                    ShortDistance  = distance;
                    ShortTarget = col;
                }

                else if (distance < SecondShortDistance || distance > ShortDistance) 
                {
                    SecondShortDistance = distance;
                    SecondeShortTarget = col;
                }
             }
        }


   주변 콜라이더 검출   

   검출된 에러
        1. 제일 가까운 것을 찾을 경우 이게 총알에서 찾는 거기 때문에 총알과 겹쳐있는 것이 제일 가까운 순위가 된다.

            해결방안: 두 번째로 가까운 것을 찾아보자


        2. 몬스터 레이어 설정이 되어 있지 않음

            해결방안: 전체검출


        3. 검출이 안 되고 있음.



            //문제상황
            해당 방향으로 탄환이 안 틀어감. 검출은 거의 다 됐음.
             

            //this.transform.LookAt(ResultTarget);
            //Rigidbody ribody = GetComponent<Rigidbody>();
            //ribody.velocity = Vector3.zero;
            //ribody.velocity = dir * 5f;
            //dir.y = 0;
            // this.transform.Translate(dir * 5f * Time.deltaTime );

            //else if (Shortdist > Dist) // Dist가 최소값보다 작다면
            //{
            //    if(Shortdist < Dist & Dist < SecondShortdist)
            //    {
            //        SecondShortdist = Dist; // 두 번째 값
            //        SecondShortTarget = colTarget.transform;
            //    }
            //    else
            //    { 
            //        Shortdist = Dist;   // 최소값
            //        ShortTarget = colTarget.transform;
            //    }

            //else if (Dist > 0.1f && Dist < SecondShortdist && Dist > Shortdist)
            //{
            //    SecondShortdist = Dist;
            //    SecondShortTarget = colTarget.transform;
            //}
*/