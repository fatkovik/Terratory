using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/UnitSO")]
    public class UnitScriptableObject : ScriptableObject
    {
        public float Price;
        public float VisionRadius;

        public float HealthPoints;
        public float HealthRegenPerSecond;

        public float AttackRange;
        public float Damage;
        public float AttackSpeed;

        public float Speed;

        public Sprite Icon;

        public UnitType Type;
    }
}