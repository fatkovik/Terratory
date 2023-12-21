using Cities;
using Scripts;
using System;
using System.Collections.Generic;
using Constants;
using static Constants.Constants;
using UnityEngine;
using Assets.Scripts.Cities;
using Base.Input;
using Assets.Scripts.EventSO;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Owners;
using Assets.Scripts.Units;
using System.Net.Mime;

public class CityViewPresenter : MonoBehaviour, IDamagable, ISelectable
{
    public event Action<UnitType, int> UnitAmountChanged;

    [SerializeField] private CityOwnerChangedEventSO cityOwnerChangedEvent;

    [SerializeField] private CityView cityView;
    [SerializeField] private SpriteRenderer cityPinRenderer;
    [SerializeField] private SpriteRenderer regionRenderer;
    [SerializeField] private UnitDBSO unitDB;
    [SerializeField] private OwnerType owner;
    [SerializeField] private bool IsMainPlayerCity = false;

    public bool IsCapital => this.IsMainPlayerCity && this.owner == OwnerType.Player;

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

    public OwnerType Owner 
    {
        get => this.owner;
        set
        {
            this.owner = value;

            if (this.owner != OwnerType.Player)
            {
                this.cityView.ShowCountOverlay(false);
            }
            this.cityView.ShowCountOverlay(true);
        }
    }

    private UnitFactory unitFactory;
    private Dictionary<UnitType, int> units;

    private float influenceRadius;

    private float maxHealthPoints;
    private float healthRegenPerSecond; 

    public UnitType UnitTypeToSpawn = UnitType.Infantry;

    public bool HasHealth => this.HealthPoints > 0;

    private float strengthThreshold;
    private float strength;
    public float Strength
    {
        get => strength;
        private set 
        {
            this.strength = value;
            this.cityView.SetStrengthOverlay(strength);

            if (strength < strengthThreshold)
            {
                Rebel();
            }
        }
    }

    private float baseGoldPerSecond;
    private float goldPerSecond;
    public float GoldPerSecond
    {
        get => goldPerSecond;
        private set { this.goldPerSecond = value; }
    }

    private void OnEnable()
    {
        UnitAmountChanged += OnUnitAmountChanged;
    }

    private void OnDisable()
    {
        UnitAmountChanged -= OnUnitAmountChanged;
    }

    private void OnUnitAmountChanged(UnitType type, int arg2)
    {
        this.cityView.SetUnitOverlay(type, arg2);
        this.Strength = CalculateStrength();
    }

    private float CalculateStrength()
    {
        float strength = 0;
        foreach (var unitKey in units.Keys)
        {
            strength += units[unitKey] * unitDB.UnitConfigs[unitKey].Damage;
        }
        return strength;
    }

    public void TakeDamage(OwnerType owner, float amount)
    {
        this.HealthPoints -= amount;
        if (this.HealthPoints <= 0)
        {
            cityOwnerChangedEvent.Raise(new CityOwnerChangedEventArgs { CapturedCity = this, NewOwner = owner });
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
        this.baseGoldPerSecond = config.BaseGoldPerSecond;
        this.strengthThreshold = config.StrenghtThreshold; // multiply by distance and influence shit

        this.units = new Dictionary<UnitType, int>();
        this.RegenHealth();

        this.cityView.ShowCountOverlay(this.Owner == OwnerType.Player);
        //TODO: do the calculations

        this.GoldPerSecond = this.baseGoldPerSecond;
    }

    public void CalculateAndSetGoldPerSecond(CityViewPresenter playerCapital)
    {
        var thisCityCoordinates = this.transform.position;
        var originalPlayerCityCoordinated = playerCapital.transform.position;

        var distance = Vector3.Distance(thisCityCoordinates, originalPlayerCityCoordinated);

        var normalisedDistance = Mathf.Clamp01(distance);
        this.GoldPerSecond = baseGoldPerSecond * (normalisedDistance * GoldPerSecondDistanceModifier);
        Debug.Log(this.GoldPerSecond);
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
    
    private void Rebel()
    {
        Debug.LogError("Rebel logic is not implemented");
    }

    private void Awake()
    {
        this.unitFactory = FindObjectOfType<UnitFactory>();
    }

    public void Select()
    {
    }
}