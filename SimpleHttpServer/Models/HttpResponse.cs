namespace SimpleHttpServer.Models
{
    using System;
    using System.Text;
    using SimpleHttpServer.Enums;

    public class HttpResponse
    {
        private ResponseStatusCode statusCode;
        private Header header;
        private byte[] content;

        public HttpResponse()
        {
            this.Header = new Header(HeaderType.HttpResponse);
            this.Content = new byte[] {};
        }
        public ResponseStatusCode StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string StatusMessage
        {
            get { return Enum.GetName(typeof(ResponseStatusCode), this.StatusCode); }
        }

        public Header Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        public byte[] Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public string ContentAsUtf8
        {
            set { this.Content = Encoding.UTF8.GetBytes(value); }
        }

        public override string ToString()
        {
            return $"HTTP/1.0 {(int)this.StatusCode} {this.StatusMessage}\r\n{this.Header}";
        }
    }
}
