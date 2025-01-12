using CourseCenter.WebUI.DTOs.AuthDtos;

namespace CourseCenter.WebUI.Helpers
{
    public interface IRefreshTokenService
    {
        Task<bool> RefreshTokensAsync();
        void RemoveTokensCookies();
        void SaveTokensCookies(string accessToken, string refreshToken);
        string GetAccessToken();
        UserFromTokenDto GetUserFromToken(string accessToken);
        string[] GetUserRolesFromToken(string accessToken);
    }
}
