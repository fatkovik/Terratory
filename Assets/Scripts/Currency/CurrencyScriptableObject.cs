using System;
using UnityEngine;

namespace Currency
{
    [CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/CurrencySO")]
    public class CurrencyScriptableObject : ScriptableObject
    {
        [field: SerializeField] public string CurrencyName { get; private set; }

        [field: SerializeField] public float CurrencyAmount { get; private set; }

        public event Action<float> CurrencyAmountChanged;

        public void AddAmount(float amount)
        {
            this.CurrencyAmount += amount;
            this.CurrencyAmountChanged?.Invoke(this.CurrencyAmount);
        }

        public void RemoveAmount(float amount) 
        {
            this.CurrencyAmount -= amount;
            this.CurrencyAmountChanged?.Invoke(this.CurrencyAmount);
        }
    }
}
