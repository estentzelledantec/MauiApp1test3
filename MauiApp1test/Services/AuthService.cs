namespace AutoMasters.Services;
public sealed class AuthService : IAuthService
{
    private const string TokenKey = "automasters_jwt";
    public async Task<bool> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return false;
        await SecureStorage.Default.SetAsync(TokenKey, "demo-jwt-token");
        return true;
    }
    public Task LogoutAsync() { SecureStorage.Default.Remove(TokenKey); return Task.CompletedTask; }
    public Task<string?> GetTokenAsync() => SecureStorage.Default.GetAsync(TokenKey);
}
