﻿namespace SimpleHttpServer.Models
{
    using Enums;
    using System.Text;

    public class HttpRequest
    {
        #region Constructor
        public HttpRequest()
        {
            this.Header = new Header(HeaderType.HttpRequest);
        }
        #endregion

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