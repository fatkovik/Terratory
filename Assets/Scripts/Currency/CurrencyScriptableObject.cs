﻿using System;
using UnityEngine;

namespace Currency
{
    [CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/CurrencySO")]
    public class CurrencyScriptableObject : ScriptableObject
    {
        public string CurrencyName { get; private set; }

        public float CurrencyAmount { get; private set; }

        public event Action<float> CurrencyAmountChanged;

        public void Add(float amount)
        {
            this.CurrencyAmount += amount;
            this.CurrencyAmountChanged?.Invoke(this.CurrencyAmount);
        }

        public void Remove(float amount) 
        {
            this.CurrencyAmount -= amount;
            this.CurrencyAmountChanged?.Invoke(this.CurrencyAmount);
        }
    }
}
