using Cities;
using Scripts;
using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Assets.Scripts.Cities;
using Unity.Collections.LowLevel.Unsafe;
using Base.Input;
using Assets.Scripts.EventSO;

public class CityViewPresenter : MonoBehaviour, IDamagable, ISelectable
{
    //events
    [SerializeField]
    private CitySelectedEventSO citySelectedEventSO;

    //public event Action<UnitType, int> UnitAmountChanged;

    [SerializeField]
    private CityView cityView;

    [SerializeField]
    private OwnerType owner;
    public OwnerType Owner => this.owner;

    UnitFactory unitFactory;

    private Dictionary<UnitType, int> units;

    //TODO: implement this
    //private float unitStrenght => this.units.Values.Sum(ul => ul.Sum(u => u.Strenght));

    private float healthPoints;
    private float influenceRadius;
    private float healthRegenPerSecond;

    private float strengthThreshold;
    private float goldPerSecond;

    [SerializeField] 
    private Sprite areaSprite;
    private Color areaColor;

    public bool HasHealth => this.healthPoints > 0;

    public event Action<OwnerType> OnOutOfHealth;

    public void TakeDamage(OwnerType owner, float amount)
    {
        this.healthPoints -= amount;
        Debug.Log("city HIT!!");
        if (this.healthPoints <= 0)
        {
            this.owner = owner;
            OnOutOfHealth?.Invoke(this.owner);
            Debug.Log("City Destroyed");
        }
    }

    public void ChangeColor()
    {
        //TODO: implement
    }

    public void CitySelected()
    {
    }

    public void AddUnits(UnitType unitToAdd)
    {
        if (!this.units.ContainsKey(unitToAdd))
        {
            this.units.Add(unitToAdd, 0);
        }

        this.units[unitToAdd]++;

        this.cityView.Initilize(unitToAdd, this.units[unitToAdd]);

        CreateUnitObject(unitToAdd);
    }

    public void CreateUnitObject(UnitType unitType)
    {
        if (this.units[unitType] < 1)
        {
            Debug.Log("No Units Left");
            return;
        }

        this.units[unitType]--;
        var newUnit = this.unitFactory.CreateUnit(unitType, this.transform.position);
        newUnit.owner = OwnerType.Ally;
        newUnit.Init();
    }

    public void Init(CityScriptableObject config)
    {
        this.healthPoints = config.BaseHealthPoints;
        this.influenceRadius = config.InfluenceRadius;
        this.healthRegenPerSecond = config.HealthRegenPerSecond;

        this.units = new Dictionary<UnitType, int>();
        //TODO: do the calculations;
    }

    //TODO: implement and Update strenghtThreshold;

    private void Awake()
    {
        this.unitFactory = FindObjectOfType<UnitFactory>();
    }

    public void Select()
    {
        this.citySelectedEventSO.Raise(this);
    }
}