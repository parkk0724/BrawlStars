using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    public int damage = 20;
    public float curTime = 0.0f;
    public float BulletLiveMaxTime = 2.0f;
    Vector3 TargetDirVector; 
    Coroutine Temp;

    private void Start()
    {
        StartCoroutine("DestoryBullet");
        Rigidbody ribody = GetComponent<Rigidbody>();
        ribody.velocity = this.transform.forward * 5f;
        TargetDirVector = this.transform.forward * 5f * Time.deltaTime;
    }

    private void Update()
    {
        this.transform.Translate(TargetDirVector, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle")
            Destroy(gameObject);

        if (other.gameObject.tag == "Monster")
        { 
            Debug.Log("���� ����");
            SearchTarget();
            //other.GetComponent<Monster>().Hit(damage, Color.red);
        }
    }
    IEnumerator DestoryBullet()
    {
        yield return new WaitForSecondsRealtime(4);
        Destroy(gameObject);
    }

    void SearchTarget() // �ֺ��� ���� �˻��ؼ� ���� ���� ���� Ʈ�� �� ��ǥ.
    {
        Debug.Log("Search Start");
        float Shortdist = Mathf.Infinity;                       // �ּҰ�: ���Ѵ�
        float SecondShortdist = Mathf.Infinity;                 // �� ��° �ּҰ�: ���Ѵ� �ʱ�ȭ
        float range = 3.0f;                                    // Ž������
        Transform ShortTarget = null;                           // ����� ���
        Transform SecondShortTarget = null;                     // ����� ���
        Transform ResultTarget = null;                          // �������
        LayerMask EnemyLayer = LayerMask.NameToLayer("Target"); // �� ���̾� ����

        Debug.Log("Target: " + EnemyLayer);
        // ���� �ڽ��� ��ġ���� EnemyLayer�� ���� �浹ü�� ã�´�.

        /*
          ���� �˰���
            1. �Ѿ��� �ε�ģ��. OnTriggerEnter ����
            2. OnTrigeerEnter
                {
                    collider[] arrCol = OverlapSphere(����, �ݰ�)

                    foreach(collider col in arrCol)
                     {
                        float distance = sqrmagnitude(�ݶ��̴�.pos - �ҷ���ġ.pos);

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


           �ֺ� �ݶ��̴� ����   

           ����� ����
                1. ���� ����� ���� ã�� ��� �̰� �Ѿ˿��� ã�� �ű� ������ �Ѿ˰� �����ִ� ���� ���� ����� ������ �ȴ�.
                
                    �ذ���: �� ��°�� ����� ���� ã�ƺ���


                2. ���� ���̾� ������ �Ǿ� ���� ����

                    �ذ���: ��ü����


                3. ������ �� �ǰ� ����.
        */

        Collider[] EnemyColliders = Physics.OverlapSphere(this.transform.position, range);

        if (EnemyColliders.Length > 0) // ����� �� ������ ����.
        {
            foreach(Collider colTarget in EnemyColliders)
            {
                float Dist = Vector3.SqrMagnitude(this.transform.position - colTarget.transform.position);

                if (!colTarget.gameObject.tag.Equals("Monster")) // ���� �ƴ϶�� ��ŵ
                    continue;

                if (Dist < Shortdist)
                {
                    Shortdist = Dist;
                    ShortTarget = colTarget.transform;
                }

                else if (Dist < SecondShortdist || Dist > Shortdist)
                {
                    SecondShortdist = Dist;
                    SecondShortTarget = colTarget.transform;
                }
            }
                //else if (Shortdist > Dist) // Dist�� �ּҰ����� �۴ٸ�
                //{
                //    if(Shortdist < Dist & Dist < SecondShortdist)
                //    {
                //        SecondShortdist = Dist; // �� ��° ��
                //        SecondShortTarget = colTarget.transform;
                //    }
                //    else
                //    { 
                //        Shortdist = Dist;   // �ּҰ�
                //        ShortTarget = colTarget.transform;
                //    }
        }

        ResultTarget = SecondShortTarget; // ������

        if (ResultTarget != null)
        {
            Debug.Log(ResultTarget.position);
            // ����� �ݶ��̴��� ���� ���� ���
            Vector3 dir = ResultTarget.position - this.transform.position;
            dir.Normalize();
            dir.y = 0f;
            TargetDirVector = dir * 5f * Time.deltaTime;

            //������Ȳ
            /* �ش� �������� źȯ�� �� Ʋ�. ������ ���� �� ����.
             
             */
            //this.transform.LookAt(ResultTarget);
            //Rigidbody ribody = GetComponent<Rigidbody>();
            //ribody.velocity = Vector3.zero;
            //ribody.velocity = dir * 5f;
            //dir.y = 0;
            // this.transform.Translate(dir * 5f * Time.deltaTime );

            Debug.Log("Search End");
        }
    }
}
