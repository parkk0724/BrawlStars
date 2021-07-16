using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float range = 7.0f;
    public float height = 3.0f;

    Rigidbody myRigid;

    Vector3 curPos;
    Vector3 prePos;

    float dist = 0.0f;

    Coroutine bulletflying;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = this.GetComponent<Rigidbody>();
        bulletflying = StartCoroutine(BulletFlying());
    }

    IEnumerator BulletFlying()
    {
        dist += 0.5f;// ����ī������ �߻��ϴ� ���̰� 0���̺��� ũ�Ƿ� sin�׷������� �߰����� �����ϴ°ǵ� ��Ȯ�� ��ġ ����� ������ ���� �ϴ� 0.5��ŭ ������
        while (dist < range)
        {
            //�� ��ġ, ������ ����

            float delta = speed * Time.deltaTime;           
           
            if (range - dist < 0.0f)
            {
                delta = range - (dist - delta);
            }
            dist += delta;
            
            //�̵� ���� ���� ���̸� sin ���� ���� ���� �� ����
            this.transform.Translate(Vector3.forward * delta);
            float h = Mathf.Sin(dist * (Mathf.PI / range)) * height; //0.5�� ó�� ���� ��ġ ���� �� 
            Vector3 pos = this.transform.position;

            pos.y = h;
            this.transform.position = pos;

            //�� ��ġ, ���� -> ���� ��ġ, ������ �����ؼ� ������ ����

            yield return null;
        }
        dist = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("dd");
            Destroy(this.gameObject);
        }
    }
}
