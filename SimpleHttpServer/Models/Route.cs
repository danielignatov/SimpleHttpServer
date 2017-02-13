namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Route
    {
        #region Properties
        public string Name { get; set; }

        public string UrlRegex { get; set; }

        public RequestMethod Method { get; set; }

        public Func<HttpRequest, HttpResponse> Callable { get; set; }
        #endregion
    }
}