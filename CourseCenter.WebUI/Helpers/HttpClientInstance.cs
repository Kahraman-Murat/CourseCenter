namespace CourseCenter.WebUI.Helpers
{
    public static class HttpClientInstance
    {

        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();

            //http://localhost:5064/api/
            //https://localhost:7092/api/
            
            client.BaseAddress = new Uri("http://localhost:5064/api/"); 

            return client;
        } 


    }
}
