using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Units;
using AYellowpaper.SerializedCollections;
using Constants;
using UnityEngine;

namespace Scripts
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField] 
        private Unit unitPrefab;

        [SerializeField]
        private UnitDBSO unitDB;

        public Unit CreateUnit(UnitType unitType, Vector3 position)
        {
            Unit newUnit = Instantiate(unitPrefab, position, Quaternion.identity);
            newUnit.SetConfig(unitDB.UnitConfigs[unitType]);
            return newUnit;
        }
    }
}
