using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class worldCharacterEffectManager : MonoBehaviour
    {
        public static worldCharacterEffectManager instance;

        [Header("VFX")]
         public GameObject Booldsplatter;
        [Header("Damage")]
        public takedamageEffect takedamageeffect;
    

        [SerializeField] List<InstantEffectManager> instantEffect;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            GenerateEffectID();

        }

        private void GenerateEffectID()
        {
            for (int i = 0; i < instantEffect.Count; ++i)
            {
                instantEffect[i].IDeffect = i;
            }
        }

    }
}
