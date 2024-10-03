using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace sg
{
    
    public class FogwallScritable : NetworkBehaviour
    {
        [Header("Fog")]
        [SerializeField] GameObject[] FogGameObject;
        [Header("ID")]
        [SerializeField] public int FogWallID;
        [Header("is Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

       
        
           

        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            isActive.OnValueChanged += OnIsActiveFogWallChanged;
            WorldObjectmanager.instance.addFogWall(this);

        }
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            OnIsActiveFogWallChanged(false, isActive.Value);
            isActive.OnValueChanged -= OnIsActiveFogWallChanged;
            WorldObjectmanager.instance.removeFogWall(this);

        }
        private void OnIsActiveFogWallChanged(bool old,bool newstatus )
        {
            if(isActive.Value)
            {
                foreach(var foggameobject in FogGameObject)
                {
                    foggameobject.SetActive(true);
                }

            }
            else
            {
                foreach (var foggameobject in FogGameObject)
                {
                    foggameobject.SetActive(false);
                }
            }
        }
    }
}
