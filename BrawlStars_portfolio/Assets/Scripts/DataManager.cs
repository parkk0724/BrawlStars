using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public struct Character
    {
        public int index;
        public string name;
        public string charPrefab;
    }

    public GameObject startPos;

    [HideInInspector]
    public GameObject select_characterl;
    private void Update()
    {
        LoadTextData();

       GameObject obj =  Instantiate(Resources.Load<GameObject>(Characters[1001].charPrefab), startPos.transform.position, startPos.transform.rotation);
    }

    public Dictionary<int, Character> Characters = new Dictionary<int, Character>();
    private void LoadTextData()
    {
        TextAsset CharacterData = Resources.Load<TextAsset>("TextData/CharacterData");

        string temp = CharacterData.text.Replace("\r\n", "\n");
        string[] row = temp.Split('\n');

        for (int i = 1; i < row.Length; i++)
        {
            string[] data = row[i].Split('\t');

            Character CharData = new Character();

            CharData.index = int.Parse(data[0]);
            CharData.name = data[1];
            CharData.charPrefab = data[2];

            Characters.Add(CharData.index, CharData);
        }
    }

}
