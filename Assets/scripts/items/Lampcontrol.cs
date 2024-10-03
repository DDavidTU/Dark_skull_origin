using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace sg
{
    public class Lampcontrol : MonoBehaviour
    {
        public static Lampcontrol instance;
        [SerializeField] public GameObject Lightting;
        [SerializeField] public GameObject Lamp;

        [SerializeField] public Transform transformdown;
        [SerializeField] public Transform transformup;

        private Material material;
        private void Awake()
        {
            if (instance == null)
            {

                instance = this;
            }
            else Destroy(gameObject);

            material = Lamp.GetComponent<Renderer>().material;

        }


        public void moveup()
        {
            Lamp.transform.parent = transformup;
            Lamp.transform.localPosition = Vector3.zero;
            Lamp.transform.localRotation = Quaternion.identity;
            Lamp.transform.localScale = Vector3.one;
        }
        public void movedown()
        {
            Lamp.transform.parent = transformdown;
            Lamp.transform.localPosition = Vector3.zero;
            Lamp.transform.localRotation = Quaternion.identity;
            Lamp.transform.localScale = Vector3.one;
        }
        public void Lightcontrol()
        {
            if (!Lightting.activeSelf)
            {
                material.EnableKeyword("_EMISSION");
                Lightting.SetActive(true);
            }
            else
            {
                material.DisableKeyword("_EMISSION");
                Lightting.SetActive(false);
            }
        }
    }
}
