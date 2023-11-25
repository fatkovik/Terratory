using Assets.Scripts.Units;
using Constants;
using UnityEngine;

public class ShopButtonPrefabScript : MonoBehaviour
{
    public UnitType UnitType { get; private set; }

    public void Init(UnitType unitType)
    {
        this.UnitType = unitType;
    }
}
