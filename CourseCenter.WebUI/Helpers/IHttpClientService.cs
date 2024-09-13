namespace CourseCenter.WebUI.Helpers
{
    public interface IHttpClientService
    {
        HttpClient CreateHttpClient();
        Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod method, string apiUrl, TRequest requestDataObject = default);
    }
}
