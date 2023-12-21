using System.Collections;
using Assets.Scripts.EventSO;
using Assets.Scripts.Owners;
using Assets.Scripts.Player;
using AYellowpaper.SerializedCollections;
using Cities;
using Constants;
using Currency;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{ 
    [SerializeField] private CityOwnerChangedEventSO cityOwnerChangedEventSo;

    [SerializeField] private CityCapturedEventSO cityCapturedEventSO;
    [SerializeField] private CitySetTargetEventSO citySetTargetEventSO;

    [SerializeField] private List<CityViewPresenter> CityList;

    [SerializeField] private CitySO cityScriptableObject;
    [SerializeField] private OwnerDataScriptableObject ownerDataScriptableObject;
    private List<CityViewPresenter> AllyCities;

    [SerializeField] private CurrencyScriptableObject currencyScriptableObject;

    private CityViewPresenter playerCapitalCity;

    private float totalGoldPerSecond;

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
        this.CityList = new List<CityViewPresenter>(GameObject.FindGameObjectsWithTag("City").Length);
        foreach (var city in GameObject.FindGameObjectsWithTag("City"))
        {
            var cityScript = city.GetComponent<CityViewPresenter>();
            cityScript.Init(cityScriptableObject);
            CityList.Add(cityScript);

            SetCityColor(cityScript, ownerDataScriptableObject.OwnerDataDictionary);
        }

        this.AllyCities = CityList.FindAll(x => x.Owner == OwnerType.Player);
        this.playerCapitalCity = CityList.Find(x => x.IsCapital);
        this.totalGoldPerSecond = this.AllyCities.Sum(c => c.GoldPerSecond);

        SetGoldPerSecond();
    }

    private void OnCityOwnerChanged(CityOwnerChangedEventArgs args)
    {
        Debug.Log("City Captured");
        OwnerChanged(args.CapturedCity, args.NewOwner);
        SetCityColor(args.CapturedCity, ownerDataScriptableObject.OwnerDataDictionary);

        var isAdded = this.AllyCities.TryAddIfNotContains(args.CapturedCity);

        if (isAdded)
        {
            args.CapturedCity.CalculateAndSetGoldPerSecond(playerCapitalCity);
            this.totalGoldPerSecond += args.CapturedCity.GoldPerSecond;
        }
    }

    //set flag to terminate coroutine when city is lost;
    private void SetGoldPerSecond(bool terminate = false)
    {
        StartCoroutine(AddGoldPerSecondCoroutine());

        IEnumerator AddGoldPerSecondCoroutine()
        {
            while (!terminate)
            {
                this.currencyScriptableObject.AddAmount(totalGoldPerSecond);
                Debug.Log(totalGoldPerSecond);
                yield return new WaitForSeconds(Constants.Constants.GoldStepInterval);
            }
        }
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
