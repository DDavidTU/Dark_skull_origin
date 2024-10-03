using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace sg
{
    public class NetWorkObjectSpawners : MonoBehaviour
    {
        [Header("Network object")]
        [SerializeField] GameObject NetWorkgameObject;
        [SerializeField] GameObject instantiatedGameObject;

        private void Awake()
        {

        }
        private void Start()
        {

            WorldObjectmanager.instance.SpawnObject(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnObject()
        {
            if (NetWorkgameObject != null)
            {

                instantiatedGameObject = Instantiate(NetWorkgameObject);
                instantiatedGameObject.transform.position = transform.position;
                instantiatedGameObject.transform.rotation = transform.rotation;
                instantiatedGameObject.GetComponent<NetworkObject>().Spawn();
            }

        }
    }
}
