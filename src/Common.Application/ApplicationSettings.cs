namespace Portfolio.Common.Application;

public class ApplicationSettings
{
    public ApplicationSettings() => this.Secret = default!;

    public string Secret { get; private set; }
}
