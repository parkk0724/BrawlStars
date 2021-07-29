using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITemType
{
    Potion, Equipment
}
public enum USE
{
    HP, STAMINA, FEVER, TSTOP, INVINCIBLE
}
public enum ITemGrade
{
    A, B, C, D
}

[System.Serializable]
public class Item
{
    public ITemType itemtype;
    public USE use;
    public ITemGrade itemGrade;
    public string itemName;
    public int itemCount;
    public GameObject itemPrefab;

    public Item(ITemType _itemtpye, USE _use, ITemGrade _itemGrade, string _itemName, int _itemCount, GameObject _itemPrefab)
    {
        itemtype = _itemtpye;
        use = _use;
        itemGrade = _itemGrade;
        itemName = _itemName;
        itemCount = _itemCount;
        itemPrefab = _itemPrefab;
    }
    public bool Use()
    {
        return false;
    }

}
