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
        public HttpResponse(ResponseStatusCode statusCode, Header header, string content)
        {

        }
        #endregion

        #region Properties
        public ResponseStatusCode StatusCode { get; set; }

        public string StatusMessage {
            get
            {
                StringBuilder statusMessage = new StringBuilder();

                foreach (var letter in this.StatusMessage)
                {
                    if (letter != ' ')
                    {
                        statusMessage.Append(letter);
                    }
                    else
                    {
                        statusMessage.Append($"{letter} ");
                    }
                }

                return statusMessage.ToString();
            }
            set
            {
                this.StatusMessage = value;
            }
        }

        public Header Header { get; set; }

        public byte[] Content { get; private set; }

        public byte[] ContentAsUtfEight
        {
            get
            {
                return this.ContentAsUtfEight;
            }
            set
            {
                char[] chars = Encoding.Unicode.GetChars(value);
                byte[] bytes = Encoding.Unicode.GetBytes(chars);

                this.ContentAsUtfEight = bytes;
                this.Content = bytes;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            response.AppendLine($"HTTP/1.0 {(int)this.StatusCode} {this.StatusMessage}");
            response.AppendLine(this.Header.ToString());

            return base.ToString();
        }
        #endregion
    }
}