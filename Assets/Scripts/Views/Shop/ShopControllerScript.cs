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

namespace Assets.Scripts.Views.Shop
{
    public class ShopControllerScript : MonoBehaviour
    {
        [SerializeField]
        private CurrencyScriptableObject currency;

        [SerializeField]
        private UnitDBSO unitDB;

        //TODO: create shop that fires events when the unit is selected;
        //and add the unit NUMBER to the  city

        private City selectedCity;
        private void CitySelectedEventHandler(City selectedCity)
        {
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
