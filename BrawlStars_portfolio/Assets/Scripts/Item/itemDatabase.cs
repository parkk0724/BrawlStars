using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {

    static private itemDatabase instane;
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
    int itemDorp_Nomal = 1;
    int itemDorp_Boss = 3;


    void Start()
    {
        itemList.Add(new Item(ITemType.Potion, USE.HP, ITemGrade.D, "HpPotion", 50, Resources.Load<GameObject>("Prefabs/Item/HpPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.STAMINA, ITemGrade.D, "StaminaPotion", 2, Resources.Load<GameObject>("Prefabs/Item/StaminaPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.FEVER, ITemGrade.D, "FeverPotion", 50, Resources.Load<GameObject>("Prefabs/Item/FeverPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.INVINCIBLE, ITemGrade.C, "InviciblePotion", 0, Resources.Load<GameObject>("Prefabs/Item/InviciblePotion")));

    }
    void Update()
    {
        monsterSearch(itemDorp_Nomal , itemDorp_Boss);
    }
    void monsterSearch(int DropCount , int DropCount_2)
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
                    if (monster[i].GetComponent<Monster>().GetHp() <= 0 && !monster[i].GetComponent<BossMonster>())
                    {
                        Transform monsterTransform;
                        monsterTransform = monster[i].transform;
                        ItemDrop(monsterTransform, DropCount);
                    }
                }
            }
        }
    }
    public void ItemDrop(Transform monsterTansform, int _itemCount) // 아이템드랍
    {
        for (int z = 0; z < _itemCount; z++) // 아이템 드랍 몇번 할꺼냐
        {
            int firstrage = Random.Range(0, 10); 
            if (firstrage < 4)
            {
                return;
            }
            else if (firstrage < 9)
            {
                ArrayList table = new ArrayList();
                for (int i = 0; i < instane.itemList.Count; i++)
                {
                    if (instane.itemList[i].itemGrade == (ITemGrade)3) // 등급비교
                    {
                        table.Add(i); //테이블에 아이템의 인덱스값을 저장
                    }
                }
                int tableindex = Random.Range(0, table.Count); // 테이블의 값을 랜덤으로 돌린 인덱스값
                GameObject obj = Instantiate(instane.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, instane.itemList[(int)table[tableindex]].itemPrefab.transform.rotation); //생성
                obj.GetComponent<DropItem>().item.itemtype = instane.itemList[(int)table[tableindex]].itemtype; //생성된 드랍아이템의 아이템 타입을 아이템데이터베이스 형식으로 바꿈
                obj.GetComponent<DropItem>().item.itemName = instane.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = instane.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = instane.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = instane.itemList[(int)table[tableindex]].use;
            }


            else if (firstrage < 10)
            {
                ArrayList table = new ArrayList();
                for (int i = 0; i < instane.itemList.Count; i++)
                {
                    if (instane.itemList[i].itemGrade == (ITemGrade)2)
                    {
                        table.Add(i);
                    }
                }
                int tableindex = Random.Range(0, table.Count);
                GameObject obj = Instantiate(instane.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, instane.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);
                obj.GetComponent<DropItem>().item.itemtype = instane.itemList[(int)table[tableindex]].itemtype;
                obj.GetComponent<DropItem>().item.itemName = instane.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = instane.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = instane.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = instane.itemList[(int)table[tableindex]].use;
            }
        } 
    }
}
