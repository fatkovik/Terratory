using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currencyText;

    public void Initilize(string currencyAmount)
    {
        currencyText.SetText(currencyAmount);
    }
}
