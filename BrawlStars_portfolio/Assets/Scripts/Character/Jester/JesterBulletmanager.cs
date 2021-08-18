using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterBulletmanager : MonoBehaviour
{
    static private JesterBulletmanager _instance;

    static public JesterBulletmanager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("JesterBulletmanager");
                _instance = obj.AddComponent<JesterBulletmanager>();
            }
            return _instance;
        }
    }

    private Dictionary<string, List<GameObject>> totalBullet = new Dictionary<string, List<GameObject>>();

    public void AddBullet(string key, int poolcount = 10)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Bullet/" + key);

        List<GameObject> bullets = new List<GameObject>();

        for (int i = 0; i < poolcount; i++)
        {
            GameObject bullet = Instantiate(prefab, transform);
            bullet.SetActive(false);
            bullet.name = key + "-" + i;

            bullets.Add(bullet);

        }
        totalBullet.Add(key, bullets);
    }

    public void Fire(string key, Transform trans)
    {
        if (!totalBullet.ContainsKey(key))
        {
            return;
        }
        List<GameObject> bullets = totalBullet[key];

        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                bullet.transform.position = trans.position;
                bullet.transform.rotation = trans.rotation;
                bullet.SetActive(true);

                return;
            }
        }
    }
}
