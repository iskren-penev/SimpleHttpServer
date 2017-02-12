namespace SimpleHttpServer.Models
{
    using System;
    using SimpleHttpServer.Enums;

    public class Route
    {
        private string name;
        private string urlRegex;
        private RequestMethod method;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string UrlRegex
        {
            get { return this.urlRegex; }
            set { this.urlRegex = value; }
        }

        public RequestMethod Method
        {
            get { return this.method; }
            set { this.method = value; }
        }

        public Func<HttpRequest, HttpResponse> Callable { get; set; }
    }
}
