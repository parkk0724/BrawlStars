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
            // �ذ���
            //      1. LookAt + offset �߰�
            //      2. ����, dir, y�� ȸ��

        }
        else
            this.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void SearchEnemy()
    {
        // ����: position �Ÿ�: fRange
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
        Debug.Log("Ÿ����ġ: " + tfTarget.position);

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
*/