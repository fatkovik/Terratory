using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text currencyPerSecondText;

    private void OnEnable()
    {
        currencyText.SetText(Constants.Constants.BaseCurrencyText);
        currencyPerSecondText.SetText(Constants.Constants.BaseCurrencyText);
    }

    public void Initilize(string currencyAmount)
    {
        currencyText.SetText(currencyAmount);
    }

    public void SetCurrencyPerSecond(string perSecondAmount)
    {
        currencyPerSecondText.SetText(perSecondAmount);
    }
}
