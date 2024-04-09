using AutoMapper;
using Marketplace.Application.Services.Encrypter;
using Marketplace.Application.Services.Token;
using Marketplace.Communication.Request;
using Marketplace.Communication.Response;
using Marketplace.Domain.Repository;
using Marketplace.Domain.Repository.User;

namespace Marketplace.Application.UseCases.User.Signup;

public class SignupUseCase : ISignupUseCase
{
    private readonly IUserWriteOnlyRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnity _workUnity;
    private readonly PasswordEncrypter _passwordEncrypter;
    private readonly TokenController _tokenController;

    public SignupUseCase(IUserWriteOnlyRepository userRepository, IMapper mapper, IWorkUnity workUnity, PasswordEncrypter passwordEncrypter, TokenController tokenController)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _workUnity = workUnity;
        _passwordEncrypter = passwordEncrypter;
        _tokenController = tokenController;
    }

    public async Task<ResponseSignupUserJson> Execute(RequestSignupUserJson request)
    {
        var user = _mapper.Map<Martkeplace.Domain.Entity.User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _userRepository.Create(user);
        await _workUnity.Commit();

        var token = _tokenController.GenerateToken(request.Email);

        return new ResponseSignupUserJson
        {
            Token = token
        };
    }
}
