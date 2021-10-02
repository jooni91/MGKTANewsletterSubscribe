using SendGridNewsletterSubscribe.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SendGridNewsletterSubscribe.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISendGridApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="contactListIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> AddOrUpdateContact(List<string> emails, List<string>? contactListIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="contactListIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> AddOrUpdateContact(List<Recipient> recipients, List<string>? contactListIds, CancellationToken cancellationToken = default);
    }
}
