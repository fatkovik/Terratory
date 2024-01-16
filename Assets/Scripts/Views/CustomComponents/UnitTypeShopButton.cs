using Assets.Scripts.Units;
using Constants;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Button))]
public class UnitTypeShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text priceText;
    private UnityAction onButtonClick;
    
    public UnitType UnitType { get; private set; }
    bool hasListener;

    public void Init(UnitType unitType, Sprite sprite, float price, UnityAction onClick)
    {
        this.UnitType = unitType;
        this.button.image.sprite = sprite;
        this.onButtonClick = onClick;
        this.priceText.SetText(price.ToString());

        this.button.onClick.AddListener(onButtonClick);
    }

    private void OnEnable()
    {
        if (onButtonClick == null) return;
        this.button.onClick.AddListener(onButtonClick);
    }

    private void OnDisable()
    {
        if(onButtonClick == null) return;
        this.button.onClick.RemoveListener(onButtonClick);
    }
}
