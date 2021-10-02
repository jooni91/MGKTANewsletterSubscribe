using Newtonsoft.Json;

namespace SendGridNewsletterSubscribe.Models
{
    public class Recipient
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? LastName { get; set; }

        [JsonProperty(PropertyName = "address_line_1", NullValueHandling = NullValueHandling.Ignore)]
        public string? AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "address_line_2", NullValueHandling = NullValueHandling.Ignore)]
        public string? AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "alternate_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? AlternateEmails { get; set; }

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string? City { get; set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }

        [JsonProperty(PropertyName = "postal_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? PostalCode { get; set; }

        [JsonProperty(PropertyName = "state_province_region", NullValueHandling = NullValueHandling.Ignore)]
        public string? StateProvinceRegion { get; set; }

        [JsonProperty(PropertyName = "custom_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object? CustomFields { get; set; }

        public Recipient(string email)
        {
            Email = email;
        }
    }
}
