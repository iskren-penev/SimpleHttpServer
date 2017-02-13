namespace Tester
{
    using System.Collections.Generic;
    using SimpleHttpServer;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;

    class TesterStartUp
    {
        public static void Main(string[] args)
        {
            List<Route> routes = new List<Route>()
            {
                new Route
                {
                    Name = "HelloHandler",
                    UrlRegex = $"^/hello$",
                    Method = RequestMethod.GET,
                    Callable = (HttpRequest request) =>
                    {
                        return new HttpResponse()
                        {
                            ContentAsUtf8 = "<h1>Hello there ;)</h1>",
                            StatusCode = ResponseStatusCode.OK
                        };
                    }
                }
            };

            HttpServer server = new HttpServer(8181, routes);
            server.Listen();
        }
    }
}
