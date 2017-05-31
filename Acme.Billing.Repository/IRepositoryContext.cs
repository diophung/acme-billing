using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository
{
    public interface IRepositoryContext
    {
        void Update();
        void Delete();
    }
}