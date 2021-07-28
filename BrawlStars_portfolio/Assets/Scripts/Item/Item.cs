using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITemType
{
    Potion, Equipment //즉발 // 비즉발
}
public enum USE
{
    HP, STAMINA, FEVER, TSTOP, INVINCIBLE // 즉발 아이템 이름 대충 만들었음 수정할것
}
[System.Serializable]
public class Item
{
    public ITemType itemtype;
    public USE use;
    public string itemName;
    public int itemCount;

    public bool Use()
    {
        return false;
    }

}
