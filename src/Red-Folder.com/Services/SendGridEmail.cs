using RedFolder.Models;
using RedFolder.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace RedFolder.Services
{
    public class SendGridEmail : IEmail
    {
        private const string CONTACT_THANK_YOU_TEMPLATE = "d-b7898a39f2c441d6812a466fe02b9ab4";

        private readonly SendGridConfiguration _configuration;

        public SendGridEmail(SendGridConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendContactThankYou(ContactForm contactForm)
        {
            try
            {
                var client = new SendGridClient(_configuration.ApiKey);
                var from = new EmailAddress(contactForm.EmailAddress);
                var to = new EmailAddress(_configuration.From);

                var msg = MailHelper.CreateSingleTemplateEmail(from, to, CONTACT_THANK_YOU_TEMPLATE, new
                {
                    name = HttpUtility.HtmlEncode(contactForm.Name),
                    howcanihelp = HttpUtility.HtmlEncode(contactForm.HowCanIHelp)
                });

                if (!contactForm.EmailAddress.ToLower().Equals(_configuration.From.ToLower()))
                {
                    msg.AddBcc(new EmailAddress(_configuration.From));
                }

                var response = await client.SendEmailAsync(msg);

                return response?.StatusCode == HttpStatusCode.Accepted;
            }
            catch { }

            return false;
        }
    }
}
