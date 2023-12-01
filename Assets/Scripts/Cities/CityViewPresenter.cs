using Cities;
using Scripts;
using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using Assets.Scripts.Cities;
using Base.Input;
using Assets.Scripts.EventSO;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Owners;

public class CityViewPresenter : MonoBehaviour, IDamagable, ISelectable
{
    //events
    public event Action<UnitType, int> UnitAmountChanged;

    [SerializeField] private CityCapturedEventSO cityCapturedEvent;

    [SerializeField] private CityView cityView;
    [SerializeField] private SpriteRenderer cityPinRenderer;
    [SerializeField] private SpriteRenderer regionRenderer;
    [SerializeField] private OwnerType owner;

    private float healthPoints;
    public float HealthPoints
    {
        get => healthPoints;
        private set
        {
            this.healthPoints = value;
            this.cityView.SetHealthOverlay((int)this.healthPoints);
        }
    }

    public OwnerType Owner => this.owner;

    UnitFactory unitFactory;

    private Dictionary<UnitType, int> units;

    private float influenceRadius;

    private float maxHealthPoints;
    private float healthRegenPerSecond;

    private float strengthThreshold;
    private float goldPerSecond;

    public UnitType UnitTypeToSpawn = UnitType.Infantry;

    public bool HasHealth => this.HealthPoints > 0;

    //TODO: implement this
    //private float unitStrenght => this.units.Values.Sum(ul => ul.Sum(u => u.Strenght));

    private void OnEnable()
    {
        UnitAmountChanged += this.cityView.SetUnitOverlay;
    }

    private void OnDisable()
    {
        UnitAmountChanged -= this.cityView.SetUnitOverlay;
    }

    public void TakeDamage(OwnerType owner, float amount)
    {
        this.HealthPoints -= amount;
        Debug.Log("city HIT!!");
        if (this.HealthPoints <= 0)
        {
            var oldOwner = this.owner;
            this.owner = owner;
            cityCapturedEvent?.Raise(new CityCapturedEventArgs { CapturedCity = this, OldOwner = oldOwner });
            Debug.Log("City Destroyed");
        }
    }

    public void SetColor(OwnerData data)
    {
        this.GetComponent<SpriteRenderer>().color = data.CityColor;
        this.regionRenderer.color = data.RegionColor;
    }

    public void AddUnits(UnitType unitToAdd)
    {
        if (!this.units.ContainsKey(unitToAdd))
        {
            this.units.Add(unitToAdd, 0);
        }

        this.units[unitToAdd]++;

        UnitAmountChanged?.Invoke(unitToAdd, this.units[unitToAdd]);
    }

    public void CreateUnitObject(UnitType unitType, CityViewPresenter target)
    {
        if (this.units[unitType] < 1)
        {
            Debug.Log("No Units Left");
            return;
        }

        this.units[unitType]--;
        UnitAmountChanged?.Invoke(unitType, this.units[unitType]);

        var newUnit = this.unitFactory.CreateUnit(unitType, this.owner, this.transform.position);
        newUnit.transform.position += Helpers.RandomVector(-0.5f, 0.5f);
        newUnit.Init(target);
    }

    public void SetTarget(CityViewPresenter target)
    {
        CreateUnitObject(this.UnitTypeToSpawn, target);
    }

    public void Init(CitySO config)
    {
        this.HealthPoints = config.BaseHealthPoints;
        this.influenceRadius = config.InfluenceRadius;
        this.healthRegenPerSecond = config.HealthRegenPerSecond;

        this.units = new Dictionary<UnitType, int>();
        this.RegenHealth();
        //TODO: do the calculations;
    }

    private void RegenHealth()
    {
        StartCoroutine(HealCoroutine());

        IEnumerator HealCoroutine()
        {
            if (this.HealthPoints < this.maxHealthPoints - this.healthRegenPerSecond)
            {
                this.HealthPoints += this.healthRegenPerSecond * Time.deltaTime;
            }
            yield return new WaitForSeconds(1);
        }
    }

    //TODO: implement and Update strenghtThreshold;

    private void Awake()
    {
        this.unitFactory = FindObjectOfType<UnitFactory>();
    }

    public void Select()
    {
        Debug.Log("City selected");
    }
}