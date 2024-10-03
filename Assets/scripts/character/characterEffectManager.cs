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

            //��ʧ��ı�S�ĩ�b���⨭�W
            if(BooldSplatterVFX!=null)
            {
                GameObject booldsplatter=Instantiate(BooldSplatterVFX,contactPoint,Quaternion.identity);
            }
            else//�S���w�A�ۤv�qworldManager��
            {
                GameObject booldsplatter = Instantiate(worldCharacterEffectManager.instance.Booldsplatter, contactPoint, Quaternion.identity);
            }
        }
    }
}
