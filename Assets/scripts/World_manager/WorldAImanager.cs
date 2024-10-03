using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace sg
{
    public class WorldAImanager : MonoBehaviour
    {
    


        public static WorldAImanager instance;
        [Header("Character")]
        [SerializeField]  List<AicharacterSpawner> aicharacterSpawners;
      
        [SerializeField] List<GameObject> spawnCharacters;


        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

      
      

       
        public void SpawnCharacter(AicharacterSpawner aicharacterSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                aicharacterSpawners.Add(aicharacterSpawner);
                aicharacterSpawner.AttemptToSpawnCharacter();
            }
        }

      

    }
}
