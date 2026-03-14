using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SECatalog", menuName = "ScriptableObjects/SECatalog")]
public class SECatalog : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public string ID;
        public string Name;
        public AudioClip[] Clip;
    }

    public List<Entry> KeyboardSounds;
}