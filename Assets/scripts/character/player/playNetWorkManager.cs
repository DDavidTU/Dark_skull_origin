using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.AI;
using JetBrains.Annotations;


namespace sg
{
    public class playNetWorkManager : characterNetworkManager
    {

        PlayerManager player;

        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Equipment")]
        public NetworkVariable<int> currentweaponBeingused = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentRightHandWeaponID= new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentLeftHandWeaponID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isusingRightHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isusingLeftHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("PP Effect")]
        public NetworkVariable<float> MaxReductCurrentHp = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();

        }

        public void SetcharacterActionHand(bool usingRightHand)
        {
            if(usingRightHand)
            {
                isusingLeftHand.Value = false;
                isusingRightHand.Value = true;
            }
            else
            {
                isusingRightHand.Value = false;
                isusingLeftHand.Value = true;
            }
        }
        public void SetnewMaxHpva(int oldHpvalue,int newHpvalue)
        {
           MaxHp.Value= player.playerstatusmanager.caculateHpBaseofHpValevel(newHpvalue);
            playerUIManger.instance.playeruihubmanager.SetmaxHPValue(MaxHp.Value);

           

            currentHp.Value = MaxHp.Value;
        }

       

        public void SetnewMaxStamina(int oldataminavalue, int newstaminavalue)
        {
           Maxstamina.Value= player.playerstatusmanager.caculatestaminaBaseonEndurancelevel(newstaminavalue);
            playerUIManger.instance.playeruihubmanager.SetmaxstaminaValue(Maxstamina.Value);
            currentstamina.Value = Maxstamina.Value;
        }

        public void OncurrentRightHandChangID(int oldID, int newID)
        {
            weaponitem newweapon =Instantiate( worldItemDatabase.Instance.GetWeaponByID(newID));
            player.playerInventoryManager.currentrightweapon = newweapon;
            player.playerEquipmentmanager.LoadRightHandWeapon();

            if (player.IsOwner)
            {
                playerUIManger.instance.playeruihubmanager.SetRightWeaponQuickSlotsIcon(newID);
            }
        }
        public void OncurrentLeftHandChangID(int oldID, int newID)
        {
            weaponitem newweapon =Instantiate( worldItemDatabase.Instance.GetWeaponByID(newID));
            player.playerInventoryManager.currentleftweapon = newweapon;
            player.playerEquipmentmanager.LoadLeftHandWeapon();
            if (player.IsOwner)
            {
                playerUIManger.instance.playeruihubmanager.SetLeftWeaponQuickSlotsIcon(newID);
            }
        }

        public void OncurrentbeingusedIDchange(int oldID, int newID)
        {
            weaponitem newweapon = Instantiate(worldItemDatabase.Instance.GetWeaponByID(newID));
            player.playercombatManager.currentweaponBeingused = newweapon;
           
        }

        [ServerRpc]
        public void NotifyTheServeofWeaponActionServerRpc(ulong clientID,int weaponID,int ActionID)
        {
            if(IsServer)
            {
                NotifyTheServeofWeaponActionClientRpc(clientID, weaponID, ActionID);
            }
        }
        [ClientRpc]
        private void NotifyTheServeofWeaponActionClientRpc(ulong clientID, int weaponID, int ActionID)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                preformWeaponBasedAction(weaponID, ActionID);
            }
        }
        private void preformWeaponBasedAction(int weaponID, int ActionID)
        {
           weaponitemAction weaponitemAction =WorldActionMananger.Instance.GetWeaponitemActionByID(ActionID);
            if (weaponitemAction != null)
            {
                weaponitemAction.AttemptToPerforaction(player, worldItemDatabase.Instance.GetWeaponByID(weaponID));
            }
            else
            {
                Debug.LogError("action is null");
            }

            
        }

        public void SetMaxReductCurrentHP(int oldvalue,int newvalue)
        {
            MaxReductCurrentHp.Value = (MaxHp.Value - newvalue) / (float)MaxHp.Value;
        }

    } 




}
