using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item;
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
        
        item_updown = StartCoroutine(ItemUpDown());
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
}
