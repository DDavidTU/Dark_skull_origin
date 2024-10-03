using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using Unity.Netcode;


namespace sg
{
    public class AI_characterManager : charactermanger
    {
        [HideInInspector] public AI_characterCombatManager aI_CharacterCombatManager;
        [HideInInspector]public AI_characterNetworkManager aI_CharacterNetworkManager;
        [HideInInspector]public AI_LocomationManager aI_LocomationManager;
        [HideInInspector] public CharacterController aI_CharacterController;
        [HideInInspector] public AI_characterAnimatorForManager aI_CharacterAnimatorForManager;
        [HideInInspector] public AI_characterStatusManager aI_CharacterStatusManager;
        


        [Header("Navmesh")]
        [SerializeField] public NavMeshAgent navMeshAgent;


        [Header("curret_status")]
        [SerializeField] public AI_status CurrentStatus;

        [Header("AI_status")]
        [SerializeField] public AI_idle_status idle;
        [SerializeField] public AI_FollowTarget_status followTarget;
        [SerializeField]public AI_combat_status combatstance;
        [SerializeField]public AI_attack_status attack;

       
        protected override void Awake()
        {
            base.Awake();
            aI_CharacterCombatManager=GetComponent<AI_characterCombatManager>();          
            aI_CharacterNetworkManager=GetComponent<AI_characterNetworkManager>();
            aI_LocomationManager = GetComponent<AI_LocomationManager>();
            aI_CharacterController=GetComponent<CharacterController>();
            aI_CharacterAnimatorForManager=GetComponent<AI_characterAnimatorForManager>();
            aI_CharacterStatusManager=GetComponent<AI_characterStatusManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();


            //使用可編輯的物件，用複製較不會改變原始設定
            idle = Instantiate(idle);
            followTarget = Instantiate(followTarget);
            combatstance=Instantiate(combatstance);
            attack=Instantiate(attack);

            CurrentStatus = idle;


                                   
            
           
        }
        protected override void Start()
        {
            base.Start();
            worldsavegamemanger.instance.Aicharacter.Add(this.gameObject);
           
        }

        protected override void Update()
        {
            base.Update();
            aI_CharacterCombatManager.HandleActionRecovery(this);

          
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if(IsOwner)
            processStateMachine();
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

          

            if (IsOwner)
            {
                

               
                //aI_CharacterNetworkManager.currentHp.OnValueChanged += AI_HUB_manager.instance.SetnewHPValue;





            }
            //狀態
            aI_CharacterNetworkManager.currentHp.OnValueChanged += aI_CharacterNetworkManager.CheckHp;

        


            //如果是角色擁有者，但不是伺服器主機，客戶端將重新將遊戲角色載入進去。
           
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

        

            if (IsOwner)
            {


              


                //aI_CharacterNetworkManager.currentHp.OnValueChanged -= AI_HUB_manager.instance.SetnewHPValue;


            }

            aI_CharacterNetworkManager.currentHp.OnValueChanged -= aI_CharacterNetworkManager.CheckHp;


        }
        private void processStateMachine()
        {
            //? 的意思是說，current是null，如果是，那傳回nextStatue.Ticl(this)
            AI_status nextStatue = CurrentStatus?.Tick(this);
            if(nextStatue != null)
            {
                CurrentStatus = nextStatue;
            }
            //每次執行ageny，都定位到角色身上，避免agent亂跑
            navMeshAgent.transform.localPosition = Vector3.zero;
            
            navMeshAgent.transform.localRotation = Quaternion.identity;

            //若目標，在角色側邊後身後，製作可轉過去的動畫前的，計算
            if(aI_CharacterCombatManager.currenttarget!=null)
            {
                aI_CharacterCombatManager.TargetDirection =  aI_CharacterCombatManager.currenttarget.transform.position- transform.position ; 
                aI_CharacterCombatManager.viewableAngle = WorldUtillityManager.Instance.calculateviewAngle(transform,aI_CharacterCombatManager.TargetDirection);

                aI_CharacterCombatManager.TargerDistance = Vector3.Distance(aI_CharacterCombatManager.currenttarget.transform.position, transform.position);//看前後有無差異

            }

            if(navMeshAgent.enabled)
            {
                Vector3 AgentDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(AgentDestination, transform.position);
                if(remainingDistance>navMeshAgent.stoppingDistance)
                {
                    aI_CharacterNetworkManager.ismoving.Value= true;
                }
                else
                {
                    aI_CharacterNetworkManager.ismoving.Value = false;
                }

            }
            else
            {
                aI_CharacterNetworkManager.ismoving.Value = false;
            }
        }

        public override IEnumerator ProcessDeathEvent(bool manualSelectDeadAnimator = false)
        {
            if (IsOwner)
            {
                characternetcodeManager.currentHp.Value = 0;
                IsDead.Value = true;

                if (!manualSelectDeadAnimator)
                {
                   
                    characterAnimatorforManager.PlayTargetactionAnimation("dead_1", true);

                    Destroy(gameObject, 2);
                }
            }

            yield return new WaitForSeconds(5);

        }

    }
}
