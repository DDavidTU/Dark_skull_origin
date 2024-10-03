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


            //�ϥΥi�s�誺����A�νƻs�����|���ܭ�l�]�w
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
            //���A
            aI_CharacterNetworkManager.currentHp.OnValueChanged += aI_CharacterNetworkManager.CheckHp;

        


            //�p�G�O����֦��̡A�����O���A���D���A�Ȥ�ݱN���s�N�C��������J�i�h�C
           
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
            //? ���N��O���Acurrent�Onull�A�p�G�O�A���Ǧ^nextStatue.Ticl(this)
            AI_status nextStatue = CurrentStatus?.Tick(this);
            if(nextStatue != null)
            {
                CurrentStatus = nextStatue;
            }
            //�C������ageny�A���w��쨤�⨭�W�A�קKagent�ö]
            navMeshAgent.transform.localPosition = Vector3.zero;
            
            navMeshAgent.transform.localRotation = Quaternion.identity;

            //�Y�ؼСA�b���ⰼ��ᨭ��A�s�@�i��L�h���ʵe�e���A�p��
            if(aI_CharacterCombatManager.currenttarget!=null)
            {
                aI_CharacterCombatManager.TargetDirection =  aI_CharacterCombatManager.currenttarget.transform.position- transform.position ; 
                aI_CharacterCombatManager.viewableAngle = WorldUtillityManager.Instance.calculateviewAngle(transform,aI_CharacterCombatManager.TargetDirection);

                aI_CharacterCombatManager.TargerDistance = Vector3.Distance(aI_CharacterCombatManager.currenttarget.transform.position, transform.position);//�ݫe�ᦳ�L�t��

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
