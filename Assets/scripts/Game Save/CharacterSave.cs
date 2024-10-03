using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    [System.Serializable]
    public class CharacterSave 
    {
        [Header("Scene index")]
        public int sceneindex=1;
        [Header("character Name")]
        public string character_name= "character";
        [Header("Time Play")]
        public float playtime;

        [Header("World coordinates")]
        public float position_x;
        public float position_y;
        public float position_z;
        [Header("Resourse")]
        public int currentHp;
        public float currentstamina;
        [Header("Status")]
        public int Hp;
        public int stamina;
        [Header("playerinventory")]
        public List<weaponitem> weaponsInventory;

        public weaponitem[] weaponinRightSlots = new weaponitem[3];
        public weaponitem[] weaponinLefttSlots = new weaponitem[3];
        public weaponitem currentrightweapon;
        public weaponitem currentleftweapon;

        public int HPRecoveryamount;
        [Header("Bosses")]
        public Serialdictionary<int, bool> bossesAwakened;//boss³Q³ê¿ô
        public Serialdictionary<int, bool> bossesDefeat;//boss³QÀ»±Ñ
        
        public void CharacterSaveData()
        {
            bossesAwakened = new Serialdictionary<int, bool>();
            bossesDefeat = new Serialdictionary<int, bool>();
        }
    }
}
