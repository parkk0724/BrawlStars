using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item;
    public bool check = false;
    public void setItem(Item _item)
    {
        item.itemtype = _item.itemtype;
        item.use = _item.use;
        item.itemGrade = _item.itemGrade;
        item.itemName = _item.itemName;
        item.itemCount = _item.itemCount;
    }
    public Item GetItem()
    {
        return item;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    float dist = 0.0f;
    Coroutine item_updown;
    void Start()
    {
        StartCoroutine(Jumpitem());
        //item_updown = StartCoroutine(ItemUpDown());
    }
    IEnumerator ItemUpDown()
    {
        float dir = 1.0f;
        while (true)
        {
            float delta = 0.2f * Time.deltaTime;

            dist += delta;

            if (dist > 0.3f)
            {
                delta = 0.3f - (dist - delta);
                dist = 0.0f;
                dir *= -1.0f;
            }

            this.transform.Translate(Vector3.up * dir * delta, Space.World);

            yield return null;
        }

    }
    public bool Check()
    {
        return check;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            check = true;
        }
        else
        {
            check = false;
        }
    }
    IEnumerator Jumpitem()
    {
        Vector3 StartPos = this.transform.position;

        float xPos = Random.Range(-2, 2);
        float zPos = Random.Range(-2, 2);

        Vector3 EndPos = new Vector3(StartPos.x + xPos, this.transform.position.y, StartPos.z + zPos);
        Vector3 dir = EndPos - StartPos;
        float dist = Vector3.Distance(StartPos, EndPos);
        float dist_2 = 0;
        dir.Normalize();
        float Speed = 1f;
        float delta = 0;
        while (dist_2 / dist <= 0.9)
        {
            delta += Time.deltaTime * Speed;
            dist_2 += delta;
            if (dist_2 - delta < 0)
            {
                delta = dist_2 - dist;
            }
            float height = Mathf.Sin(dist_2 / dist * Mathf.PI);

            this.transform.Translate(dir * delta, Space.World);

            Vector3 pos = this.transform.position;
            pos.y = height;
            this.transform.position = pos;

            //this.transform.position = StartPos;


            //transform.position += dir * delta;
            //Vector3 pos = Vector3.Lerp(StartPos, EndPos, angle);


            yield return null;
        }

    }
}
