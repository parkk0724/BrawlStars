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
        dist += 0.5f;// 바주카포에서 발사하는 높이가 0높이보다 크므로 sin그래프에서 중간부터 시작하는건데 정확한 수치 계산이 지금은 힘들어서 일단 0.5만큼 보정함
        while (dist < range)
        {
            //전 위치, 방향을 저장

            float delta = speed * Time.deltaTime;           
           
            if (range - dist < 0.0f)
            {
                delta = range - (dist - delta);
            }
            dist += delta;
            
            //이동 값에 따른 높이를 sin 값을 통해 높이 값 도출
            this.transform.Translate(Vector3.forward * delta);
            float h = Mathf.Sin(dist * (Mathf.PI / range)) * height; //0.5는 처음 시작 위치 보정 값 
            Vector3 pos = this.transform.position;

            pos.y = h;
            this.transform.position = pos;

            //전 위치, 방향 -> 현재 위치, 방향을 내적해서 각도를 구함

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
