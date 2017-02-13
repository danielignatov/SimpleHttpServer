namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HttpProcessor
    {
        #region Properties
        private IList<Route> Routes;
        private HttpRequest Request;
        private HttpResponse Response;
        #endregion

        #region Constructor
        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }
        #endregion

        #region Methods
        public void HandleClient(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                this.Request = GetRequest(stream);
                this.Response = RouteRequest();
                StreamUtils.WriteResponse(stream, this.Response);
            }
        }

        private HttpRequest GetRequest(Stream inputStream)
        {
            // Processing the first line to get RequestMethod and URL
            string[] requestParameters = StreamUtils.ReadLine(inputStream).Split(' ');

            if (requestParameters.Length != 3)
            {
                throw new Exception("Incorrect number of request parameters.");
            }

            // Processing the next lines to obtain header
            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), requestParameters[0].ToString().ToUpper());
            string url = requestParameters[1];

            Header header = new Header(HeaderType.HttpRequest);
            string line;

            while ((line = StreamUtils.ReadLine(inputStream)) != null)
            {
                if (String.IsNullOrEmpty(line)) // Possibly line.Eq = ""
                {
                    break;
                }

                string[] lineParameters = line.Split(':'); // Implement exeption
                string lineName = lineParameters[0];
                string lineValue = lineParameters[1];

                if (lineName.ToLower() == "cookie")
                {
                    string[] cookies = lineValue.Split(';');

                    foreach (var cookie in cookies)
                    {
                        string[] cookieParameters = cookie.Trim().Split('=');
                        string cookieName = cookieParameters[0];
                        string cookieValue = cookieParameters[1];

                        header.Cookies.Add(new Cookie(cookieName, cookieValue));
                    }
                }
                else if (lineName.ToLower() == "content-lenght")
                {
                    header.ContentLenght = lineValue;
                }
                else
                {
                    header.OtherParameters.Add(lineName, lineValue);
                }
            }

            string content = null;

            if (header.ContentLenght != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLenght);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            var request = new HttpRequest()
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };

            return request;
        }

        private HttpResponse RouteRequest()
        {
            var routes = this.Routes
                .Where(x => Regex.Match(Request.Url, x.UrlRegex).Success)
                .ToList();

            if (!routes.Any())
            {
                return HttpResponseBuilder.NotFound();
            }

            var route = routes.SingleOrDefault(x => x.Method == Request.Method);

            if (route == null)
            {
                return new HttpResponse()
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };
            }

            try
            {
                // Succes
                return route.Callable(this.Request);
            }
            catch (Exception)
            {
                // Error 500
                Console.WriteLine("Internal Server Error");
                return HttpResponseBuilder.InternalServerError();
            }
        }
        #endregion
    }
}