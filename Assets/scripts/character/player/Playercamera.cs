using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace sg
{
    public class Playercamera : MonoBehaviour
    {
        // Start is called before the first frame update
        public static Playercamera instance;
        public PlayerManager player;
        public Camera cameraObjection;
       

        [SerializeField] public Transform cameraUPandDOWN;

        [Header("Camera Setting")]
        
        private float cameraSpeed=1;
        [SerializeField] float leftandrightlookspeed=150;
        [SerializeField] float upanddownrotationspeed=150;
        [SerializeField] float minimumPivot = -30;
        [SerializeField] float maximumPivot = 60;
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] public LayerMask colliderwithLayer;


        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraobjectposition;
        [SerializeField] float leftandrightlookangle;
        [SerializeField] float upanddownlookangle;
        [SerializeField] float cameraZPosition;
        [SerializeField] float targetCameraZposition;

        [Header("Lock On")]
        [SerializeField] float maxiummimumLockOndistance = 20;
        [SerializeField] float minimumViewableAngle = -50;
        [SerializeField] float maximumimumViewableAngle = 50;
        [SerializeField] float lockFromSpeed = 0.2f;
        [SerializeField] float setcameraHeightSpeed = 0.05f;
        [SerializeField] float UnLockOnCameraHeight = 1.6f;
        [SerializeField] float LockOnCameraHeight = 2.2f;

        private Coroutine cameraLockonHeightcoroutine;

        private List<charactermanger> avaliableTargets = new List<charactermanger>();
        public charactermanger nearestTarget;
        public charactermanger Right_target;
        public charactermanger Left_target;

       
       

        private void Awake()


        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
           cameraZPosition= cameraObjection.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
           

            if(player!=null)
            {
                CameraFollowTarget();

                if (player.IsDead.Value)
                {
                    nearestTarget = null;
                    Right_target = null;
                    Left_target = null;
                    clearlockONtarget();
                    player.playerNetcodeManger.isLocking.Value = false;
                    return;
                }
               
                Handlerotation();
                Handlecollider();


            }
        }

        private void CameraFollowTarget()
        {
            Vector3 targetcamera = Vector3.SmoothDamp(transform.position,player.transform.position,ref cameraVelocity,cameraSpeed*Time.deltaTime);
            transform.position = targetcamera;
        }

        private void Handlerotation()
        {
            if(player.playerNetcodeManger.isLocking.Value)
            {
                Vector3  rotationDirection=player.playercombatManager.currenttarget.charactercombatmanager.lockonTransform.position-transform.position;
                rotationDirection.Normalize();
                rotationDirection.y = 0;

                Quaternion targetRotation= Quaternion.LookRotation(rotationDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,lockFromSpeed);

                rotationDirection = player.playercombatManager.currenttarget.charactercombatmanager.lockonTransform.position - cameraUPandDOWN.position;
                rotationDirection.Normalize();

                targetRotation= Quaternion.LookRotation(rotationDirection);
                cameraUPandDOWN.rotation = Quaternion.Slerp(cameraUPandDOWN.rotation, targetRotation, lockFromSpeed);

                leftandrightlookangle = transform.eulerAngles.y;
                upanddownlookangle=transform.eulerAngles.x;


            }
            else
            {
                leftandrightlookangle += (Playerinputmnager.instance.camerahorizontal_Input * leftandrightlookspeed) * Time.deltaTime;
                upanddownlookangle -= (Playerinputmnager.instance.cameravertical_Input * upanddownrotationspeed) * Time.deltaTime;
                upanddownlookangle = Mathf.Clamp(upanddownlookangle, minimumPivot, maximumPivot);

                Vector3 cameraRotation = Vector3.zero;
                Quaternion targetRotation;

                cameraRotation.y = leftandrightlookangle;
                targetRotation = Quaternion.Euler(cameraRotation);
                transform.rotation = targetRotation;

                cameraRotation = Vector3.zero;
                cameraRotation.x = upanddownlookangle;
                targetRotation = Quaternion.Euler(cameraRotation);
                cameraUPandDOWN.localRotation = targetRotation;
            }


           
        }

        private void Handlecollider()
        {
            targetCameraZposition = cameraZPosition;
            RaycastHit hit;
            Vector3 direction =cameraObjection.transform.position-cameraUPandDOWN.position;
            direction.Normalize();
            if (Physics.SphereCast(cameraUPandDOWN.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZposition), colliderwithLayer))
            {
                float distancefromhitobject = Vector3.Distance(cameraUPandDOWN.position, hit.point);
                targetCameraZposition = -(distancefromhitobject - cameraCollisionRadius);
            }

            if(Mathf.Abs(targetCameraZposition)<cameraCollisionRadius) 
            {
                targetCameraZposition = -cameraCollisionRadius;            
            }
            cameraobjectposition.z = Mathf.Lerp(cameraObjection.transform.localPosition.z, targetCameraZposition, 0.2f);
            cameraObjection.transform.localPosition = cameraobjectposition;
        }
        
        public void HandleLocateLockonTargets()

        {
            float shortestdistance= Mathf.Infinity; //距離我方的最短距離
            float shortestDistandforRightTarget=Mathf.Infinity;
            float shortestDistandorLeftTarget=-Mathf.Infinity;
            

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxiummimumLockOndistance, WorldUtillityManager.Instance.GetCharacterLayerMask());

            for(int i = 0; i < colliders.Length; i++)
            {
                charactermanger Locktarget= colliders[i].GetComponent<charactermanger>();
                if(Locktarget != null)
                {
                    Vector3 LocktargetDirection=Locktarget.transform.position- player.transform.position;
                    float viewableAngle = Vector3.Angle(LocktargetDirection, cameraObjection.transform.forward);

                    if (Locktarget.IsDead.Value)
                        continue;
                    if (player.transform.root == Locktarget.transform.root)
                        continue;
                  
                    if(viewableAngle>minimumViewableAngle && viewableAngle< maximumimumViewableAngle )
                    {
                        RaycastHit hit;

                        if(Physics.Linecast(player.charactercombatmanager.lockonTransform.position,Locktarget.charactercombatmanager.lockonTransform.position,out hit,WorldUtillityManager.Instance.GetEnviroLayerMask()))
                        {
                            //我們鎖定的對象，被擋住了，不能鎖定
                            continue;

                        }
                        else
                        {
                            avaliableTargets.Add(Locktarget);
                        }
                    }

                    
                }
            }

            //潛在目標中，來看出誰是最近的
            for(int a = 0;a< avaliableTargets.Count;a++)
            {
                if (avaliableTargets[a] != null)
                {
                    Vector3 LocktargetDirection = avaliableTargets[a].transform.position - player.transform.position;
                    float distanceFromTarget = Vector3.Distance(player.transform.position, avaliableTargets[a].transform.position);
                    if(distanceFromTarget < shortestdistance)
                    {
                        shortestdistance= distanceFromTarget;
                        nearestTarget = avaliableTargets[a];
                    }
                    if(player.playerNetcodeManger.isLocking.Value)
                    {
                        Vector3 relativeEnemyposition = player.transform.InverseTransformPoint(avaliableTargets[a].transform.position);
                        var distanceFromRightTarget = relativeEnemyposition.x;
                        var distanceFromLeftTarget = relativeEnemyposition.x;

                        if (avaliableTargets[a] == player.playercombatManager.currenttarget)
                            continue;
                     
                        if (relativeEnemyposition.x >= 0.00 && distanceFromRightTarget <  shortestDistandforRightTarget)
                        {
                            shortestDistandforRightTarget= distanceFromRightTarget;
                            Right_target = avaliableTargets[a];


                        }
                        else if(relativeEnemyposition.x <= 0.00 && distanceFromLeftTarget > shortestDistandorLeftTarget)
                        {
                            shortestDistandorLeftTarget = distanceFromRightTarget;
                            Left_target = avaliableTargets[a];
                        }
                    }
                }
                else
                {
                    clearlockONtarget();
                    player.playerNetcodeManger.isLocking.Value = false;

                }
            }
        }

        public void SetlockcameraHeight()
        {
            
            if(cameraLockonHeightcoroutine != null)
                StopCoroutine(cameraLockonHeightcoroutine);

            cameraLockonHeightcoroutine = StartCoroutine(SetcameraLockHeight());
        }
        public void clearlockONtarget()
        {
            nearestTarget = null;
            Right_target = null;
            Left_target = null;
            avaliableTargets.Clear();
        }

        public IEnumerator  WaitThenFindnewTarget()
        {
            while(player.isPerformingAction)
            {
                yield return null;
            }
            clearlockONtarget ();
            HandleLocateLockonTargets();
            if(nearestTarget != null)
            {
                player.playercombatManager.setTarget(nearestTarget);
                player.playerNetcodeManger.isLocking.Value=true;
            }
            yield return null;
        }

        public IEnumerator SetcameraLockHeight()
        {
            
            float duration = 2;
            float time = 0;
            Vector3 velocity=Vector3.zero;
            Vector3 newLockedCameraHeight = new Vector3(cameraUPandDOWN.localPosition.x,LockOnCameraHeight) ;
            Vector3 newUnLockedCameraHeight = new Vector3(cameraUPandDOWN.localPosition.x,UnLockOnCameraHeight);

            while(time<duration)
            {
                time += Time.deltaTime;

                if (player != null)
                {
                    if (player.playerNetcodeManger.isLocking.Value)
                    {
                        cameraUPandDOWN.localPosition = Vector3.SmoothDamp(cameraUPandDOWN.localPosition, newLockedCameraHeight, ref velocity, setcameraHeightSpeed);
                        cameraUPandDOWN.localRotation = Quaternion.Slerp(cameraUPandDOWN.localRotation, Quaternion.Euler(0, 0, 0), lockFromSpeed);
                       
                    }
                    else
                    {
                        cameraUPandDOWN.localPosition = Vector3.SmoothDamp(cameraUPandDOWN.localPosition
                             , newUnLockedCameraHeight, ref velocity, setcameraHeightSpeed);

                    }
                }
                yield return null;
            }
           if(player!=null)
            {
                if(player.playercombatManager.currenttarget!=null )
                {
                    cameraUPandDOWN.localPosition = newLockedCameraHeight;
                    cameraUPandDOWN.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    cameraUPandDOWN.localPosition = newUnLockedCameraHeight;
                }
                
            }
            yield return null;

        }


    }
}
