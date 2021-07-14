using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Event : MonoBehaviour
{
    public GameObject bazooka_bullet;
    public Transform bazooka_bullet_pos;
    // Start is called before the first frame update
  

    private void Fire()
    {
        GameObject Bazooka_Bullet = Instantiate(bazooka_bullet, bazooka_bullet_pos.position, bazooka_bullet_pos.rotation);
        Bazooka_Bullet.transform.Rotate(Vector3.right * 45.0f);
    }
}
