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
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class CityViewPresenter : MonoBehaviour, IDamagable, ISelectable
{
    //events
    //public event Action<UnitType, int> UnitAmountChanged;

    [SerializeField] private CityCapturedEventSO cityCapturedEvent;

    [SerializeField] private CityView cityView;
    [SerializeField] private Sprite areaSprite;
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

    //TODO: implement this
    //private float unitStrenght => this.units.Values.Sum(ul => ul.Sum(u => u.Strenght));

    private float influenceRadius;

    private float maxHealthPoints;
    private float healthRegenPerSecond;

    private float strengthThreshold;
    private float goldPerSecond;

    private Color areaColor;

    public bool HasHealth => this.HealthPoints > 0;

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

    public void SetColor(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
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

        this.cityView.SetUnitOverlay(unitToAdd, this.units[unitToAdd]);

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
        newUnit.owner = OwnerType.Player;
        newUnit.Init();
    }

    public void Init(CityScriptableObject config)
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