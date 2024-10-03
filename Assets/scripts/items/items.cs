using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{

    public class item : ScriptableObject
    {
        [Header("items information")]
        public string itemsname;
        public Sprite itemsicon;
        [TextArea] public string itemdesript;
        public int itemID;

    }
}
