using System;
using Constants;
using JetBrains.Annotations;

namespace Cities
{
    public interface IDamagable
    {
        void TakeDamage(OwnerType owner, float amount);

        bool HasHealth { get; }
    }
}
