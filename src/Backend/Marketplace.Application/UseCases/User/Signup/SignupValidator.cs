using FluentValidation;
using Marketplace.Communication.Request;
using Marketplace.Exceptions;
using System.Text.RegularExpressions;

namespace Marketplace.Application.UseCases.User.Signup;

public class SignupValidator : AbstractValidator<RequestSignupUserJson>
{
    public SignupValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_USER_INVALIDO);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_VAZIO);

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.SOBRENOME_VAZIO);

        RuleFor(r => r.Cpf).Custom((cpf, context) =>
        {
            string cpfPattern = "[0-9]{9}";
            TimeSpan timeout = TimeSpan.FromMilliseconds(500);
            var result = Regex.IsMatch(cpf, cpfPattern, RegexOptions.None, timeout);
             
            if (!result)
            {
                context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(cpf), ResourceErrorMessages.CPF_INVALIDO));
            }
        });

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.SENHA_USER_VAZIO);
    }
}
