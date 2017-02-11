namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Header
    {
        #region Constructor
        public Header(HeaderType type)
        {
            this.Type = type;
            this.ContentType = "text/html";
            this.Cookies = new CookieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }
        #endregion

        #region Properties
        public HeaderType Type { get; set; }

        public string ContentType { get; set; }

        public string ContentLenght { get; set; }

        public CookieCollection Cookies { get; private set; }

        public Dictionary<string, string> OtherParameters { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder header = new StringBuilder();

            header.AppendLine($"Content-type: {this.ContentType}");

            if (this.Cookies.Count > 0)
            {
                if (this.Type == HeaderType.HttpRequest)
                {
                    header.AppendLine($"Cookie: {this.Cookies.ToString()}");
                }
                else if (this.Type == HeaderType.HttpResponse)
                {
                    foreach (var cookie in this.Cookies)
                    {
                        header.AppendLine($"Set-Cookie: {cookie}");
                    }
                }
            }

            if (this.ContentLenght != null)
            {
                header.AppendLine($"Content-Length: {this.ContentLenght}");
            }

            foreach (var other in this.OtherParameters)
            {
                header.AppendLine($"{other.Key}: {other.Value}");
            }

            header.AppendLine();
            header.AppendLine();

            return header.ToString();
        }
        #endregion
    }
}