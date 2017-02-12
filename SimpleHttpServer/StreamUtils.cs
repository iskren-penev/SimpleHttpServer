namespace SimpleHttpServer
{
    using System.IO;
    using System.Text;
    using System.Threading;
    using SimpleHttpServer.Models;

    public static class StreamUtils
    {
        public static string ReadLine(Stream stream)
        {
            int nextChar;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                nextChar = stream.ReadByte();
                if (nextChar == '\n') { break; }
                if (nextChar == '\r') { continue; }
                if (nextChar == -1) { Thread.Sleep(1); continue; };
                sb.Append((char) nextChar);
            }

            return sb.ToString();
        }

        public static void WriteRespone(Stream stream, HttpResponse response)
        {
            byte[] responseHeader = Encoding.UTF8.GetBytes(response.ToString());
            stream.Write(responseHeader, 0, responseHeader.Length);
            stream.Write(response.Content, 0, response.Content.Length);
        }
    }
}
