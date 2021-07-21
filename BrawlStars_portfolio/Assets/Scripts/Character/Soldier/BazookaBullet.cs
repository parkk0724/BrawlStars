using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    public GameObject explosion_effect;
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
        dist += 0.7f;// ����ī������ �߻��ϴ� ���̰� 0���̺��� ũ�Ƿ� sin�׷������� �߰����� �����ϴ°ǵ� ��Ȯ�� ��ġ ����� ������ ���� �ϴ� 0.7��ŭ ������
        while (dist < range)
        {
            //�� ��ġ, ������ ����
            prePos = this.transform.position;
            Vector3 predir_forward = this.transform.forward;
            Vector3 predir_up = this.transform.up;
            float delta = speed * Time.deltaTime;           
           
            if (range - dist < 0.0f)
            {
                delta = range - (dist - delta);
            }
            dist += delta;
            
            //�̵� ���� ���� ���̸� sin ���� ���� ���� �� ����
            this.transform.Translate(this.transform.forward * delta, Space.World);

            float h = Mathf.Sin(dist * (Mathf.PI / range)) * height; 
            Vector3 pos = this.transform.position;
            pos.y = h;
            this.transform.position = pos;

            //�� ��ġ, ���� -> ���� ��ġ, ������ �����ؼ� ������ ����
            Vector3 dir = this.transform.position - prePos;
            dir.Normalize();
            float d = Vector3.Dot(predir_forward, dir);
            float r = Mathf.Acos(d);
            float e = 180.0f * (r / Mathf.PI);

           if (Vector3.Dot(predir_up, dir) >= 0.0f)
               this.transform.Rotate(-this.transform.right * e, Space.World);
           else
               this.transform.Rotate(this.transform.right * e, Space.World);

            yield return null;
        }
        dist = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Obstacle" || other.tag == "Wall" || other.tag == "Monster" || other.tag == "Player")
        {
            GameObject Explosion_Effect = Instantiate(explosion_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
