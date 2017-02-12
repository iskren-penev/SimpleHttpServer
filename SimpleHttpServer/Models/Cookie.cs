namespace SimpleHttpServer.Models
{
    public class Cookie
    {
        private string name;
        private string value;

        public Cookie(): this(null,null)
        {
        }

        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
