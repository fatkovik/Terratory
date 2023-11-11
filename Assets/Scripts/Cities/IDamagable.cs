using System;
using Constants;

namespace Cities
{
    public interface IDamagable
    {
        event Action<OwnerType> OnOutOfHealth;
        void TakeDamage(OwnerType owner, float amount);
    }
}
