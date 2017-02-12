namespace SimpleHttpServer
{
    using System.IO;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;

    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = File.ReadAllText("Resources/Pages/500.html");

            return new HttpResponse()
            {
                StatusCode = ResponseStatusCode.InternalServerError,
                ContentAsUtf8 = content
            };
        }

        public static HttpResponse NotFound()
        {
            string content = File.ReadAllText("Resources/Pages/404.html");

            return new HttpResponse()
            {
                StatusCode = ResponseStatusCode.NotFound,
                ContentAsUtf8 = content
            };
        }
    }
}
