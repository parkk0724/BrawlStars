using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace mibnMBT
{
    public class UIRoot : MonoBehaviour
    {

        //Action Button..
        public Button button_Idle;
        public Button button_Run;
        public Button button_Attack01;
        public Button button_Attack02;
        public Button button_Damage;
        public Button button_Dead;

        //Weapon Button..
        public Button button_Axe01;
        public Button button_Axe02;
        public Button button_Sword01;
        public Button button_Hammer01;
        public Button button_Staff01;
        public Button button_Staff02;
        public Button button_Staff03;
        public Button button_Staff04;
        public Button button_Blunt01;
        public Button button_Blunt02;
        public Button button_Blunt03;
        public Button button_Blunt04;
        public Button button_Dagger01;
        public Button button_Dagger02;
        public Button button_Dagger03;
        public Button button_Dagger04;

        //Monster Button..
        public Button Button_OrcWarrior;
        public Button Button_GoblinWizard;
        public Button Button_OrgeHitter;
        public Button Button_TrolCurer;

        //Color Button..
        public Button Button_Color01;
        public Button Button_Color02;
        public Button Button_Color03;
        public Button Button_Color04;

        // Use this for initialization
        void Start()
        {
            //Action Button Listener..
            button_Idle.onClick.AddListener(() => PlayAnimation(button_Idle));
            button_Run.onClick.AddListener(() => PlayAnimation(button_Run));
            button_Attack01.onClick.AddListener(() => PlayAnimation(button_Attack01));
            button_Attack02.onClick.AddListener(() => PlayAnimation(button_Attack02));
            button_Damage.onClick.AddListener(() => PlayAnimation(button_Damage));
            button_Dead.onClick.AddListener(() => PlayAnimation(button_Dead));

            //Weapon Button Listener..
            button_Axe01.onClick.AddListener(() => SetWeapon(button_Axe01));
            button_Axe02.onClick.AddListener(() => SetWeapon(button_Axe02));
            button_Sword01.onClick.AddListener(() => SetWeapon(button_Sword01));
            button_Hammer01.onClick.AddListener(() => SetWeapon(button_Hammer01));
            button_Staff01.onClick.AddListener(() => SetWeapon(button_Staff01));
            button_Staff02.onClick.AddListener(() => SetWeapon(button_Staff02));
            button_Staff03.onClick.AddListener(() => SetWeapon(button_Staff03));
            button_Staff04.onClick.AddListener(() => SetWeapon(button_Staff04));
            button_Blunt01.onClick.AddListener(() => SetWeapon(button_Blunt01));
            button_Blunt02.onClick.AddListener(() => SetWeapon(button_Blunt02));
            button_Blunt03.onClick.AddListener(() => SetWeapon(button_Blunt03));
            button_Blunt04.onClick.AddListener(() => SetWeapon(button_Blunt04));
            button_Dagger01.onClick.AddListener(() => SetWeapon(button_Dagger01));
            button_Dagger02.onClick.AddListener(() => SetWeapon(button_Dagger02));
            button_Dagger03.onClick.AddListener(() => SetWeapon(button_Dagger03));
            button_Dagger04.onClick.AddListener(() => SetWeapon(button_Dagger04));

            //Monster Button Listener..
            Button_OrcWarrior.onClick.AddListener(() => ChangeMonster(Button_OrcWarrior));
            Button_GoblinWizard.onClick.AddListener(() => ChangeMonster(Button_GoblinWizard));
            Button_OrgeHitter.onClick.AddListener(() => ChangeMonster(Button_OrgeHitter));
            Button_TrolCurer.onClick.AddListener(() => ChangeMonster(Button_TrolCurer));

            //Color Button Listener..
            Button_Color01.onClick.AddListener(() => ChangeColor(Button_Color01));
            Button_Color02.onClick.AddListener(() => ChangeColor(Button_Color02));
            Button_Color03.onClick.AddListener(() => ChangeColor(Button_Color03));
            Button_Color04.onClick.AddListener(() => ChangeColor(Button_Color04));


        }

        void PlayAnimation(Button target)
        {
            string name = target.name.Split('_')[1];
            MainController.Inst.PlayAnimation(name);
        }

        void SetWeapon(Button target)
        {
            string name = target.name.Split('_')[1];
            MainController.Inst.SetWeapon(name);
        }

        void ChangeMonster(Button target)
        {
            string name = target.name.Split('_')[1];
            MainController.Inst.ChangeMonster(name);
        }

        void ChangeColor(Button target)
        {
            int index = int.Parse(target.name.Split('_')[1].Substring(5));
            MainController.Inst.ChangeColor(index);
        }
    }
}
