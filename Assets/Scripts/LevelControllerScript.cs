using Assets.Scripts.EventSO;
using Assets.Scripts.Player;
using Cities;
using Constants;
using Scripts;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{ 
    [SerializeField] private CityCapturedEventSO cityCapturedEventSO;
    [SerializeField] private CitySetTargetEventSO citySetTargetEventSO;

    [SerializeField] private List<CityViewPresenter> CityList;

    [SerializeField] private CityScriptableObject cityScriptableObject;
    [SerializeField] private OwnerDataScriptableObject ownerDataScriptableObject;

    private void OnEnable()
    {
        cityCapturedEventSO.EventRaised += OnCityCaptured;
        citySetTargetEventSO.EventRaised += OnCityTargetSet;
    }

    private void Start()
    {
        CityList = new List<CityViewPresenter>(GameObject.FindGameObjectsWithTag("City").Length);
        foreach (var city in GameObject.FindGameObjectsWithTag("City"))
        {
            var cityScript = city.GetComponent<CityViewPresenter>();
            cityScript.Init(cityScriptableObject);
            CityList.Add(cityScript);

            SetCityColor(cityScript, ownerDataScriptableObject.OwnerDataDictionary[cityScript.Owner].Color);
        }
    }

    private void OnCityCaptured(CityCapturedEventArgs args)
    {
        Debug.Log("City Captured");
        SetCityColor(args.CapturedCity, ownerDataScriptableObject.OwnerDataDictionary[args.CapturedCity.Owner].Color);
    }

    private void SetCityColor(CityViewPresenter city, Color color)
    {
        city.SetColor(color);
    }


    private void OnCityTargetSet(AttackInfo data)
    {
        throw new NotImplementedException();
    }
}
