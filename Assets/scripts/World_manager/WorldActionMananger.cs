using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace sg
{
    public class WorldActionMananger : MonoBehaviour
    {
        public static WorldActionMananger Instance;

        [Header("Weapon item Action")]
        public weaponitemAction[] weaponitemAction;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            for(int i = 0; i < weaponitemAction.Length; i++)
            {
                weaponitemAction[i].actionID = i;
            }
        }
        public weaponitemAction GetWeaponitemActionByID(int id)
        {
            return weaponitemAction.FirstOrDefault(action=>action.actionID==id);
        }

    }
}
