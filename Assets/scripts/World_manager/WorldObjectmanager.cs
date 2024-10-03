using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace sg
{
    public class WorldObjectmanager : MonoBehaviour
    {
        public static WorldObjectmanager instance;

    
        [Header("Objects")]
        [SerializeField] List<NetWorkObjectSpawners> Networkobjectspawners;

        [SerializeField] List<GameObject> spawnObjects;
        [Header("Fog walls")]
        public List<FogwallScritable> Fogwalls;

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

        public void SpawnObject(NetWorkObjectSpawners ObjectSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Networkobjectspawners.Add(ObjectSpawner);
                ObjectSpawner.AttemptToSpawnObject();
            }
        }

        public void addFogWall(FogwallScritable fogwall)
        {
          if(!Fogwalls.Contains(fogwall))
            {
                Fogwalls.Add(fogwall);
            }

        }
        public void removeFogWall(FogwallScritable fogwall)
        {
            if (!Fogwalls.Contains(fogwall))
            {
                Fogwalls.Remove(fogwall);
            }
        }



    }
}
