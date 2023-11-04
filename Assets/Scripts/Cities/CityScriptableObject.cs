using UnityEngine;

namespace Cities
{
    [CreateAssetMenu(fileName = "City", menuName = "ScriptableObjects/CitySO")]
    public class CityScriptableObject : ScriptableObject
    {
        public float BaseHealthPoints;
        public float HealthRegenPerSecond;
        public float InfluenceRadius;
    }
}
