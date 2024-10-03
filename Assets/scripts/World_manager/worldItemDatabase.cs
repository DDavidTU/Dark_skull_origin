using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

namespace sg
{
    public class worldItemDatabase : MonoBehaviour
    {
        public static worldItemDatabase Instance;

        public weaponitem unarmedWeapon;

        [Header("Weapon")]
        [SerializeField]List<weaponitem> weapons = new List<weaponitem>();

        [Header("item")]
        private List<item> items=new List<item>();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }
            for(int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }
        }

        public weaponitem GetWeaponByID(int ID)
        {
          return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }
        public AudioClip  ChoseRandonSound(AudioClip[] audioClips)
        {
           

                int index = Random.Range(0, audioClips.Length);
                return audioClips[index];
            
            
        }
    }
}
