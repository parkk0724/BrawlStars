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
        dist += 0.7f;// 바주카포에서 발사하는 높이가 0높이보다 크므로 sin그래프에서 중간부터 시작하는건데 정확한 수치 계산이 지금은 힘들어서 일단 0.7만큼 보정함
        while (dist < range)
        {
            //전 위치, 방향을 저장
            prePos = this.transform.position;
            Vector3 predir_forward = this.transform.forward;
            Vector3 predir_up = this.transform.up;
            float delta = speed * Time.deltaTime;           
           
            if (range - dist < 0.0f)
            {
                delta = range - (dist - delta);
            }
            dist += delta;
            
            //이동 값에 따른 높이를 sin 값을 통해 높이 값 도출
            this.transform.Translate(this.transform.forward * delta, Space.World);

            float h = Mathf.Sin(dist * (Mathf.PI / range)) * height; 
            Vector3 pos = this.transform.position;
            pos.y = h;
            this.transform.position = pos;

            //전 위치, 방향 -> 현재 위치, 방향을 내적해서 각도를 구함
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
