using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Billing.DomainModel;
using Acme.Billing.Repository.Interface;

namespace Acme.Billing.Repository.Implementation
{
    public class EmailRepository : RepositoryBase, IEmailRepository
    {
        public EmailRepository()
        {
            
        }
        public void Send(Email email, DateTime toBeSentBy)
        {
            //todo: implement emailing system
            throw new NotImplementedException();
        }

        public void SendMultiple(IEnumerable<Email> emails)
        {
            throw new NotImplementedException();
        }
    }
}
