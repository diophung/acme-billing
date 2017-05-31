using System;
using System.Collections.Generic;
using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    /// <summary>
    /// Perform tasks related to email
    /// </summary>
    public interface IEmailRepository
    {
        /// <summary>
        /// Send email to customer by this <c>toBeSentBy</c> date.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="toBeSentBy"></param>
        void Send(Email email, DateTime toBeSentBy);

        /// <summary>
        /// Send multiple emails
        /// </summary>
        /// <param name="emails"></param>
        void SendMultiple(IList<Email> emails);
    }
}