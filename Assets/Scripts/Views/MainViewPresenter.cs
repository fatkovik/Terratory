using Currency;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewPresenter : MonoBehaviour
{
    [SerializeField]
    private MainView mainView;

    [SerializeField]
    private CurrencyScriptableObject currencyScriptableObject;

    private void OnEnable()
    {
        currencyScriptableObject.CurrencyAmountChanged += CurrencyAmountChangedHandler;   
    }

    private void CurrencyAmountChangedHandler(float amount)
    {
        mainView.Initilize(amount.ToString());
    }

    //FOR TESTING PURPOESSES ONLY
    private void Start()
    {
        CurrencyAmountChangedHandler(currencyScriptableObject.CurrencyAmount);
    }
}
