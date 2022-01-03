namespace Portfolio.Application.Features;

public class LoginOutputModel
{
    public LoginOutputModel(string token)
            => this.Token = token;

    public string Token { get; }
}
