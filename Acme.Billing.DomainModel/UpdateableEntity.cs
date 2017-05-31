using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Billing.DomainModel
{
    public abstract class UpdateableEntity
    {
        public abstract void Update();
        public abstract void Delete();
        public abstract void Sync();
    }
}
