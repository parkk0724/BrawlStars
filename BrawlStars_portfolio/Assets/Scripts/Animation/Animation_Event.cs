using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Event : MonoBehaviour
{
    public GameObject player;
    public GameObject bazooka_bullet;
    public Transform bazooka_bullet_pos;
    // Start is called before the first frame update
    private void Fire()
    {
        GameObject Bazooka_Bullet = Instantiate(bazooka_bullet, bazooka_bullet_pos.position, Quaternion.identity);
        float dot = Vector3.Dot(Bazooka_Bullet.transform.forward, player.transform.forward);
        float r = Mathf.Acos(dot);
        float e = 180.0f * (r / Mathf.PI);

        if (Vector3.Dot(player.transform.forward, bazooka_bullet.transform.right) >= 0)
            Bazooka_Bullet.transform.Rotate(Bazooka_Bullet.transform.up * e);
        else
            Bazooka_Bullet.transform.Rotate(-Bazooka_Bullet.transform.up * e);
    }
}
