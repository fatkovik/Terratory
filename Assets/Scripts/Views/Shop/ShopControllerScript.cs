﻿using Assets.Scripts.EventSO;
using Assets.Scripts.Units;
using AYellowpaper.SerializedCollections;
using Constants;
using Currency;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Shop
{
    public class ShopControllerScript : MonoBehaviour
    {
        [SerializeField] private Button shopButtonPrefab;
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
                var button = Instantiate(shopButtonPrefab);
                button.GetComponent<UnitButtonPrefabScript>().Init(unit.Key);
                button.transform.SetParent(shopButtonsLayout.transform);
                button.onClick.AddListener(() => BuyUnit(unit.Key));
                button.image.sprite = unit.Value.Icon;
            }
        }

        private void CitySelectedEventHandler(CityViewPresenter selectedCity)
        {
            Debug.Log($"Selected City: {selectedCity.name} \n Owner: {selectedCity.Owner}");
            this.selectedCity = selectedCity;
        }

        public void BuyUnit(UnitType chosenUnit)
        {
            if (currency.CurrencyAmount < unitDB.UnitConfigs[chosenUnit].Price || this.selectedCity == null)
            {
                return;
            }

            this.currency.RemoveAmount(unitDB.UnitConfigs[chosenUnit].Price);
            this.selectedCity.AddUnits(chosenUnit);
        }
    }
}
