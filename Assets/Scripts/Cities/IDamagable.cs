using System;
using Units;

namespace Cities
{
    public interface IDamagable
    {
        event Action<OwnerType> OnOutOfHealth;
        void TakeDamage(OwnerType owner, float amount);
    }
}
