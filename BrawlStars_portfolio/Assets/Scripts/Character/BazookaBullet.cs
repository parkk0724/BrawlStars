using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    public GameObject player;
    public float speed = 5.0f;
    public float range = 7.0f;
    public float height = 3.0f;

    float dist = 0.0f;    
    bool crash = true;

    Coroutine bulletflying;
    // Start is called before the first frame update
    void Start()
    {
        bulletflying = StartCoroutine(BulletFlying());
        if (!crash) StopCoroutine(BulletFlying());
    }

    // Update is called once per frame
    
    IEnumerator BulletFlying()
    {
        Vector3 pos = this.transform.position;
        while (pos.y != 0.0f)
        {           
            float delta = speed * Time.deltaTime;
            dist += delta;

            pos.z += delta;
            pos.y = (Mathf.Sin((Mathf.PI / range) * pos.z)) * height;

            this.transform.position = pos;

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        crash = false;
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
