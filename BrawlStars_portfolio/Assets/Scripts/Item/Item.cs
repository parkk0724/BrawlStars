using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    float dist = 0.0f;
    //float dir = 1.0f;

    Coroutine item_updown;
    public enum ItemType
    {
        NONE, HEAL
    }    
    // Start is called before the first frame update
    void Start()
    {
        item_updown = StartCoroutine(ItemUpDown());
    }

    // Update is called once per frame
    void Update()
    {
     
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
