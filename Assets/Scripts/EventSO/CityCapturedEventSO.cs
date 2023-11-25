using Constants;
using UnityEngine;

namespace Assets.Scripts.EventSO
{
    [CreateAssetMenu(fileName = "CityCapturedEvent", menuName = "ScriptableObjects/Events/CityCapturedEvent")]
    public class CityCapturedEventSO : BaseEventSO<CityCapturedEventArgs> 
    {
    }

    public struct CityCapturedEventArgs
    {
        public OwnerType OldOwner;
        /// <summary>
        /// New owner is in the CapturedCity.Owner property
        /// </summary>
        public CityViewPresenter CapturedCity;
    }
}
