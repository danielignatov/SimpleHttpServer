namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpRequest
    {
        #region Properties
        public RequestMethod Method { get; set; }

        public string Url { get; set; }

        public Header Header { get; set; }

        public string Content { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder request = new StringBuilder();

            request.AppendLine($"{this.Method.ToString()} {this.Url} HTTP/1.0");
            request.AppendLine($"{this.Header.ToString()}");
            request.AppendLine($"{this.Content}");

            return request.ToString();
        }
        #endregion
    }
}