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
                obj.transform.SetParent(GameObject.Find("Soldier").transform);
                _instance = obj.AddComponent<BazookaBullet_Manager>();
            }

            return _instance;
        }
    }

    private int poolCount = 10;

    public void CreateBullet()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject prefabs = Resources.Load<GameObject>("Prefabs/Bullet/BazookaMissile");
            GameObject bullet = Instantiate(prefabs, transform);
            bullet.name = "BazookaBullet_" + i;
            bullet.SetActive(false);
        }
    }
}
