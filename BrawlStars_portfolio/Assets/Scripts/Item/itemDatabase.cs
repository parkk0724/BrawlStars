using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{

    static public itemDatabase instance;
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); ;
            instance = this;
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
        monsterSearch(itemDorp_Nomal, itemDorp_Boss);
    }
    void monsterSearch(int DropCount, int DropCount_2)
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
                        Dropitem(monsterTransform, DropCount);
                    }
                }
            }
        }
    }

    void Dropitem(Transform monsterTansform, int _itemCount)
    {
        for (int z = 0; z < _itemCount; z++) // 아이템 드랍 몇번 할꺼냐
        {
            int DropRange = Random.Range(0, 10);
            if (DropRange < 3)
            {
                return;
            }
            else if (DropRange < 9)
            {
                ArrayList table = new ArrayList();

                foreach (var keyValue in ItemData) //for문 사용시 이상하게 잘안됨
                {
                    for (int i = 0; i < keyValue.Value.Count; i++)
                    {
                        if (keyValue.Value[i].itemGrade == "D") //키 벨루의 아이템 등급을 비교한다
                        {
                            table.Add(keyValue.Key);
                        }
                    }
                }
                #region 실패의 방법
                //for (int i = 0; i < instance.ItemData.Count; i++)
                //{
                //    for (int j = 0; j < instance.ItemData[i].Count; j++)
                //    {
                //        if(instance.ItemData[i][j].itemGrade == "D")
                //        {
                //            table.Add(i);
                //        }
                //    }
                //}
                //Prefabs/Item/FeverPotion
                #endregion
                int tableindex = Random.Range(0, table.Count);// 테이블의 값을 랜덤으로 돌린 인덱스값

                List<reitem> mydate = new List<reitem>(); //새로운 리스트를 만들어서 그곳에 값을 저장하고
                mydate = instance.ItemData[(int)table[tableindex]];// 그것을 넣는다
                GameObject obj = Resources.Load<GameObject>(mydate[0].itemPrefab);
                obj.GetComponent<DropItem>().reitem.Key = mydate[0].Key; //데이터를 넣는다
                obj.GetComponent<DropItem>().reitem.itemtype = mydate[0].itemtype;
                obj.GetComponent<DropItem>().reitem.itemName = mydate[0].itemName;
                obj.GetComponent<DropItem>().reitem.itemGrade = mydate[0].itemGrade;
                obj.GetComponent<DropItem>().reitem.itemCount = mydate[0].itemCount;
                obj.GetComponent<DropItem>().reitem.uSEitem = mydate[0].uSEitem;
                Instantiate(obj, monsterTansform.position, obj.transform.rotation); //생성시킨다
            }
            else if (DropRange < 10)
            {
                ArrayList table = new ArrayList();
                foreach (var keyValue in ItemData)
                {
                    for (int i = 0; i < keyValue.Value.Count; i++)
                    {
                        if (keyValue.Value[i].itemGrade == "C")
                        {
                            table.Add(keyValue.Key);
                        }
                    }
                }
                int tableindex = Random.Range(0, table.Count);
                //생성된 드랍아이템의 아이템 타입을 아이템데이터베이스 형식으로 바꿈
                List<reitem> mydate = new List<reitem>();
                mydate = instance.ItemData[(int)table[tableindex]];
                GameObject obj = Resources.Load<GameObject>(mydate[0].itemPrefab);
                obj.GetComponent<DropItem>().reitem.Key = mydate[0].Key;
                obj.GetComponent<DropItem>().reitem.itemtype = mydate[0].itemtype;
                obj.GetComponent<DropItem>().reitem.itemName = mydate[0].itemName;
                obj.GetComponent<DropItem>().reitem.itemGrade = mydate[0].itemGrade;
                obj.GetComponent<DropItem>().reitem.itemCount = mydate[0].itemCount;
                obj.GetComponent<DropItem>().reitem.uSEitem = mydate[0].uSEitem;
                Instantiate(obj, monsterTansform.position, obj.transform.rotation);
            }
        }
    }
    #region FirstSolution // 처음에 만든 아이템 드랍방식
    /*
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
                for (int i = 0; i < instance.itemList.Count; i++)
                {
                    if (instance.itemList[i].itemGrade == (ITemGrade)3) // 등급비교
                    {
                        table.Add(i); //테이블에 아이템의 인덱스값을 저장
                    }
                }
                int tableindex = Random.Range(0, table.Count); // 테이블의 값을 랜덤으로 돌린 인덱스값
                GameObject obj = Instantiate(instance.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, instance.itemList[(int)table[tableindex]].itemPrefab.transform.rotation); //생성
                obj.GetComponent<DropItem>().item.itemtype = instance.itemList[(int)table[tableindex]].itemtype; //생성된 드랍아이템의 아이템 타입을 아이템데이터베이스 형식으로 바꿈
                obj.GetComponent<DropItem>().item.itemName = instance.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = instance.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = instance.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = instance.itemList[(int)table[tableindex]].use;
            }


            else if (firstrage < 10)
            {
                ArrayList table = new ArrayList();
                for (int i = 0; i < instance.itemList.Count; i++)
                {
                    if (instance.itemList[i].itemGrade == (ITemGrade)2)
                    {
                        table.Add(i);
                    }
                }
                int tableindex = Random.Range(0, table.Count);
                GameObject obj = Instantiate(instance.itemList[(int)table[tableindex]].itemPrefab, monsterTansform.position, instance.itemList[(int)table[tableindex]].itemPrefab.transform.rotation);
                obj.GetComponent<DropItem>().item.itemtype = instance.itemList[(int)table[tableindex]].itemtype;
                obj.GetComponent<DropItem>().item.itemName = instance.itemList[(int)table[tableindex]].itemName;
                obj.GetComponent<DropItem>().item.itemGrade = instance.itemList[(int)table[tableindex]].itemGrade;
                obj.GetComponent<DropItem>().item.itemCount = instance.itemList[(int)table[tableindex]].itemCount;
                obj.GetComponent<DropItem>().item.use = instance.itemList[(int)table[tableindex]].use;
            }
        } 
    }*/
    #endregion
    void LoadItemTextData() //데이터화 시켜서 불러옴
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TextData/Item");

        string temp = textAsset.text.Replace("\r\n", "\n");
        string[] row = temp.Split('\n');


        for (int i = 1; i < row.Length; i++)
        {
            string[] data = row[i].Split(',');

            if (data.Length <= 1) continue;
            List<reitem> myitem = new List<reitem>(); //리스트가 위에서 실행하면 중복으로 실행이 되서 밑으로 빼야함
            myitem.Add(new reitem(int.Parse(data[0]), data[1], data[2], data[3], data[4], int.Parse(data[5]), data[6]));

            ItemData.Add(int.Parse(data[0]), myitem);
        }
    }
}
