using Assets.Scripts.EventSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Constants;
namespace Base.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler; 
        [SerializeField] private CitySelectedEventSO _citySelectedEvent;
        [SerializeField] private CitySetTargetEventSO citySetTargetEvent;

        private CityViewPresenter _selectedAllyCity;

        private void OnEnable()
        {
            _inputHandler.ObjectSelected += ProcessCitySelection;
        }

        private void OnDisable()
        {
            _inputHandler.ObjectSelected -= ProcessCitySelection;
        }

        private void ProcessCitySelection(ISelectable obj)
        {
            var selectedCity = obj as CityViewPresenter;
            if (selectedCity == null) return;
            
            if (selectedCity.Owner == OwnerType.Player)
            {
                _selectedAllyCity = selectedCity;
                _citySelectedEvent.Raise(_selectedAllyCity);
            }
            else if(_selectedAllyCity != null)
            {
                citySetTargetEvent.Raise(new AttackInfo(_selectedAllyCity, selectedCity));
            }
        }
    }
}
