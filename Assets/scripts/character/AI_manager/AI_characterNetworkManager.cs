using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace sg
{
    public class AI_characterNetworkManager : characterNetworkManager
    {
        AI_characterManager aI_Character;


        protected override void Awake()
        {
            base.Awake();
            aI_Character = GetComponent<AI_characterManager>();
            if(!aI_Character.enabled )
            {
                aI_Character.enabled=true;
            }
          

        }
      
      
       
    }
}
