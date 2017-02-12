namespace SimpleHttpServer.Models
{
    using System.Text;
    using SimpleHttpServer.Enums;

    public class HttpRequest
    {
        private RequestMethod method;
        private string url;
        private string content;
        private Header header;

        public HttpRequest()
        {
            this.Header = new Header(HeaderType.HttpRequest);
        }

        public Header Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public RequestMethod Method
        {
            get { return this.method; }
            set { this.method = value; }
        }

        public override string ToString()
        {
            return $"{this.Method} {this.Url} HTTP/1.0\r\n{this.header}{this.Content}";
        }
    }
}
