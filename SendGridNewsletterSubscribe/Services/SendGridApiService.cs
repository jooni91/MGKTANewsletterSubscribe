using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGridNewsletterSubscribe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SendGridNewsletterSubscribe.Services
{
    /// <inheritdoc/>
    public class SendGridApiService : ISendGridApiService
    {
        private readonly ISendGridClient _client;

        public SendGridApiService(string apiKey)
        {
            _client = new SendGridClient(apiKey);
        }
        public SendGridApiService(SendGridClient client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public async Task<string> AddOrUpdateContact(List<string> emails, List<string>? contactListIds, CancellationToken cancellationToken = default)
        {
            return await AddOrUpdateContact(emails.Select(email => new Recipient(email)).ToList(), contactListIds, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<string> AddOrUpdateContact(List<Recipient> recipients, List<string>? contactListIds, CancellationToken cancellationToken = default)
        {
            var payload = JsonConvert.SerializeObject(new
            {
                list_ids = contactListIds,
                contacts = recipients
            }, 
            new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = await _client.RequestAsync(BaseClient.Method.PUT, payload, urlPath: "marketing/contacts", cancellationToken: cancellationToken);
            var body = await response.Body.ReadAsStringAsync();
            var jobId = JObject.Parse(body).Value<string>("job_id");

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(jobId))
            {
                throw new ApplicationException($"Adding recipient failed. Response body: {body}");
            }

            return jobId;
        }
    }
}
