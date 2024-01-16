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
using Extensions;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CityOwnerChangedEventSO cityOwnerChangedEventSo;
    [SerializeField] private CitySetTargetEventSO citySetTargetEventSO;

    [SerializeField] private List<CityViewPresenter> CityList;

    [SerializeField] private CitySO cityScriptableObject;
    [SerializeField] private OwnerDataScriptableObject ownerDataScriptableObject;
    private static List<CityViewPresenter> AllyCities;

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
            cityScript.Rebelled += OnCityRebelled;
            CityList.Add(cityScript);

            SetCityColor(cityScript, ownerDataScriptableObject.OwnerDataDictionary);
        }

        AllyCities = CityList.FindAll(x => x.Owner == OwnerType.Player);
        this.playerCapitalCity = CityList.Find(x => x.IsCapital);
        this.totalGoldPerSecond = AllyCities.Sum(c => c.GoldPerSecond);

        this.currencyScriptableObject.ChangePerSecondAmount(totalGoldPerSecond);
        SetGoldPerSecond();
    }

    private void OnCityRebelled(CityViewPresenter city)
    {
        OwnerChanged(city,OwnerType.Neutral);
    }

    private void OnCityOwnerChanged(CityOwnerChangedEventArgs args)
    {
        Debug.Log("City Captured");
        OwnerChanged(args.CapturedCity, args.NewOwner);
        SetCityColor(args.CapturedCity, ownerDataScriptableObject.OwnerDataDictionary);

        var isAdded = AllyCities.TryAddIfNotContains(args.CapturedCity);

        if (isAdded)
        {
            args.CapturedCity.CalculateAndSetGoldPerSecond(playerCapitalCity);
            this.totalGoldPerSecond += args.CapturedCity.GoldPerSecond;
            this.currencyScriptableObject.ChangePerSecondAmount(totalGoldPerSecond);
        }
    }

    private void SetCityColor(CityViewPresenter city, SerializedDictionary<OwnerType, OwnerData> OwnerDataDictionary)
    {
        city.SetColor(OwnerDataDictionary[city.Owner]);
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
                yield return new WaitForSeconds(Constants.Constants.CurrencyStepInterval);
            }
        }
    }
    private void OwnerChanged(CityViewPresenter city, OwnerType newOwner)
    {
        city.Owner = newOwner;
        SetCityColor(city, ownerDataScriptableObject.OwnerDataDictionary);
    }
    
    private void OnCityTargetSet(AttackInfo data)
    {
        Debug.Log("Enemy city selected");
        data.PlayerCity.SetTarget(data.EnemyCity);
    }

    public static CityViewPresenter GetTheNearestAllyCity(CityViewPresenter city)
    {
        if (AllyCities == null || AllyCities.Count <= 1)
        {
            Debug.LogError("Positions list is empty or null!");
            return null;
        }

        Vector3 cityPosition = city.transform.position;
        CityViewPresenter nearestCity = AllyCities[0];
        float minDistance = Vector3.Distance(cityPosition, nearestCity.transform.position);

        foreach (var c in AllyCities)
        {
            if(city == c) continue;
            float distance = Vector3.Distance(cityPosition, c.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCity = c;
            }
        }

        return nearestCity;
    }
}

