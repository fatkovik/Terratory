using UnityEngine;

namespace Assets.Scripts.EventSO
{
    [CreateAssetMenu(fileName = "CitySetTargetEventSO", menuName = "ScriptableObjects/Events/CitySetTargetEventSO")]
    public class CitySetTargetEventSO : BaseEventSO<AttackInfo>
    {
    }

    public struct AttackInfo
    {
        public readonly CityViewPresenter PlayerCity;
        public readonly CityViewPresenter EnemyCity;

        public AttackInfo(CityViewPresenter player, CityViewPresenter enemy)
        {
            PlayerCity = player;
            EnemyCity = enemy;
        }
    }
}
