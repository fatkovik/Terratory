using Assets.Scripts.Owners;
using AYellowpaper.SerializedCollections;
using Constants;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "OwnerDataScriptableObject", menuName = "ScriptableObjects/OwnerDataSO")]
    public class OwnerDataScriptableObject : ScriptableObject
    {
        [SerializedDictionary]
        public SerializedDictionary<OwnerType, OwnerData> OwnerDataDictionary;
    }
}
