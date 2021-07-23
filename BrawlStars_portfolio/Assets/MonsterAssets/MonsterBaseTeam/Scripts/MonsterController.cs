using UnityEngine;
using System.Collections;

namespace mibnMBT
{
    public class MonsterController : MonoBehaviour
    {
        public Transform m_WeaponDummy;
        public Transform m_PelvisBone;
        public GameObject m_Shadow;
        public float shadowHeight = 0.01f;        
        public float shadowSize = 1.0f;

        float initHeight;
        float currentHeight;

        Animator m_Animator;        

       // [HideInInspector]
        public GameObject currentWeapon;
        
        void Start()
        {
            m_Animator = this.GetComponent<Animator>();

            if (m_PelvisBone != null) { initHeight = m_PelvisBone.position.y; }
        }

        private void Update()
        {
            //Shadow Control.
            if (m_PelvisBone != null && m_Shadow != null)
            {
                Vector3 newShadowPosition = m_PelvisBone.position;
                m_Shadow.transform.position = new Vector3(newShadowPosition.x, shadowHeight, newShadowPosition.z);

                currentHeight = newShadowPosition.y;
                float shadowRate = (initHeight / currentHeight);

                Vector3 newShadowSize = new Vector3(shadowSize + (shadowRate * 0.2f), shadowSize + (shadowRate * 0.2f), shadowSize + (shadowRate * 0.2f));
                m_Shadow.transform.localScale = newShadowSize;                
            }
        }

        public void PlayAnimation(string clipName)
        {
            m_Animator.SetTrigger(clipName);
        }

        public void SetWeapon(GameObject weapon)
        {
            if (currentWeapon != null)
            {
                currentWeapon.transform.parent = null;
                currentWeapon.transform.position = Vector3.zero;
                currentWeapon.transform.localRotation = Quaternion.identity;
                currentWeapon.SetActive(false);
            }

            weapon.transform.parent = m_WeaponDummy;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            currentWeapon = weapon;
            currentWeapon.SetActive(true);
        }
    }
}
