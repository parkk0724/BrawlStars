using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITemType
{
    Potion, Equipment //��� // �����
}
public enum USE
{
    HP, STAMINA, FEVER, TSTOP, INVINCIBLE // ��� ������ �̸� ���� ������� �����Ұ�
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
