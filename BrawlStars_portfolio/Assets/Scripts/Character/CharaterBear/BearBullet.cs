using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    public int damage;
    Coroutine Temp;

    private void Start()
    {
        StartCoroutine("DestoryBullet");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall") 
            Destroy(gameObject);
        if (other.gameObject.tag == "Monster") Debug.Log("���� ����");
            // other.GetComponent<Monster>().Hit();
    }

    IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}