using System;
using Constants;
using JetBrains.Annotations;

namespace Cities
{
    public interface IDamagable
    {
        event Action<OwnerType> OnOutOfHealth;
        void TakeDamage(OwnerType owner, float amount);

        bool HasHealth { get; }
    }
}
