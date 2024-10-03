using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{
    public class WorldSoundManager : MonoBehaviour
    {
       public static WorldSoundManager instance;
        [Header("DamageSound")]
        public AudioClip[] PhysicDamageSound;
        [Header("Actionsound")]
       public AudioClip  Rollsound;

        
        
        private void Awake()
        {
            if (instance == null ) 
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
        }
       
        public AudioClip ReadomChoseSFX(AudioClip[] audioClips)
        {
            int index = Random.Range(0, audioClips.Length);
            return audioClips[index];
        }
    }
}
