namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpResponse
    {
        #region Constructor
        public HttpResponse()
        {
            this.Header = new Header(HeaderType.HttpResponse);
            this.Content = new byte[] { };
        }
        #endregion

        #region Properties
        public ResponseStatusCode StatusCode { get; set; }

        public string StatusMessage {
            get
            {
                return Enum.GetName(typeof(ResponseStatusCode), this.StatusCode);
            }
            set
            {
                this.StatusMessage = value;
            }
        }

        public Header Header { get; set; }

        public byte[] Content { get; private set; }

        public string ContentAsUtfEight
        {
            get
            {
                return this.ContentAsUtfEight;
            }
            set
            {
                this.Content = Encoding.UTF8.GetBytes(value);
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            response.AppendLine($"HTTP/1.0 {(int)this.StatusCode} {this.StatusMessage}");
            response.AppendLine(this.Header.ToString());

            return response.ToString();
        }
        #endregion
    }
}