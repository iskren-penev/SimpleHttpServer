namespace SimpleHttpServer
{
    using System.IO;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;

    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string path = Path.GetFullPath(Path.Combine(currentPath, @"..\..\..\"));
            string internalServerErrorPath = path + "\\SimpleHttpServer\\Resources\\Pages\\500.html";
            string content = File.ReadAllText(internalServerErrorPath);

            return new HttpResponse()
            {
                StatusCode = ResponseStatusCode.InternalServerError,
                ContentAsUtf8 = content
            };
        }

        public static HttpResponse NotFound()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string path = Path.GetFullPath(Path.Combine(currentPath, @"..\..\..\"));
            string notFoundPath = path + "\\SimpleHttpServer\\Resources\\Pages\\404.html";
            string content =File.ReadAllText(notFoundPath);
            return new HttpResponse()
            {
                StatusCode = ResponseStatusCode.NotFound,
                ContentAsUtf8 = content
            };
        }
    }
}
