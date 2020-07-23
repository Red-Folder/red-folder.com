using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RedFolder.ViewModels
{
    public class ContactForm
    {
        [Display(Name = "Your name")]
        [Required(ErrorMessage = "I really could do with knowing who I'm talking to")]
        public string Name { get; set; }

        [Display(Name = "Your email address")]
        [Required(ErrorMessage = "Its how I contact you - it doesn't get used for anything else")]
        public string EmailAddress { get; set; }

        [Display(Name = "And what problem do you want help with?")]
        [Required(ErrorMessage = "Help me to help you by telling me a little about your problem")]
        public string HowCanIHelp { get; set; }

       public string RecaptureToken { get; set; }
    }
}
