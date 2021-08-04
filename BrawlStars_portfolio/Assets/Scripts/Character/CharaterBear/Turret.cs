using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float fRange = 10.0f;
    public float fRotSpeed = 20f;
    public int BulletDamage = 10;
    public LayerMask LayerSearchTarget;
    private float FireDelay = 1.0f;         // 총알생성 발사 간격 기준치
    private float FireDelayTime = 1.0f;     // Time.deltaTime으로 더해줄 것
    public Transform tBulletPos = null;
    public Transform tfTarget;
    public GameObject TurretBulletPrefab = null;
    Transform ShortTransform = null;

    void Start()
    {
        TurretBulletPrefab = Resources.Load<GameObject>("Prefabs/Turret/TurretBoolet_ToonMagic_small");
        tBulletPos = transform.Find("TurretBulletPos");
        //Transform[] childrens = GetComponentsInChildren<Transform>();
        //
        //foreach (Transform tf in childrens)
        //{
        //    if (tf.name == "BulletPos") tBulletPos = tf;
        //}

        InvokeRepeating("SearchEnemy", 0f, 0.5f);
    }

    void Update()
    {
        if (tfTarget != null)
        {
            Vector3 vDirecterToTarget = (tfTarget.position - this.transform.position).normalized;
            vDirecterToTarget.y = 0f;

            // 3번째: 1239
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(vDirecterToTarget),
                                                  fRotSpeed * Time.deltaTime);

            Fire();
        }
        else
            this.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void Fire()
    {
        FireDelayTime += Time.deltaTime;
        if(FireDelay <= FireDelayTime)  // 1.0f <= 1.0f 0.5초마다 발사, 
        {
            Debug.Log("발사!!");
            Instantiate(TurretBulletPrefab, tBulletPos.transform.position, tBulletPos.transform.rotation);
            FireDelayTime = 0.0f;       // 발사하면 총알딜레이 초기화
        }
    }
    void SearchEnemy()
    {
        // 중점: position 거리: fRange
        Collider[] Cols = Physics.OverlapSphere(this.transform.position, fRange);
        tfTarget = null;

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
    }

}

/*
    개발할 것
        1. 총알 발사
            1-1) Instantiate(Resources.Load<GameObject>("prefabs/Turret/EmpTureetBullet"), bulletpos.pos, bulletpos.rot);


        2. 총알 스크립트 처리
            2-1) 앞으로 날려주기
                public float speed = 1000.0f;
                private Rigidbody rigidbody;

                rigidbody = GetComponent<Rigidbody>();
                rigidbody.AddForce(transform.forward * speed);

            2-2) 데미지처리 
                 col.getcomponent<Monster>().Hit(10, color.red);
                
*/

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

            //this.transform.LookAt(tfTarget.position );
            // 해결방안
            //      1. LookAt + offset 추가
            //      2. 내적, dir, y축 회전
            

            //if (fFowardBackDotValue < 0) fAngle = -fAngle;

            // fAngle * Time.deltaTime;
            Debug.Log("돈다! 각도: " + fAngle);
            
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, fAngle, 0f));


            /* 
             방법1: 내적
                float fDotResult = Vector3.Dot(this.transform.forward, vDirecterToTarget);
                float r = Mathf.Acos(fDotResult);   // Radian Value, 2PI
                float DegreeAngle = 180 * r / Mathf.PI;   // Degree Value, 360
                float rotDir = 1.0f;
                float fRightBackDotValue = Vector3.Dot(this.transform.right, vDirecterToTarget); 
                if (fRightBackDotValue < 0.0f) rotDir = -1.0f;

             방법2
                while (DegreeAngle > 0.0f)
                { 
                    float delta = Time.deltaTime * fRotSpeed;
                    if (DegreeAngle - delta < 0.0f) delta = DegreeAngle;
                    this.transform.Rotate(0, DegreeAngle * rotDir, 0);
                }

            방법3 외적을 통한 방법
                public Transform monster;

                private void update()
                {
                    vector3 forwad = transform.froward;
    
                    vector3 destDir = monster.position - transform.position;
                    forward.y = 0;
                    destDir.y = 0;

                    Vector3 cross = Vector3.Cross(forward, destDir);
                    
                    if(corss.y > 0) transform.rotate(Vecotr3.up * speed * Time.deltaTime);
                    else if(cross.y < 0) transform.rotate(Vector3.up * -spedd * Time.deltaTime);
                }

            그외 시도
            //Vector3 dir = tfTarget.position - this.transform.position;
            //dir.y = tfTarget.up.y * 0.1f;
            // 현재 회전값, 목표 방향(회전), 회전 속도
            // Quaternion.LookRotation: 특정 방향을 보게 하는 겁니다. 즉, dir 방향으로 고개를 돌리라는 거죠.
            // Quaternion.LookRotation(dir): return dir의 Quaternion값
            //Debug.Log(Quaternion.LookRotation(dir));
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), fRotSpeed * Time.deltaTime);
*/