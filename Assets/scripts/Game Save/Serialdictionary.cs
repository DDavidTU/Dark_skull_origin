using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Serialdictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> tkeys = new List<Tkey>();
    [SerializeField] private List<Tvalue> tvalues = new List<Tvalue>();
    public void OnBeforeSerialize()
    {
        tkeys.Clear();
        tvalues.Clear();
        foreach(KeyValuePair<Tkey,Tvalue> pair in this)
        {
            tkeys.Add(pair.Key);
            tvalues.Add(pair.Value);

        }
    }

    public void OnAfterDeserialize()
    {
        Clear();
        if(tkeys.Count!=tvalues.Count)
        {
            Debug.LogError("Keys 數量 != tvalues 數量");
        }
        for(int i=0; i<tkeys.Count;i++)
        {
            Add(tkeys[i], tvalues[i]);
        }
    }
}

