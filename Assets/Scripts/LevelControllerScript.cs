using Cities;
using Scripts;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{
    [SerializeField]
    private List<CityViewPresenter> CityList;

    [SerializeField]
    private CityScriptableObject cityScriptableObject;

    //TESTING
    private void Start()
    {
        CityList = new List<CityViewPresenter>(GameObject.FindGameObjectsWithTag("City").Length);
        foreach (var city in GameObject.FindGameObjectsWithTag("City"))
        {
            var cityScript = city.GetComponent<CityViewPresenter>();
            cityScript.Init(cityScriptableObject);
            CityList.Add(cityScript);
        }
    }
}
