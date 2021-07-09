using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_Moving : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.0f;

    Coroutine cameramove = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    speed = speed * 1.5f;
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //    speed = speed * (2 / 3);

        float delta = speed * Time.deltaTime;
    }
}
