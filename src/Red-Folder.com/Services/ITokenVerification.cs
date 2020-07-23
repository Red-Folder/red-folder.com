using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface ITokenVerification
    {
        Task<bool> Valid(string token);
    }
}