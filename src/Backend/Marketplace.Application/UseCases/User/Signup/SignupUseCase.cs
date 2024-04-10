using AutoMapper;
using Marketplace.Application.Services.Encrypter;
using Marketplace.Application.Services.Token;
using Marketplace.Communication.Request;
using Marketplace.Communication.Response;
using Marketplace.Domain.Repository;
using Marketplace.Domain.Repository.User;
using Marketplace.Exceptions;
using Marketplace.Exceptions.ExceptionsBase;

namespace Marketplace.Application.UseCases.User.Signup;

public class SignupUseCase : ISignupUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnity _workUnity;
    private readonly PasswordEncrypter _passwordEncrypter;
    private readonly TokenController _tokenController;

    public SignupUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper, IWorkUnity workUnity, PasswordEncrypter passwordEncrypter, TokenController tokenController)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _workUnity = workUnity;
        _passwordEncrypter = passwordEncrypter;
        _tokenController = tokenController;
    }

    public async Task<ResponseSignupUserJson> Execute(RequestSignupUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entity.User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _userWriteOnlyRepository.Create(user);
        await _workUnity.Commit();

        var token = _tokenController.GenerateToken(request.Email);

        return new ResponseSignupUserJson
        {
            Token = token
        };
    }

    private async Task Validate(RequestSignupUserJson request)
    {
        var validator = new SignupValidator();
        var validationResult = await validator.ValidateAsync(request);

        var userExists = await _userReadOnlyRepository.ExistsUserByEmail(request.Email);

        if (userExists)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Email", ResourceErrorMessages.EMAIL_REPETIDO));
        }

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorsValidationException(errorMessages);
        }
    }
}
