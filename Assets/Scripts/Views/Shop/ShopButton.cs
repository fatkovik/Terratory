using Assets.Scripts.Units;
using Constants;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private UnityAction onButtonClick;
    private bool wasInitialized;
    public UnitType UnitType { get; private set; }
    public ButtonClickedEvent OnClick => this.button.onClick;
    public Image Image => this.button.image;
    public void Init(UnitType unitType, Sprite sprite, UnityAction onClick)
    {
        this.UnitType = unitType;
        this.button.image.sprite = sprite;
        this.onButtonClick = onClick;
    }

    private void OnEnable()
    {
        if (!wasInitialized) return;
        this.button.onClick.AddListener(onButtonClick);
    }

    private void OnDisable()
    {
        this.button.onClick.RemoveListener(onButtonClick);
    }
}
