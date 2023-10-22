using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Unit", menuName = "UnitSO", order = 1)]
    public class UnitScriptableObject : ScriptableObject
    {
        public int Price;

        public float HealthPoints;
        public float HealthRegenPerSecond;

        public float AttackRange;
        public float DamagePerSecond;

        public float Speed;

        public Sprite Icon;

        public UnitType Type;
    }
}