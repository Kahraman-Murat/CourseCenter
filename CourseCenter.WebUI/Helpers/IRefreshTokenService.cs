namespace CourseCenter.WebUI.Helpers
{
    public interface IRefreshTokenService
    {
        Task<bool> RefreshTokensAsync();
        void RemoveTokensCookies();
        void SaveTokensCookies(string accessToken, string refreshToken);
        string GetAccessToken();
    }
}
