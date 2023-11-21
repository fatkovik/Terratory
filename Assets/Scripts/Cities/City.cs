﻿using Cities;
using Scripts;
using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class City : MonoBehaviour, IDamagable
{
    UnitFactory unitFactory;

    private Dictionary<UnitType, int> units;

    //TODO: implement this
    //private float unitStrenght => this.units.Values.Sum(ul => ul.Sum(u => u.Strenght));

    private float healthPoints;
    private float influenceRadius;
    private float healthRegenPerSecond;

    private float strengthThreshold;
    private float goldPerSecond;

    private OwnerType owner;

    [SerializeField] 
    private Sprite areaSprite;
    private Color areaColor;

    public event Action<OwnerType> OnOutOfHealth;

    public void TakeDamage(OwnerType owner, float amount)
    {
        this.healthPoints -= amount;

        if (this.healthPoints <= 0)
        {
            this.owner = owner;
            OnOutOfHealth?.Invoke(this.owner);
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
        this.units[unitToAdd]++;
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

        //TODO: do the calculations;
    }

    //TODO: implement and Update strenghtThreshold;

    private void Awake()
    {
        this.unitFactory = FindObjectOfType<UnitFactory>();
    }
}