using AYellowpaper.SerializedCollections;
using Constants;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitDB", menuName = "ScriptableObjects/UnitDB")]
    public class UnitDBSO : ScriptableObject
    {
        [SerializedDictionary("Unit Type", "Config")]
        public SerializedDictionary<UnitType, UnitScriptableObject> UnitConfigs;
    }
}
