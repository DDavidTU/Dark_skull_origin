using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{
    public class AI_status : ScriptableObject
    {
        public virtual AI_status Tick(AI_characterManager aicharacter)
        {
          
            return this;

        }

        protected virtual AI_status SwitchState(AI_characterManager aicharacter,AI_status Newaistatus)
        {
            ResetstatusFlags(aicharacter);
            return Newaistatus;

        }

        protected virtual void ResetstatusFlags(AI_characterManager aicharacter)
        {

        }
    }
}
