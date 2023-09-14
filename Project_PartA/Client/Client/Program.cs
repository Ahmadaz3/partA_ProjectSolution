using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                TcpClient client = new TcpClient();
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");// loop pack ip for testing 
                int port = 8000;
                client.Connect(serverIP, port);
                Console.Write("Enter a message to send to the server: ");
                string message = Console.ReadLine();
                byte[] buffer = Encoding.ASCII. GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
                byte[] responseBuffer = new byte[1024];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string responseMessage = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                Console.WriteLine("Server response: " + responseMessage);
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
            }
             
        }

    }
}

