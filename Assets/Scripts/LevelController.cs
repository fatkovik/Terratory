using Assets.Scripts.EventSO;
using Assets.Scripts.Owners;
using Assets.Scripts.Player;
using AYellowpaper.SerializedCollections;
using Cities;
using Constants;
using Scripts;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LevelController : MonoBehaviour
{ 
    [SerializeField] private CityCapturedEventSO cityCapturedEventSO;
    [SerializeField] private CitySetTargetEventSO citySetTargetEventSO;

    [SerializeField] private List<CityViewPresenter> CityList;

    [SerializeField] private CitySO cityScriptableObject;
    [SerializeField] private OwnerDataScriptableObject ownerDataScriptableObject;

    private void OnEnable()
    {
        cityCapturedEventSO.EventRaised += OnCityCaptured;
        citySetTargetEventSO.EventRaised += OnCityTargetSet;
    }

    private void OnDisable()
    {
        cityCapturedEventSO.EventRaised -= OnCityCaptured;
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

    private void OnCityCaptured(CityCapturedEventArgs args)
    {
        Debug.Log("City Captured");
        SetCityColor(args.CapturedCity, ownerDataScriptableObject.OwnerDataDictionary);
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
}
