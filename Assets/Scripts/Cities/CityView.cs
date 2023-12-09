using Constants;
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
        [SerializeField] private TMP_Text infantryCount;
        [SerializeField] private TMP_Text tankCount;
        [SerializeField] private TMP_Text artilleryCount;
        [SerializeField] private TMP_Text currentHealth;
        [SerializeField] private TMP_Text strength;

        //TODO: change after testing... if it must be preserved use dictionary 
        public void SetUnitOverlay(UnitType type, int count)
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

        public void SetHealthOverlay(int health)
        {
            this.currentHealth.SetText($"Health: {health}");   
        }

        public void SetStrengthOverlay(float strength)
        {
            this.strength.SetText($"Strength: {strength}");
        }

        /// <summary>
        /// False = hide, True = show
        /// </summary>
        /// <param name="hide"></param>
        public void ShowCountOverlay(bool hide)
        {
            if (this.infantryCount.gameObject.activeInHierarchy == hide && 
                this.tankCount.gameObject.activeInHierarchy == hide && 
                this.artilleryCount.gameObject.activeInHierarchy == hide &&
                this.strength.gameObject.activeInHierarchy == hide)
            {
                return;
            }

            this.infantryCount.gameObject.SetActive(false);
            this.tankCount.gameObject.SetActive(false);
            this.artilleryCount.gameObject.SetActive(false);
            this.strength.gameObject.SetActive(false);
        }
    }
}
