namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string pageContent = File.ReadAllText("../Resources/Pages/500.html");
            HttpResponse response = new HttpResponse(ResponseStatusCode.InternalServerError, )

            return 
        }

        public static HttpResponse NotFound()
        {

        }
    }
}