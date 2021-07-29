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
    A,B,C,D
}

[System.Serializable]
public class Item
{
    public ITemType itemtype;
    public USE use;
    public ITemGrade itemGrade;
    public string itemName;
    public int itemCount;

    public bool Use()
    {
        return false;
    }

}
