using Currency;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewPresenter : MonoBehaviour
{
    [SerializeField] private MainView mainView;

    [SerializeField] private CurrencyScriptableObject currencyScriptableObject;

    private void OnEnable()
    {
        currencyScriptableObject.CurrencyAmountChanged += CurrencyAmountChangedHandler;   
        currencyScriptableObject.CurrencyPerSecondAmountChanged += CurrencyPerSecondChangedHandler;   
    }

    private void OnDisable()
    {
        currencyScriptableObject.CurrencyAmountChanged -= CurrencyAmountChangedHandler;
        currencyScriptableObject.CurrencyPerSecondAmountChanged -= CurrencyPerSecondChangedHandler;
    }

    private void CurrencyAmountChangedHandler(float amount)
    {
        mainView.Initilize(amount.ToString());
    }

    private void CurrencyPerSecondChangedHandler(float amount)
    {
        mainView.SetCurrencyPerSecond($"+{amount}");
    }

    //For Testing Purposes
    private void Start()
    {
       // CurrencyAmountChangedHandler(currencyScriptableObject.CurrencyAmount);
    }
}
