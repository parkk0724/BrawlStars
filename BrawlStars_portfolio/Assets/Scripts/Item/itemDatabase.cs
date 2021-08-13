using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {

    static private itemDatabase _instance;
    static public itemDatabase instance
    {
        get
        {
            if (_instance == null) //인스턴스가 널일때만
            {
                GameObject obj = new GameObject("itemDatabase"); //하이러키창의 크리에이트 엠티랑 같음
                _instance = obj.AddComponent<itemDatabase>();
            }

            return _instance;
        }

    }
    public List<Item> itemList = new List<Item>();
    GameObject[] monster;
    int itemDorp_Nomal = 1;
    int itemDorp_Boss = 3;
    public Dictionary<int, List<reitem>> ItemData = new Dictionary<int, List<reitem>>();

    void Start()
    {
        LoadItemTextData();
        #region FirstSolution ITemList
        itemList.Add(new Item(ITemType.Potion, USE.HP, ITemGrade.D, "HpPotion", 50, Resources.Load<GameObject>("Prefabs/Item/HpPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.STAMINA, ITemGrade.D, "StaminaPotion", 2, Resources.Load<GameObject>("Prefabs/Item/StaminaPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.FEVER, ITemGrade.D, "FeverPotion", 50, Resources.Load<GameObject>("Prefabs/Item/FeverPotion")));
        itemList.Add(new Item(ITemType.Potion, USE.INVINCIBLE, ITemGrade.C, "InviciblePotion", 0, Resources.Load<GameObject>("Prefabs/Item/InviciblePotion")));
        #endregion
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
                for (int i = 0; i < _instance.itemList.Count; i++)
                {
                    if (_instance.itemList[i].itemGrade == (ITemGrade)3) // 등급비교
                    {
                        table.Add(i); //테이블에 아이템의 인덱스값을 저장
                    }
                }
                int tableindex = Random.Range(0, table.Count); // 테이블의 값을 랜덤으로 돌린 인덱스값
                GameObject obj = Instantiate(_instance.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, _instance.itemList[(int)table[tableindex]].itemPrefab.transform.rotation); //생성
                obj.GetComponent<DropItem>().item.itemtype = _instance.itemList[(int)table[tableindex]].itemtype; //생성된 드랍아이템의 아이템 타입을 아이템데이터베이스 형식으로 바꿈
                obj.GetComponent<DropItem>().item.itemName = _instance.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = _instance.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = _instance.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = _instance.itemList[(int)table[tableindex]].use;
            }


            else if (firstrage < 10)
            {
                ArrayList table = new ArrayList();
                for (int i = 0; i < _instance.itemList.Count; i++)
                {
                    if (_instance.itemList[i].itemGrade == (ITemGrade)2)
                    {
                        table.Add(i);
                    }
                }
                int tableindex = Random.Range(0, table.Count);
                GameObject obj = Instantiate(_instance.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, _instance.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);
                obj.GetComponent<DropItem>().item.itemtype = _instance.itemList[(int)table[tableindex]].itemtype;
                obj.GetComponent<DropItem>().item.itemName = _instance.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = _instance.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = _instance.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = _instance.itemList[(int)table[tableindex]].use;
            }
        } 
    }

    void LoadItemTextData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TextData/Item");

        string temp = textAsset.text.Replace("\r\n", "\n");
        string[] row = temp.Split('\n');
        List<reitem> myitem = new List<reitem>();

        for (int i = 1; i < row.Length; i++)
        {
            string[] data = row[i].Split(',');

            myitem.Add(new reitem(int.Parse(data[0]), data[1], data[2], data[3], data[4], int.Parse(data[5]), data[6]));

            ItemData.Add(int.Parse(data[0]), myitem);
        }
    }
}
