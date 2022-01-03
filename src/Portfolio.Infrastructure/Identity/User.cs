namespace Portfolio.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    internal User(string email) : base(email) => this.Email = email;
}
