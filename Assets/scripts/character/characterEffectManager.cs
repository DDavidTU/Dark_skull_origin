using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class characterEffectManager : MonoBehaviour
    {

        charactermanger character;

        [Header("VFX")]
        [SerializeField]GameObject BooldSplatterVFX;
        protected virtual void Awake()
        {
           character= GetComponent<charactermanger>();
        }
        public virtual void ProcessInstantEffect(InstantEffectManager effect)
        {
            effect.ProcessEffect(character);
        }

        public void PlayBooldsplatterVFX(Vector3 contactPoint)
        {

            //手動把視覺特效放在角色身上
            if(BooldSplatterVFX!=null)
            {
                GameObject booldsplatter=Instantiate(BooldSplatterVFX,contactPoint,Quaternion.identity);
            }
            else//沒指定，自己從worldManager抓
            {
                GameObject booldsplatter = Instantiate(worldCharacterEffectManager.instance.Booldsplatter, contactPoint, Quaternion.identity);
            }
        }
    }
}
