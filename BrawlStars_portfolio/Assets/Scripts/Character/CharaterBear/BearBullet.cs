using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    public int damage = 20;
    public float curTime = 0.0f;
    public float BulletLiveMaxTime = 2.0f;
    
    Coroutine Temp;

    private void Start()
    {
        StartCoroutine("DestoryBullet");
    }

    private void Update()
    {
        this.transform.Translate(this.transform.forward * 5f * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" | other.gameObject.tag == "Obstacle")
            Destroy(gameObject);

        if (other.gameObject.tag == "Monster")
        { 
            Debug.Log("몬스터 맞춤");
            SearchTarget();
            //other.GetComponent<Monster>().Hit(damage, Color.red);
        }
    }
    /*
       구현할 내용

       총알이 맞춘다  -> 주변 콜라이더 객체 검출 -> 콜라이더.pos - 불렛위치.pos 
    */
    IEnumerator DestoryBullet()
    {
        yield return new WaitForSecondsRealtime(4);
        Destroy(gameObject);
    }

    void SearchTarget() // 주변에 적을 검사해서 적을 향해 방향 트는 게 목표.
    {
        Debug.Log("Search Start");
        float Shortdist = Mathf.Infinity;                       // 최소값: 무한대
        float SecondShortdist = Mathf.Infinity;                 // 두 번째 최소값: 무한대 초기화
        float range = 100.0f;                                    // 탐색범위
        Transform ShortTarget = null;                           // 가까운 대상
        Transform SecondShortTarget = null;                     // 가까운 대상
        Transform ResultTarget = null;                          // 최종대상
        LayerMask EnemyLayer = LayerMask.NameToLayer("Target"); // 적 레이어 설정

        Debug.Log("Target: " + EnemyLayer);
        // 현재 자신의 위치에서 EnemyLayer를 가진 충돌체를 찾는다.


        /*
           주변 콜라이더 검출   

           검출된 에러
                1. 제일 가까운 것을 찾을 경우 이게 총알에서 찾는 거기 때문에 총알과 겹쳐있는 것이 제일 가까운 순위가 된다.
                
                해결방안: 두 번째로 가까운 것을 찾아보자

                2. 몬스터 레이어 설정이 되어 있지 않음

                해결방안: 전체검출

                3. 검출이 안 되고 있음.
        */
        Collider[] EnemyColliders = Physics.OverlapSphere(this.transform.position, range);

        if (EnemyColliders.Length > 0) // 검출된 게 있으면 진행.
        {
            foreach(Collider colTarget in EnemyColliders)
            {
                float Dist = Vector3.SqrMagnitude(this.transform.position - colTarget.transform.position);

                if (!colTarget.gameObject.tag.Equals("Monster")) // 몬스터 아니라면 스킵
                    continue;
                else if (Shortdist > Dist) // Dist가 최소값보다 작다면
                {
                    if(Shortdist < Dist & Dist < SecondShortdist)
                    {
                        SecondShortdist = Dist; // 두 번째 값
                        SecondShortTarget = colTarget.transform;
                    }
                    else
                    { 
                        Shortdist = Dist;   // 최소값
                        ShortTarget = colTarget.transform;
                    }
                }
            }
        }

        ResultTarget = SecondShortTarget; // 최종값
        

        if (ResultTarget != null)
        {
            Debug.Log(ResultTarget.position);
            // 검출된 콜라이더를 향해 방향 잡고
            Vector3 dir = ResultTarget.position - this.transform.position;
            dir.Normalize();

            // Rigidbody ribody = GetComponent<Rigidbody>();
            dir.y = 0;
            this.transform.Translate(dir * 5f * Time.deltaTime );
            //ribody.velocity = dir * 20f;

            Debug.Log("Search End");
        }
    }
}
