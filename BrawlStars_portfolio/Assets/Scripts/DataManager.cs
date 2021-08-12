using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager instance;
    public struct Character
    {
        public int index;
        public string name;
        public string charPrefab;
    }

    [HideInInspector]
    public Character select_character;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadTextData();
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

            if (data.Length <= 1) continue;

            Character CharData = new Character();

            int value = 0;
            int.TryParse(data[0], out value);
            CharData.index = value;
            CharData.name = data[1];
            CharData.charPrefab = data[2];

            Characters.Add(CharData.index, CharData);
        }
    }

}
