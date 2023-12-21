using Constants;
using UnityEngine;

namespace Assets.Scripts.EventSO
{
    [CreateAssetMenu(fileName = "CityCapturedEvent", menuName = "ScriptableObjects/Events/CityCapturedEvent")]
    public class CityOwnerChangedEventSO : BaseEventSO<CityOwnerChangedEventArgs> 
    {
    }

    public struct CityOwnerChangedEventArgs
    {
        // public OwnerType OldOwner;
        public OwnerType NewOwner;
        /// <summary>
        /// New owner is in the CapturedCity.Owner property
        /// </summary>
        public CityViewPresenter CapturedCity;
    }
}
