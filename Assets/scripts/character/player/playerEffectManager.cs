using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class playerEffectManager : characterEffectManager
    {
        playerstatusmanager playerstatus;

       

        [Header("player HP Effect")]
        public GameObject currentParticleFX;
        public int Healamount;
        public GameObject instantiatedFXModel;
        public playerInventoryManager playerinventoryManager;
        public weaponmodelinitialSlot weaponslot;

      
        protected override void Awake()
        {
            base.Awake();
            playerstatus = GetComponent<playerstatusmanager>();
            playerinventoryManager=GetComponent<playerInventoryManager>();
           
        }
        


        public void HealplayerFromEffect()
        {
            playerstatus.RecoveryHP(Healamount);
            GameObject Healparticle = Instantiate(currentParticleFX,playerstatus.transform);
            Destroy(instantiatedFXModel.gameObject,2.2f);
            Destroy(Healparticle, 2);
            

        }


      

    }
}
