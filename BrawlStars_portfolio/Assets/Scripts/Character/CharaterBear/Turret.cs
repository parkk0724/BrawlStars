using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float fRange = 10.0f;
    public float fRotSpeed = 20f;
    public int BulletDamage = 10;
    public LayerMask LayerSearchTarget;
    private float FireDelay = 1.0f;         // �Ѿ˻��� �߻� ���� ����ġ
    private float FireDelayTime = 1.0f;     // Time.deltaTime���� ������ ��
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

            // 3��°: 1239
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
        if(FireDelay <= FireDelayTime)  // 1.0f <= 1.0f 0.5�ʸ��� �߻�, 
        {
            Debug.Log("�߻�!!");
            Instantiate(TurretBulletPrefab, tBulletPos.transform.position, tBulletPos.transform.rotation);
            FireDelayTime = 0.0f;       // �߻��ϸ� �Ѿ˵����� �ʱ�ȭ
        }
    }
    void SearchEnemy()
    {
        // ����: position �Ÿ�: fRange
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
    ������ ��
        1. �Ѿ� �߻�
            1-1) Instantiate(Resources.Load<GameObject>("prefabs/Turret/EmpTureetBullet"), bulletpos.pos, bulletpos.rot);


        2. �Ѿ� ��ũ��Ʈ ó��
            2-1) ������ �����ֱ�
                public float speed = 1000.0f;
                private Rigidbody rigidbody;

                rigidbody = GetComponent<Rigidbody>();
                rigidbody.AddForce(transform.forward * speed);

            2-2) ������ó�� 
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
    m_tfResultTarget = shorTarget; // ������
}

void LookatEnemy()
{
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    this.transform.LookAt(m_tfResultTarget);
}


    �ͷ� ���� ����� �߱�Ǵ� ����
        1. �ϴ� ����� �� ���ư���. -> ���� �߸��ƴ� or ����� ���� �߸� ���Դ� or ������Ʈ����.
                -> ������Ʈ ����


            // ------------------------ ����� �� ���ư��� �� �׷���?? ---------------------
            // LookRotation: Ư�� ��ǥ�� �ٶ󺸰� ����� ȸ���� ����
            //Quaternion LookRotation = Quaternion.LookRotation(tfTarget.position);

            // RotateTowards: A �������� B �������� C�� ���ǵ�� ȸ��
            //Vector3 VecterEulerValue = Quaternion.RotateTowards(this.transform.rotation, LookRotation, fRotSpeed).eulerAngles;           

            // ���Ϸ������� y�ุ �ݿ��ǰ� ������ �� ���ʹϿ����� ��ȯ
            //this.transform.rotation = Quaternion.Euler(0, VecterEulerValue.y, 0);

            //Debug.Log("LookRotation:" + LookRotation);
            //Debug.Log("���Ϸ���:" + VecterEulerValue);
            //Debug.Log("���� ȸ����:" + this.transform.rotation);
            // -------------------------- ���� �ذ� ���� �κ� ---------------------------------


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
            // �ذ���
            //      1. LookAt + offset �߰�
            //      2. ����, dir, y�� ȸ��
            

            //if (fFowardBackDotValue < 0) fAngle = -fAngle;

            // fAngle * Time.deltaTime;
            Debug.Log("����! ����: " + fAngle);
            
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, fAngle, 0f));


            /* 
             ���1: ����
                float fDotResult = Vector3.Dot(this.transform.forward, vDirecterToTarget);
                float r = Mathf.Acos(fDotResult);   // Radian Value, 2PI
                float DegreeAngle = 180 * r / Mathf.PI;   // Degree Value, 360
                float rotDir = 1.0f;
                float fRightBackDotValue = Vector3.Dot(this.transform.right, vDirecterToTarget); 
                if (fRightBackDotValue < 0.0f) rotDir = -1.0f;

             ���2
                while (DegreeAngle > 0.0f)
                { 
                    float delta = Time.deltaTime * fRotSpeed;
                    if (DegreeAngle - delta < 0.0f) delta = DegreeAngle;
                    this.transform.Rotate(0, DegreeAngle * rotDir, 0);
                }

            ���3 ������ ���� ���
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

            �׿� �õ�
            //Vector3 dir = tfTarget.position - this.transform.position;
            //dir.y = tfTarget.up.y * 0.1f;
            // ���� ȸ����, ��ǥ ����(ȸ��), ȸ�� �ӵ�
            // Quaternion.LookRotation: Ư�� ������ ���� �ϴ� �̴ϴ�. ��, dir �������� ���� ������� ����.
            // Quaternion.LookRotation(dir): return dir�� Quaternion��
            //Debug.Log(Quaternion.LookRotation(dir));
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), fRotSpeed * Time.deltaTime);
*/