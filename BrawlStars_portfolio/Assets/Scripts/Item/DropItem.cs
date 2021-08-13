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

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 dir_2 = StartPos - player.position;
        dir_2.Normalize();

        Vector3 EndPos = new Vector3(StartPos.x + xPos, this.transform.position.y, StartPos.z + zPos);
        Vector3 Endpos_2 = StartPos + dir_2 * 2;

        float dist = Vector3.Distance(this.transform.position, player.transform.position);
        float Speed = 2f;
        float delta = 0;

        if (dist > 4)
        {
            while (delta < 0.9)
            {
                delta += Time.deltaTime * Speed;
                float height = Mathf.Sin(delta * Mathf.PI) * 2;
                Vector3 pos = Vector3.Lerp(StartPos, EndPos, delta);
                pos.y = height;
                this.transform.position = pos;
                yield return null;

                #region FirstSolution
                //delta += Time.deltaTime * Speed;
                //dist_2 += delta;
                //if (dist_2 - delta < 0)
                //{
                //    delta = dist_2 - dist;
                //}
                //float height = Mathf.Sin(dist_2 / dist * Mathf.PI);
                //
                //this.transform.Translate(dir * delta, Space.World);
                #endregion
            }
        }
        else
        {
            while (delta < 0.9)
            {
                delta += Time.deltaTime * Speed;
                float height = Mathf.Sin(delta * Mathf.PI) * 2;
                Vector3 pos = Vector3.Lerp(StartPos, Endpos_2, delta);
                pos.y = height;
                this.transform.position = pos;
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ItemUpDown());
    }
}
