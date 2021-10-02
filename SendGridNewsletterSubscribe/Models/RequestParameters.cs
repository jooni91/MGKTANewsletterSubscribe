using System;

namespace SendGridNewsletterSubscribe.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CountryCode { get; set; }

        public RequestParameters(string email, string lang)
        {
            Email = email;
            CountryCode = lang;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ValidateModel()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ApplicationException($"The {nameof(Email)} request parameter was not provided.");
            }

            if (string.IsNullOrEmpty(CountryCode))
            {
                throw new ApplicationException($"The {nameof(CountryCode)} request parameter was not provided.");
            }
        }
    }
}
