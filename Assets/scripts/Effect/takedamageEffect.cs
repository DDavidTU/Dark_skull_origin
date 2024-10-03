using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName = "CharacterEffect/Instant Effect/take damage Effect")]
    public class takedamageEffect : InstantEffectManager
    {
        

        [Header("character cause damage")]
        public charactermanger charactercausedamage;

       

        [Header("Damage")]
        public float physicalDamage = 0;
        public float MagicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;
        public float lightningDamage = 0;

        [Header("Final Damage")]
        public int finalDamage = 0;

        [Header("poise")]
        public float poiseDamage = 0;
        public bool poiseisbroken=true;

       [Header("Damage Animation")]
        public bool playanimationDamage = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

       

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementDamageSound;

        [Header("Damage From")]
        public float angleHitFrom; //�T�{���˰ʵe�����
        public Vector3 contactPoint; //�T�{��G�Q�q�ĪG����m


       
        
        public override void ProcessEffect(charactermanger character)
        {

           
            base.ProcessEffect(character);

            if (character.characternetcodeManager.isInvulnerable.Value)
                return;
            //�p�G�w���`�A���ݭn�A�h�[�ˮ`�p��
            if (character.IsDead.Value)
                return;

         
            calculateDamage(character);

            PlayDirectionBasedDamageAnimation(character);
            PlayDamageVFX(character);
            PlayeDamageSFX(character);
            AppearHPBar(character);
            Beattacked(character);
        }
        private void calculateDamage(charactermanger character)
        {
            if (!character.IsOwner)
                return;

            if(charactercausedamage != null)
            {

            }

           

            finalDamage =Mathf.RoundToInt(physicalDamage+MagicDamage+fireDamage+holyDamage+lightningDamage);
            if (finalDamage <= 0)
            {
                finalDamage = 0;
            }
            

           
            poiseDamage = finalDamage;

           

            Debug.Log("final damage" + finalDamage);
            
            character.characternetcodeManager.currentHp.Value-=finalDamage;
        }
        private void PlayDamageVFX(charactermanger character)
        {
            //�i�b�o�̥[���K�B�{�q���ˮ`�S��
            character.charactereffectManager.PlayBooldsplatterVFX(contactPoint);
        }
        private void PlayeDamageSFX(charactermanger character)
        {
            AudioClip PhysicDamageSFX = WorldSoundManager.instance.ReadomChoseSFX(WorldSoundManager.instance.PhysicDamageSound);
            character.characteroundfxsource.playSoundFX(PhysicDamageSFX);
            character.charactersoundFXsource.playDamageGrunt();

        }

        private void PlayDirectionBasedDamageAnimation(charactermanger character)
        {
            if(!character.IsOwner) 
                return;
            if (character.IsDead.Value)
                return;
            playerUIManger.instance.CloseAllUI();
            playerUIManger.instance.isopenUI = false;
          


            



            if (angleHitFrom >= 145 && angleHitFrom<=180)
            {
                //�e
                damageAnimation = character.characterAnimatorforManager.Hit_Forward_Medium_01;
              
            }
            else if(angleHitFrom <=-145 &&  angleHitFrom>=-180)
            {
                //�e
                damageAnimation = character.characterAnimatorforManager.Hit_Forward_Medium_01;
               
            }
            else if (angleHitFrom >= -45 && angleHitFrom <= 45)
            {
                //��
                 damageAnimation = character.characterAnimatorforManager.Hit_Back_Medium_01;
                
            }
            else if(angleHitFrom >= -144 && angleHitFrom <= -45)
            {
                //��
                damageAnimation = character.characterAnimatorforManager.Hit_Left_Medium_01;
               
            }
            else if (angleHitFrom >= 45 && angleHitFrom <= 144)
            {

                //�k
                damageAnimation = character.characterAnimatorforManager.Hit_Right_Medium_01;
              
            }

            if (poiseDamage>= character.characternetcodeManager.Damagepoise.Value)
            {
                character.characterAnimatorforManager.PlayTargetactionAnimation(damageAnimation, true);
            }
        }

        private void  AppearHPBar(charactermanger character)
        {
            character.AI_UI_status_Bar_forAI.openBar = true;
            character.AI_UI_status_Bar_forAI.open_or_close_bar.SetActive(true);
        }
        private void Beattacked(charactermanger character)
        {
            character.charactercombatmanager.be_attacked = true;
        }
    }
}
