using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGridNewsletterSubscribe.Models;
using SendGridNewsletterSubscribe.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SendGridNewsletterSubscribe
{
    public static class SubscribeToNewsletter
    {
        [FunctionName("SubscribeToNewsletter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "subscribe-newsletter")] HttpRequest req, ILogger log, 
            ExecutionContext context, System.Threading.CancellationToken cancellationToken)
        {
            log.LogInformation("Got a new request to add an email to our newsletter subscriber list.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestParameters>(requestBody);

            (data ?? throw new ApplicationException("Deserialization of the request body was not successfull.")).ValidateModel();

            var config = GetConfig(context);
            var service = new SendGridApiService(config.GetValue<string>("SendGridApiKey"));

            await service.AddOrUpdateContact(new List<string>() { data.Email }, CreateContactIdList(config, data.CountryCode), cancellationToken);

            log.LogInformation("Email added successfully.");

            return new OkResult();
        }

        private static IConfiguration GetConfig(ExecutionContext context)
        {
            return new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(typeof(SubscribeToNewsletter).Assembly)
                .AddEnvironmentVariables()
                .Build();
        }
        private static List<string> CreateContactIdList(IConfiguration configuration, string lang)
        {
            return new List<string>() {
                lang switch
                {
                    "fi" => configuration.GetValue<string>("ContactListIdFi"),
                    _ => throw new ArgumentOutOfRangeException($"{lang} is not a supported language.")
                }
            };
        }
    }
}
