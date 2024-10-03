using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class charactermanger : NetworkBehaviour
    {

        [Header("status")]
        public NetworkVariable<bool> IsDead =new NetworkVariable<bool>(false,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
        [SerializeField] public Transform BeTarget;

        [HideInInspector] public CharacterController Characactercontroller;
       [HideInInspector] public Animator animator;

       [HideInInspector] public characterNetworkManager characternetcodeManager;
       [HideInInspector] public characterEffectManager charactereffectManager;

        [HideInInspector]public CharacterAnimatorforManager characterAnimatorforManager;
        [HideInInspector]public charactercombatmanager charactercombatmanager;
        [HideInInspector]public charactersoundFXsource characteroundfxsource;
        [HideInInspector]public characterlocomotionmanger characterlocomotionmanger;
        [HideInInspector] public charactersoundFXsource charactersoundFXsource;

        [HideInInspector] public AI_HUB_manager AI_UI_status_Bar_forAI;

        [Header("Group")]
        public characterGroup charactergroup;

        [Header("flags")]
        public bool applyRootMotion = false;
        public bool isPerformingAction=false;
        public bool canMove = true;
        public bool canRotation = false;
        public bool isjumping = false;
        public bool isGround = true;
        
      
       
        

        

        protected virtual void Start()
        {
            ignoreownbodycollider();
        }
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            Characactercontroller = GetComponent<CharacterController>();
            characternetcodeManager = GetComponent<characterNetworkManager>();
            animator = GetComponent<Animator>();
            charactereffectManager=GetComponent<characterEffectManager>();
            
            characterAnimatorforManager = GetComponent<CharacterAnimatorforManager>();
            charactercombatmanager = GetComponent<charactercombatmanager>();
            characteroundfxsource = GetComponent<charactersoundFXsource>();
            characterlocomotionmanger =GetComponent<characterlocomotionmanger>();
            charactersoundFXsource=GetComponent<charactersoundFXsource>();
            
            AI_UI_status_Bar_forAI = GetComponentInChildren<AI_HUB_manager>();
           
        }

        protected virtual void Update()
        {

            animator.SetBool("isGround", isGround);
            if (IsOwner)
            {
                characternetcodeManager.networkposition.Value=transform.position;
                characternetcodeManager.networkrotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    characternetcodeManager.networkposition.Value, ref characternetcodeManager.networkpositionVelocity, characternetcodeManager.networksmoothTime);

                transform.rotation = Quaternion.Slerp(transform.rotation,characternetcodeManager.networkrotation.Value,characternetcodeManager.networkrotationTime);
            }
        }

        protected virtual void FixedUpdate()
        {

        }
        protected virtual void LateUpdate()
        { 
        
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            //�o�̦ASet�@��IsMoving,�]������AI�覡���ܡA�Ȥ�ݱ�������AAI���ʰʵe�C
            animator.SetBool("IsMoving", characternetcodeManager.ismoving.Value);

            //�קK�Ȥ�ݱ�������
            characternetcodeManager.isActivingChanged(false, characternetcodeManager.isActive.Value);



            characternetcodeManager.ismoving.OnValueChanged += characternetcodeManager.ismovingChanged;

            characternetcodeManager.isActive.OnValueChanged += characternetcodeManager.isActivingChanged;
        }
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            
            characternetcodeManager.ismoving.OnValueChanged -= characternetcodeManager.ismovingChanged;
            characternetcodeManager.isActive.OnValueChanged -= characternetcodeManager.isActivingChanged;
        }
        
           
        
        public virtual IEnumerator ProcessDeathEvent(bool manualSelectDeadAnimator=false)
        {
            if(IsOwner)
            {
                characternetcodeManager.currentHp.Value = 0;
                 IsDead.Value = true;

                if (!manualSelectDeadAnimator)
                {
                    playerUIManger.instance.CloseAllUI();
                    characterAnimatorforManager.PlayTargetactionAnimation("dead_1",true);
                   playerUIManger.instance.playeruihubmanager.gameObject.SetActive(false);
                    ControlPP.instance.vg.color.value = Color.black;
                }
            }

            yield return new WaitForSeconds(5);

        }

       public virtual void revivecharacter()
        {

        }

        protected virtual void ignoreownbodycollider()
        {
            Collider characterControllerCollider =GetComponent<Collider>();
            Collider[] damageableCharacterColliders=GetComponentsInChildren<Collider>();

            List<Collider> igonorecolliders = new List<Collider>();

            //���o���鳡�쪺�I�����A�H�K���᤬�۩����������I��
            foreach(var collider in damageableCharacterColliders)
            {
                igonorecolliders.Add(collider);
            }

            foreach(var collider in igonorecolliders)
            {
                foreach(var igonore in igonorecolliders)
                {
                    Physics.IgnoreCollision(collider, igonore, true);
                }
            }

            //���饻��������I�����A�H�K���᤬�۩���
            igonorecolliders.Add(characterControllerCollider);
        }
    }
}
