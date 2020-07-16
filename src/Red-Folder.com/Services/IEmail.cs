using System.Threading.Tasks;
using RedFolder.ViewModels;

namespace RedFolder.Services
{
    public interface IEmail
    {
        Task<bool> SendContactThankYou(ContactForm contactForm);
    }
}