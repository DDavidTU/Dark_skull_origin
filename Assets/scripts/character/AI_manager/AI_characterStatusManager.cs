using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{


    public class AI_characterStatusManager : characterstatusmanager
    {
         AI_characterManager aI_Character;
        protected override void Awake()
        {
            base.Awake();
            aI_Character = GetComponent<AI_characterManager>();
        }

        protected override void Start()
        {
            base.Start();

            caculateHpBaseofHpValevel(aI_Character.aI_CharacterNetworkManager.Hpva.Value);
           
        }
    }
}

