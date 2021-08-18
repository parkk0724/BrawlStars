using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet_Manager : MonoBehaviour
{
    static private BazookaBullet_Manager _instance;

    static public BazookaBullet_Manager instance
    {
        get
        {
            if (!_instance)
            {
                GameObject obj = new GameObject("BazookaBullet_Manager");
                _instance = obj.AddComponent<BazookaBullet_Manager>();
            }

            return _instance;
        }
    }

    public Transform bulletPos;
    private int poolCount = 10;

    public List<GameObject> bullets = new List<GameObject>();

    public void CreateBullet()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject prefabs = Resources.Load<GameObject>("Prefabs/Bullet/BazookaMissile");
            GameObject bullet = Instantiate(prefabs, transform);
            bullet.name = "BazookaBullet_" + i;
            bullet.SetActive(false);

            bullets.Add(bullet);
        }
    }

    public void EnableBullet()
    {
        bulletPos = GameObject.Find("Bazooka_Bullet").transform;

        foreach (GameObject bullet in bullets)
        {
            if (bullet == null) continue;

            if (!bullet.activeSelf)
            {
                //Time.timeScale = 0.1f;
                bullet.transform.position = bulletPos.position;
                bullet.transform.rotation = bulletPos.rotation;

                bullet.SetActive(true);

                BazookaBullet basic_bullet = bullet.GetComponent<BazookaBullet>();
                basic_bullet.Fever_up = Soldier.instance.FeverUp;
                break;
            }
        }
    }
}
