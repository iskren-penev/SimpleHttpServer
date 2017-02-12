namespace SimpleHttpServer.Models
{
    using System.Collections.Generic;
    using System.Text;
    using SimpleHttpServer.Enums;

    public class Header
    {
        private HeaderType type;
        private string contentType;
        private string contentLenght;
        private CookieCollection cookies;
        private IDictionary<string, string> otherParameters;

        public Header(HeaderType type)
        {
            this.Type = type;
            this.ContentType = "text/html";
            this.Cookies = new CookieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }

        public HeaderType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string ContentType
        {
            get { return this.contentType; }
            set { this.contentType = value; }
        }

        public string ContentLenght
        {
            get { return this.contentLenght; }
            set { this.contentLenght = value; }
        }

        public CookieCollection Cookies
        {
            get { return this.cookies; }
            set { this.cookies = value; }
        }

        public IDictionary<string, string> OtherParameters
        {
            get { return this.otherParameters; }
            set { this.otherParameters = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Content-type: {this.ContentType}");
            if (this.Cookies.Count > 0)
            {
                if (this.Type == HeaderType.HttpRequest)
                {
                    sb.AppendLine($"Cookie: {this.Cookies.ToString()}");
                }
                else if (this.Type == HeaderType.HttpResponse)
                {
                    foreach (Cookie cookie in Cookies)
                    {
                        sb.AppendLine($"Set-Cookie: {cookie}");
                    }
                }
            }
            if (this.ContentLenght != null)
            {
                sb.AppendLine($"Content-Lenght: {this.ContentLenght}");
            }
            foreach (var otherParameter in OtherParameters)
            {
                sb.AppendLine($"{otherParameter.Key}: {otherParameter.Value}");
            }
            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
