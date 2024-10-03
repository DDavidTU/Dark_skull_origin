using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{
    public class RockFall : MonoBehaviour
    {
        [SerializeField] public float Fall_yposition;
        [SerializeField] public float Fall_Speed;
        [SerializeField] public bool Fall=false;
        Vector3 target;

        private void Awake()
        {
            target = new Vector3(this.gameObject.transform.position.x, Fall_yposition, this.gameObject.transform.position.z);
        }
        private void Update()
        {
           if(Fall)
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, target,Time.deltaTime* Fall_Speed);
        }
    }
}
