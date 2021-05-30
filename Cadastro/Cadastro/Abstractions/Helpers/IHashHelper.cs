using System.Threading.Tasks;

namespace Cadastro.Abstractions.Helpers
{
    public interface IHashHelper
    {
        Task<string> GetMD5(string input);
    }
}