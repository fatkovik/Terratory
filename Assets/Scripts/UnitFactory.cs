using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using Assets.Scripts.Units;
using AYellowpaper.SerializedCollections;
using Constants;
using UnityEngine;

namespace Scripts
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private UnitDBSO unitDB;
        [SerializeField] private OwnerDataScriptableObject ownerDataSO;

        public Unit CreateUnit(UnitType unitType, OwnerType owner, Vector3 position)
        {
            Unit newUnit = Instantiate(unitPrefab, position, Quaternion.identity);
            newUnit.SetConfig(unitDB.UnitConfigs[unitType]);
            newUnit.owner = owner;
            newUnit.SetColor(ownerDataSO.OwnerDataDictionary[owner].UnitColor);
            return newUnit;
        }
    }
}
