namespace SimpleHttpServer.Models
{
    using Enums;
    using System.IO;

    public static class HttpResponseBuilder
    {
        public static HttpResponse NotFound()
        {
            string pageContent = File.ReadAllText("../../../SimpleHttpServer/Resources/Pages/404.html");
            HttpResponse response = new HttpResponse();
            response.StatusCode = ResponseStatusCode.NotFound;
            response.Header = new Header(HeaderType.HttpResponse);
            response.ContentAsUtfEight = pageContent;

            return response;
        }

        public static HttpResponse MethodNotAllowed()
        {
            string pageContent = File.ReadAllText("../../../SimpleHttpServer/Resources/Pages/405.html");
            HttpResponse response = new HttpResponse();
            response.StatusCode = ResponseStatusCode.MethodNotAllowed;
            response.Header = new Header(HeaderType.HttpResponse);
            response.ContentAsUtfEight = pageContent;

            return response;
        }

        public static HttpResponse InternalServerError()
        {
            string pageContent = File.ReadAllText("../../../SimpleHttpServer/Resources/Pages/500.html");
            HttpResponse response = new HttpResponse();
            response.StatusCode = ResponseStatusCode.InternalServerError;
            response.Header = new Header(HeaderType.HttpResponse);
            response.ContentAsUtfEight = pageContent;

            return response;
        }
    }
}