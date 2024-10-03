using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class InstantEffectManager : ScriptableObject
    {

        [Header("Effect ID")]
        public int IDeffect;

        public virtual void ProcessEffect(charactermanger character)
        {


        }
    }
}
