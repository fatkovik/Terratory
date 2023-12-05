using Assets.Scripts.EventSO;
using Assets.Scripts.Units;
using Constants;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionPanelScript : MonoBehaviour
{
    [SerializeField] private UnitTypeButton unitButtonPrefab;
    [SerializeField] private UnitToSpawnSelectedEventSO unitToSpawnSelected;
    [SerializeField] private CitySelectedEventSO citySelectedEventSO;
    [SerializeField] private UnitDBSO unitDB;
    [SerializeField] private VerticalLayoutGroup unitSelectionLayoutGroup;

    private CityViewPresenter selectedCity;

    private void OnEnable()
    {
        citySelectedEventSO.EventRaised += CitySelectedEventHandler;

        foreach (var unit in unitDB.UnitConfigs)
        {
            var button = Instantiate(unitButtonPrefab);
            button.GetComponent<UnitTypeButton>().Init(unit.Key, unit.Value.Icon, () => SetUnitToSpawn(unit.Key));
            button.transform.SetParent(unitSelectionLayoutGroup.transform);
        }
    }

    private void SetUnitToSpawn(UnitType unit)
    {
        if (selectedCity == null) return;
        selectedCity.UnitTypeToSpawn = unit;
    }

    private void CitySelectedEventHandler(CityViewPresenter selectedCity)
    {
        this.selectedCity = selectedCity;
    }
}
