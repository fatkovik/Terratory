﻿using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Cities
{
    public class CityView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text infantryCount;

        [SerializeField]
        private TMP_Text tankCount;

        [SerializeField]
        private TMP_Text artilleryCount;

        public void Initilize(UnitType type, int count)
        {
            switch (type)
            {
                case UnitType.Infantry:
                    infantryCount.SetText($"Infanty: {count}");
                    break;
                case UnitType.Tank:
                    tankCount.SetText($"Tanks: {count}");
                    break;
                case UnitType.Artillery:
                    artilleryCount.SetText($"Artillery: {count}");
                    break;
                default:
                    break;
            }
        }
    }
}