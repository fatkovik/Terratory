using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Constants;
using UnityEngine;

namespace Scripts
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField] private Unit unitPrefab;

        //TODO: somehow make it private
        [SerializedDictionary("Unit Type", "Config")]
        public SerializedDictionary<UnitType, UnitScriptableObject> UnitConfigs;

        public Unit CreateUnit(UnitType unitType, Vector3 position)
        {
            Unit newUnit = Instantiate(unitPrefab, position, Quaternion.identity);

            newUnit.Init(UnitConfigs[unitType]);
            return newUnit;
        }
    }
}
