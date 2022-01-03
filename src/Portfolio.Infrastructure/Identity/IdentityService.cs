namespace Portfolio.Infrastructure.Identity;

using Common.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.Contracts;
using Portfolio.Application.Features;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class IdentityService : IIdentity
{
    private const string InvalidLoginErrorMessage = "Invalid credentials.";

    private readonly UserManager<User> userManager;
    private readonly ApplicationSettings applicationSettings;

    public IdentityService(
        UserManager<User> userManager,
        IOptions<ApplicationSettings> applicationSettings)
    {
        this.userManager = userManager;
        this.applicationSettings = applicationSettings.Value;
    }

    public async Task<bool> Register(UserInputModel userInput)
    {
        var user = new User(userInput.Email);

        var identityResult = await this.userManager.CreateAsync(user, userInput.Password);

        return identityResult.Succeeded;
    }

    public async Task<LoginOutputModel> Login(UserInputModel userInput)
    {
        var user = await this.userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
        {
            return default;
        }

        var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return default;
        }

        var token = this.GenerateJwtToken(
            user.Id,
            user.Email);

        return new LoginOutputModel(token);
    }

    private string GenerateJwtToken(string userId, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, email)
                }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }
}
