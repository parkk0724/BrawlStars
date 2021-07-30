using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {

    static public itemDatabase instane;
    itemDatabase database;
    private void Awake()
    {
        if(instane != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); ;
            instane = this;
        }
    }
    public List<Item> itemList = new List<Item>();
    GameObject[] monster;
    Transform monsterTransform;
    void Start()
    {
        itemList.Add(new Item(ITemType.Potion, USE.HP, ITemGrade.D, "HpPotion", 50, Resources.Load<GameObject>("Prefabs/Item/HpPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.STAMINA, ITemGrade.D, "StaminaPotion", 2, Resources.Load<GameObject>("Prefabs/Item/StaminaPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.FEVER, ITemGrade.D, "FeverPotion", 50, Resources.Load<GameObject>("Prefabs/Item/FeverPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.INVINCIBLE, ITemGrade.C, "InviciblePotion", 0, Resources.Load<GameObject>("Prefabs/Item/InviciblePotion")));

       
        database = FindObjectOfType<itemDatabase>();
      
    }
    void Update()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");
        if (monster.Length <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < monster.Length; i++)
            {
                if (monster[i].GetComponent<Monster>())
                {
                    if (monster[i].GetComponent<Monster>().GetHp() <= 0)
                    {
                        monsterTransform = monster[i].transform;
                        ItemDrop();
                    }
                }
            }
        }
    }

    public void ItemDrop()
    {
       
        int firstrage = Random.Range(0, 10);
        if (firstrage < 3)
        {
            return;
        }
        else if (firstrage < 6)
        {
            ArrayList table = new ArrayList();
            for (int i = 0; i < database.itemList.Count; i++)
            {
                if (database.itemList[i].itemGrade == (ITemGrade)3)
                {
                    table.Add(i);
                }
            }
            int tableindex = Random.Range(0, table.Count);
            Instantiate(database.itemList[(int)table[tableindex]].itemPrefab, monsterTransform.position, database.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);

        }
        else if (firstrage < 10)
        {
            ArrayList table = new ArrayList();
            for (int i = 0; i < database.itemList.Count; i++)
            {
                if (database.itemList[i].itemGrade == (ITemGrade)2)
                {
                    table.Add(i);
                }
            }
            int tableindex = Random.Range(0, table.Count);
            Instantiate(database.itemList[(int)table[tableindex]].itemPrefab, monsterTransform.position, database.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);
        }
    }
}
