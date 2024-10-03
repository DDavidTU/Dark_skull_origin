using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace sg
{
    public class AicharacterSpawner : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] GameObject charactergameObject;
        [SerializeField] GameObject instantiatedGameObject;

        private void Awake()
        {
           
        }
        private void Start()
        {
            
            WorldAImanager.instance.SpawnCharacter(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnCharacter()
        {
            if (charactergameObject != null)
            {
               
                instantiatedGameObject=Instantiate(charactergameObject);
                instantiatedGameObject.transform.position =transform.position;
                instantiatedGameObject.transform.rotation=transform.rotation;
                instantiatedGameObject.GetComponent<NetworkObject>().Spawn();
            }

        }
    }
}
