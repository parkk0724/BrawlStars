using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace mibnMBT
{
    public class MainController : MonoBehaviour {

        private static MainController _inst;
        public static MainController Inst
        {
            get
            {
                if (_inst == null)
                {
                    Debug.LogError("MainController == null");                    
                }
                return _inst;
            }
        }

        public GameObject[] m_OrcWarrior;
        public GameObject[] m_GoblinWizard;
        public GameObject[] m_OrgeHitter;
        public GameObject[] m_TrolCurer;

        public GameObject[] m_Weapon;

        [HideInInspector]
        public GameObject currentMonster;
        GameObject[] currentMonsterGroup;
        MonsterController currentMonsterController;

        Dictionary<string, GameObject> weaponDictionary;

        private void Awake()
        {
            _inst = this;
        }

        void Start() {

            //Spawn Monster & Weapon GameObject.
            SpawnGameObject(m_OrcWarrior);
            SpawnGameObject(m_GoblinWizard);
            SpawnGameObject(m_OrgeHitter);
            SpawnGameObject(m_TrolCurer);
            SpawnGameObject(m_Weapon);

            //Set Default Character = OrcWarrior01.
            m_OrcWarrior[0].SetActive(true);
            currentMonsterGroup = m_OrcWarrior;
            currentMonster = currentMonsterGroup[0];
            currentMonsterController = currentMonster.GetComponent<MonsterController>();            

            weaponDictionary = new Dictionary<string, GameObject>();            

            int c = 0;
            while ( c < m_Weapon.Length)
            {
                weaponDictionary.Add(m_Weapon[c].name, m_Weapon[c]);
                c++;
            }

            //Set Default Weapon = Axe01.
            SetWeapon("Axe01");
        }

        public void PlayAnimation(string clipName)
        {
            currentMonsterController.PlayAnimation(clipName);
        }


        public void ChangeMonster(string monsterName)
        {
            if (currentMonster.name.Contains(monsterName))
            { 
                return;
            }

            currentMonster.SetActive(false);
            string weaponName = "";

            switch (monsterName)
            {
                case "OrcWarrior":
                    currentMonsterGroup = m_OrcWarrior;
                    weaponName = "Axe01";
                    break;
                case "GoblinWizard":
                    currentMonsterGroup = m_GoblinWizard;
                    weaponName = "Staff01";
                    break;
                case "OrgeHitter":
                    currentMonsterGroup = m_OrgeHitter;
                    weaponName = "Blunt01";
                    break;
                case "TrolCurer":
                    currentMonsterGroup = m_TrolCurer;
                    weaponName = "Dagger01";
                    break;
            }

            currentMonster = currentMonsterGroup[0];
            currentMonster.SetActive(true);
            currentMonsterController = currentMonster.GetComponent<MonsterController>();
            SetWeapon(weaponName);
        }


        public void ChangeColor(int index)
        {
            if (currentMonster.name.Contains(index.ToString("D2")))
            {
                return;
            }

            currentMonster.SetActive(false);
            GameObject weaponObject = currentMonsterController.currentWeapon;

            currentMonster = currentMonsterGroup[index-1];

            currentMonster.SetActive(true);
            currentMonsterController = currentMonster.GetComponent<MonsterController>();
            currentMonsterController.SetWeapon(weaponObject);
        }


        public void SetWeapon(string weaponName)
        {
            currentMonsterController.SetWeapon(weaponDictionary[weaponName]);
        }




        //Spawn GameObject in Hierarchy
        void SpawnGameObject(GameObject[] Go)
        {
            int c = 0;
            int length = Go.Length;
            while (c < length)
            {
                string originalName = Go[c].name;
                Go[c] = Instantiate(Go[c], Vector3.zero, Quaternion.identity) as GameObject;
                Go[c].name = originalName;
                Go[c].SetActive(false);
                c++;
            }
        }
    }

}
