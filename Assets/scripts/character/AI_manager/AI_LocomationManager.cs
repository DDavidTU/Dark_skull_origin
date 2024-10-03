using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class AI_LocomationManager : characterlocomotionmanger
    {
        public void Ai_RotateTowardsAgent(AI_characterManager ai_characterManager)
        {
            ai_characterManager.transform.rotation = ai_characterManager.navMeshAgent.transform.rotation;
        }
    }
}
