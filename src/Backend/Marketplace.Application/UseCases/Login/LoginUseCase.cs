using Marketplace.Application.Services.Encrypter;
using Marketplace.Application.Services.Token;
using Marketplace.Communication.Request;
using Marketplace.Communication.Response;
using Marketplace.Domain.Repository.User;
using Marketplace.Exceptions.ExceptionsBase;

namespace Marketplace.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly PasswordEncrypter _encrypter;
    private readonly TokenController _tokenContoller;
    private readonly IUserReadOnlyRepository _userRepository;

    public LoginUseCase(PasswordEncrypter encrypter, TokenController tokenContoller, IUserReadOnlyRepository userRepository)
    {
        _encrypter = encrypter;
        _tokenContoller = tokenContoller;
        _userRepository = userRepository;
    }

    public async Task<ResponseUserLoggedJson> Execute(RequestUserLoginJson request)
    {
        var passwordEncrypted = _encrypter.Encrypt(request.Password);

        var user = await _userRepository.Login(request.Email, passwordEncrypted);

        if (user == null)
        {
            throw new ErrorInvalidLogin();
        }

        return new ResponseUserLoggedJson
        {
            Name = user.Name,
            Token = _tokenContoller.GenerateToken(request.Email)
        };
    }

}
