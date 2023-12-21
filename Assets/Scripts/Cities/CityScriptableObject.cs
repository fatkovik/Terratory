using UnityEngine;

namespace Cities
{
    [CreateAssetMenu(fileName = "City", menuName = "ScriptableObjects/CitySO")]
    public class CitySO : ScriptableObject
    {
        public float BaseHealthPoints;
        public float HealthRegenPerSecond;
        public float InfluenceRadius;
        public float StrenghtThreshold;
        public float BaseGoldPerSecond;
    }
}
