namespace SimpleHttpServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;

    public class HttpProcessor
    {
        private HttpRequest request;
        private HttpResponse response;
        private IList<Route> routes;

        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public HttpRequest Request
        {
            get { return this.request; }
            set { this.request = value; }
        }

        public HttpResponse Response
        {
            get { return this.response; }
            set { this.response = value; }
        }

        public IList<Route> Routes
        {
            get { return this.routes; }
            set { this.routes = value; }
        }

        public void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                this.Request = GetRequest(stream);
                this.Response = RouteRequest();
                StreamUtils.WriteRespone(stream, this.Response);
            }
        }

        private HttpResponse RouteRequest()
        {
            {
                var routes = this.Routes
                    .Where(x => Regex.Match(Request.Url, x.UrlRegex).Success)
                    .ToList();

                if (!routes.Any())
                    return HttpResponseBuilder.NotFound();

                var route = routes.SingleOrDefault(x => x.Method == Request.Method);

                if (route == null)
                    return new HttpResponse()
                    {
                        StatusCode = ResponseStatusCode.MethodNotAllowed
                    };

                #region FIleSystemHandler
                // extract the path if there is one
                //var match = Regex.Match(request.Url, route.UrlRegex);
                //if (match.Groups.Count > 1)
                //{
                //    request.Path = match.Groups[1].Value;
                //}
                //else
                //{
                //    request.Path = request.Url;
                //}
                #endregion


                // trigger the route handler...
                try
                {
                    return route.Callable(Request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return HttpResponseBuilder.InternalServerError();
                }

            }
        }

        private HttpRequest GetRequest(Stream stream)
        {
            //get method, url and protocol version
            string requestLine = StreamUtils.ReadLine(stream);
            string[] tokens = requestLine.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("Invalid Http request line");
            }

            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), tokens[0]);
            string url = tokens[1];

            // read header
            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(stream)) != null)
            {
                if (line == "")
                {
                    break;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("Invalid HTTP Header line: " + line);
                }
                string name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);

                if (name == "Cookie")
                {
                    string[] cookies = value.Split(';');
                    foreach (string cookie in cookies)
                    {
                        string[] cookieTokens = cookie.TrimStart().Split('=');
                        header.Cookies.AddCookie(new Cookie(cookieTokens[0], cookieTokens[1]));
                    }
                }
                else if (name == "Content-Length")
                {
                    header.ContentLenght = value;
                }
                else
                {
                    header.OtherParameters.Add(name, value);
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
                    int n = stream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            HttpRequest request = new HttpRequest()
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };

            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(request);
            Console.WriteLine("------------------------------");

            return request;
        }


    }
}
