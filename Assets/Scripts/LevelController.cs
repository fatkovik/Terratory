using Assets.Scripts.EventSO;
using Assets.Scripts.Owners;
using Assets.Scripts.Player;
using AYellowpaper.SerializedCollections;
using Cities;
using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{ 
    [SerializeField] private CityOwnerChangedEventSO cityOwnerChangedEventSo;
    [SerializeField] private CitySetTargetEventSO citySetTargetEventSO;

    [SerializeField] private List<CityViewPresenter> CityList;

    [SerializeField] private CitySO cityScriptableObject;
    [SerializeField] private OwnerDataScriptableObject ownerDataScriptableObject;

    private void OnEnable()
    {
        cityOwnerChangedEventSo.EventRaised += OnCityOwnerChanged;
        citySetTargetEventSO.EventRaised += OnCityTargetSet;
    }

    private void OnDisable()
    {
        cityOwnerChangedEventSo.EventRaised -= OnCityOwnerChanged;
        citySetTargetEventSO.EventRaised -= OnCityTargetSet;
    }

    private void Start()
    {
        CityList = new List<CityViewPresenter>(GameObject.FindGameObjectsWithTag("City").Length);
        foreach (var city in GameObject.FindGameObjectsWithTag("City"))
        {
            var cityScript = city.GetComponent<CityViewPresenter>();
            cityScript.Init(cityScriptableObject);
            CityList.Add(cityScript);

            SetCityColor(cityScript, ownerDataScriptableObject.OwnerDataDictionary);
        }
    }

    private void OnCityOwnerChanged(CityOwnerChangedEventArgs args)
    {
        Debug.Log("City Captured");
        OwnerChanged(args.CapturedCity, args.NewOwner);
    }

    private void SetCityColor(CityViewPresenter city, SerializedDictionary<OwnerType, OwnerData> OwnerDataDictionary)
    {
        city.SetColor(OwnerDataDictionary[city.Owner]);
    }

    private void OnCityTargetSet(AttackInfo data)
    {
        Debug.Log("Enemy city selected");
        data.PlayerCity.SetTarget(data.EnemyCity);
    }

    private void OwnerChanged(CityViewPresenter city, OwnerType newOwner)
    {
        city.Owner = newOwner;
        SetCityColor(city, ownerDataScriptableObject.OwnerDataDictionary);
    }
}
