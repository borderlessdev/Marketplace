using Marketplace.Application.Services.Encrypter;
using Marketplace.Application.Services.Token;
using Marketplace.Application.UseCases.User.Signup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Marketplace.Application;

public static class Bootstrapper
{

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddingAdditionalPasswordKey(services, configuration);
        AddingJwtToken(services, configuration);
        AddingUseCases(services);
    }

    private static void AddingAdditionalPasswordKey(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuration:Password:AdditionalKey");

        services.AddScoped(opt => new PasswordEncrypter(section.Value));
    }

    private static void AddingJwtToken(IServiceCollection services, IConfiguration configuration)
    {
        var lifetimeSection = configuration.GetRequiredSection("Configuration:Jwt:Lifetime");
        var secretKeySection = configuration.GetRequiredSection("Configuration:Jwt:SecretKey");

        services.AddScoped(opt => new TokenController(int.Parse(lifetimeSection.Value), secretKeySection.Value));


    }

    private static void AddingUseCases(IServiceCollection services)
    {
        services.AddScoped<ISignupUseCase, SignupUseCase>();
    }

    
}
