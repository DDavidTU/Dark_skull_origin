using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace sg
{
    public class WorldUtillityManager : MonoBehaviour
    {
        public static WorldUtillityManager Instance;

        [Header("Layers")]
        [SerializeField] LayerMask CharacterLayer;
        [SerializeField] LayerMask enviroLayer;
        [SerializeField] LayerMask itemLayer;


        private void Awake()
        {
            if(Instance==null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public LayerMask GetCharacterLayerMask()
        {
            return CharacterLayer;
        }
        public LayerMask GetEnviroLayerMask()
        {
            return enviroLayer;
        }
        public LayerMask GetitemLayerMask()
        {
            return itemLayer;
        }
        public bool is_canAttackTeamGrounp(characterGroup attackingcharactertype, characterGroup Targetcharctertype)
        {
            if(attackingcharactertype==characterGroup.Team1)
            {
                switch (Targetcharctertype)
                {
                    case characterGroup.Team1:return false;                                              
                    case characterGroup.Team2: return true;                                              
                    default:
                        break;
                }
            }
            else if(attackingcharactertype == characterGroup.Team2)
            {
                switch (Targetcharctertype)
                {
                    case characterGroup.Team1: return true;                                             
                    case characterGroup.Team2:return false;                                             
                    default:
                        break;
                }
            }
            return false;
        }

        public float calculateviewAngle(Transform Ai_transform,Vector3 TargetDirection)
        {
            TargetDirection.y = 0;

            float TargetAngle = Vector3.Angle(Ai_transform.forward,TargetDirection) ;
            Vector3 cross = Vector3.Cross(Ai_transform.forward, TargetDirection);//算出他們的叉積，可以確認到y軸(可用左手定則推出) 
            if (cross.y < 0) TargetAngle = -TargetAngle;//看第三軸向下，可推測目標在左側，負號即可得左側方位

            return TargetAngle;


        }
    }
}
