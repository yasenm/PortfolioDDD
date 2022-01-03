using Portfolio.Application.Features;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts;

public interface IIdentity
{
    Task<bool> Register(UserInputModel userInput);

    Task<LoginOutputModel> Login(UserInputModel userInput);
}
