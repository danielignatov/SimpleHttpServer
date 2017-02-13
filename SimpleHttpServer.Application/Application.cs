namespace SimpleHttpServer.Application
{
    using Enums;
    using Models;
    using System.Collections.Generic;

    class Application
    {
        static void Main(string[] args)
        {
            var routes = new List<Route>()
            {
                new Route
                {
                    Name = "Hello Handler",
                    UrlRegex = @"^/hello$",
                    Method = RequestMethod.GET,
                    Callable = (HttpRequest request) =>
                    {
                        return new HttpResponse()
                        {
                            ContentAsUtfEight = "<h3>Hello from SimpleHttpServer :)</h3>",
                            StatusCode = ResponseStatusCode.Ok
                        };
                    }
                }
            };

            HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }
    }
}