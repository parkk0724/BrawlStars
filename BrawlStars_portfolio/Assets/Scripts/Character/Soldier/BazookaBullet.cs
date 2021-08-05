using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BazookaBullet : MonoBehaviour
{
    public UnityAction Fever_up = null;

    public GameObject explosion_effect;
    public GameObject Explosion_Sound;
    public float speed = 5.0f;
    public float range = 7.0f;
    public float height = 3.0f;

    float m_fDamage = 25.0f;

    BoxCollider collider_size = null;

    Rigidbody myRigid;

    Vector3 curPos;
    Vector3 prePos;

    float dist = 0.0f;

    Coroutine bulletflying;

    // Start is called before the first frame update
    void Start()
    {
        collider_size = this.GetComponent<BoxCollider>();
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
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 2.0f);

        if (other.tag == "Ground" || other.tag == "Wall" || other.tag == "Player")
        {           
            GameObject Explosion_Effect = Instantiate(explosion_effect, this.transform.position, Quaternion.identity);
            Explosion_Sound = GameObject.Find("BazookaExplosion");
            Explosion_Sound.GetComponent<AudioSource>().Play();
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].tag == "Monster")
                {
                    colls[i].GetComponent<Monster>()?.Hit((int)m_fDamage, new Color(0, 0, 0, 1));
                    Fever_up?.Invoke();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
