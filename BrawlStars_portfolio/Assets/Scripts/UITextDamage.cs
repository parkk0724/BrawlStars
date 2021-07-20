using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_Text[] textDamage;
    static int count = 0;
    void Start()
    {
        textDamage = GetComponentsInChildren<TMPro.TMP_Text>();
        for (int i = 0; i < textDamage.Length; i++)
        {
            Init(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int n)
    {
        textDamage[n].text = "";
        textDamage[n].color = Color.white;
        textDamage[n].gameObject.SetActive(false);
    }

    public void SetDamage(int damage, Vector3 pos, Color c)
    {
        textDamage[count].color = c;
        textDamage[count].text = damage.ToString();
        textDamage[count].gameObject.SetActive(true);
        textDamage[count].transform.position = pos;
        
        StartCoroutine(ShowDamage(count));

        if (count >= textDamage.Length-1)
            count = 0;
        else
            count++;
    }

    IEnumerator ShowDamage(int n)
    {
        Color color = textDamage[n].color;
        Vector3 pos = textDamage[n].transform.position;

        while (color.a > 0.0f)
        {
            color.a -= 0.01f;
            pos.y += 0.01f;
            textDamage[n].color = color;
            textDamage[n].transform.position = Camera.main.WorldToScreenPoint(pos);

            yield return new WaitForSeconds(0.01f);
        }

        Init(n);

    }

}
