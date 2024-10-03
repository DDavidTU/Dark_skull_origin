using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class charactersoundFXsource : MonoBehaviour
    {
         private AudioSource audioSource;
        [Header("Damage Grunts")]
        [SerializeField] public AudioClip[] damageGrunts;
        [Header("Zombie attack sound")]
        [SerializeField]public AudioClip[] attackGrunts;
        [SerializeField] public AudioClip BiteGrunt;
        [SerializeField] public AudioClip ShootGrunt;

        protected virtual void Awake( )
        { 
            audioSource = GetComponent<AudioSource>();
        }


       
        public void playSoundFX(AudioClip soundFX,float volume=1,bool readomizepitch =true,float pitchRandom =0.1f)
        {
            if (soundFX != null)
            {

                audioSource.PlayOneShot(soundFX, volume);

                audioSource.pitch = 1;

                if (readomizepitch)
                {
                    audioSource.pitch += Random.Range(-pitchRandom, +pitchRandom);
                }
            }
        }
        public void PlayrollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundManager.instance.Rollsound);
        }

        public virtual void playDamageGrunt()
        {
            playSoundFX(WorldSoundManager.instance.ReadomChoseSFX(damageGrunts));
        }
        public virtual void playZombieAttackSound()
        {
            playSoundFX(WorldSoundManager.instance.ReadomChoseSFX(attackGrunts));
        }
        public virtual void playZombieBiteSound()
        {
            playSoundFX(BiteGrunt);
        }
        public virtual void playZombieShootSound()
        {
            playSoundFX(ShootGrunt);
        }

    }
}
