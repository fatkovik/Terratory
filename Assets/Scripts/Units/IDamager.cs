using Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units
{
    public interface IDamager
    {
        void GiveDamage(IDamagable damageReciever, OwnerType owner, float amount);
    }
}
