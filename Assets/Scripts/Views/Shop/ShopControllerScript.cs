using Assets.Scripts.EventSO;
using Assets.Scripts.Units;
using AYellowpaper.SerializedCollections;
using Constants;
using Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Shop
{
    public class ShopControllerScript : MonoBehaviour
    {
        [SerializeField] private CitySelectedEventSO citySelectedEventSO;

        [SerializeField] private Button infantryBuyButton;
        [SerializeField] private Button artilleryBuyButton;
        [SerializeField] private Button tankBuyButton;

        [SerializeField]
        private CurrencyScriptableObject currency;

        [SerializeField]
        private UnitDBSO unitDB;

        private CityViewPresenter selectedCity;

        private void OnEnable()
        {
            citySelectedEventSO.EventRaised += CitySelectedEventHandler;
        }

        private void Awake()
        {
            infantryBuyButton.onClick.AddListener(() => BuyUnit(UnitType.Infantry));
            artilleryBuyButton.onClick.AddListener(() => BuyUnit(UnitType.Artillery));
            tankBuyButton.onClick.AddListener(() => BuyUnit(UnitType.Tank));
        }

        public void Start()
        {
            //FOR TESTING PURPOSES
            //selectedCity = GameObject.FindGameObjectsWithTag("City").FirstOrDefault(c => c.name == "City").GetComponent<CityViewPresenter>();
        }
        private void CitySelectedEventHandler(CityViewPresenter selectedCity)
        {
            Debug.Log($"Selected City: {selectedCity.name}");
            this.selectedCity = selectedCity;
        }

        public void BuyUnit(UnitType chosenUnit)
        {
            if (currency.CurrencyAmount < unitDB.UnitConfigs[chosenUnit].Price)
            {
                Debug.Log("Not enough money");
                return;
            }

            currency.RemoveAmount(unitDB.UnitConfigs[chosenUnit].Price);

            this.selectedCity.AddUnits(chosenUnit);
        }
    }
}
