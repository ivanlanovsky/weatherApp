namespace WeatherApp.Exceptions
{
    public class ApiErrorException: Exception
    {
        public HttpResponseMessage Response { get; }
        const string _message = "Error happenen while fetching the data from API";

        public ApiErrorException(HttpResponseMessage response) : base(_message)
        {
            Response = response;
        }
    }
}
