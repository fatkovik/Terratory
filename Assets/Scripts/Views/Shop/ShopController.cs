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
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private UnitTypeButton shopButtonPrefab;
        [SerializeField] private CitySelectedEventSO citySelectedEventSO;
        [SerializeField] private HorizontalLayoutGroup shopButtonsLayout;
        [SerializeField] private CurrencyScriptableObject currency;
        [SerializeField] private UnitDBSO unitDB;

        private CityViewPresenter selectedCity;

        private void OnEnable()
        {
            citySelectedEventSO.EventRaised += CitySelectedEventHandler;

            foreach (var unit in unitDB.UnitConfigs)
            {
                UnitTypeButton button = Instantiate(shopButtonPrefab);
                button.Init(unit.Key, unit.Value.Icon, () => BuyUnit(unit.Key));
                button.transform.SetParent(shopButtonsLayout.transform);
            }
        }

        private void CitySelectedEventHandler(CityViewPresenter selectedCity)
        {
            Debug.Log($"Selected City: {selectedCity.name} \n Owner: {selectedCity.Owner}");
            this.selectedCity = selectedCity;
        }

        public void BuyUnit(UnitType chosenUnit)
        {
            float price = unitDB.UnitConfigs[chosenUnit].Price;
            if (currency.CurrencyAmount < price || this.selectedCity == null)
            {
                return;
            }

            this.currency.RemoveAmount(price);
            this.selectedCity.AddUnits(chosenUnit);
        }
    }
}
