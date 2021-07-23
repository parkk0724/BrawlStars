using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Moving : MonoBehaviour
{
    public GameObject playercamera;
    public GameObject player;

    public float speed = 0.0f;
    public bool startmove = true;
    Coroutine cameramove = null;

    float dist = 0.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 캐릭바꿀때마다 변경해야되서 알아서 찾아서 넣어주게 함. -금환
        if (startmove == true)
            cameramove = StartCoroutine(StartCameraMoving());
        else
        {
            this.transform.position = playercamera.transform.position;
        }
        dist = Mathf.Abs(player.transform.position.z - playercamera.transform.position.z);
    }
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.z = player.transform.position.z - dist;

        if (cameramove == null)
        {
           // if (Vector3.Dot(this.transform.forward, player.transform.forward) == -1)
           // {
           //     pos.z = pos.z - 5.0f;
           //     this.transform.position = Vector3.Lerp(this.transform.position, pos, 1.0f * Time.deltaTime);
           // }
           // else
           // {
                this.transform.position = Vector3.Lerp(this.transform.position, pos, 3.0f * Time.deltaTime);
           // }
        }
    }

    IEnumerator StartCameraMoving()
    {
        Vector3 dir = playercamera.transform.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = speed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            this.transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        cameramove = null;
    }
}
