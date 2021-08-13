using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class reitem
{
    public int Key;
    public string itemtype;
    public string uSEitem;
    public string itemName;
    public int itemCount;
    public string itemGrade;
    public string itemPrefab;

    public reitem(int _key, string _itemtpye, string _uSEitem, string _itemGrade, string _itemName, int _itemCount, string _itemPrefab)
    {
        Key = _key;
        itemtype = _itemtpye;
        uSEitem = _uSEitem;
        itemName = _itemName;
        itemGrade = _itemGrade;
        itemCount = _itemCount;
        itemPrefab = _itemPrefab;
    }
}
