using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            // 1- convert the "127.0.0.1" from string to an IPAdress object
            IPAddress ipadd = IPAddress.Parse("127.0.0.1");// loopback adderss (localhost)


            //2- choose a port number to be an endpoint for communcation
            // i choose the port number 8000 is often used for local development and testing
            int port = 8000;


            // 3- used to create an instance of the TcpListener class in order to set up a TCP server
            // that listens for incoming client connections
            // resource : Microsoft offical website : https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-7.0
            TcpListener server = new TcpListener(ipadd, port);

            //3- Starts listening for incoming connection request  
            // resource : https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener.start?view=net-7.0#system-net-sockets-tcplistener-start
            server.Start();

            // 4- give notification that server start listen for connections....
            Console.WriteLine("Server start listening for connections .....");


            /*
             5- loop that listens for incoming connections
             and its an infinite loop which means server 
             will continuously listen for incoming client connections.
             */

            while (true)
            {
                try
                {

                
                // 6- create an instance of TcpClient and its the connected client
                TcpClient client = server.AcceptTcpClient();


                //7- give notifaction that the tcp client connected 
                Console.WriteLine("Client successfully connected.");


                /*
                8-The NetworkStream class provides methods for sending and receiving data
                resource:https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.networkstream?view=net-7.0
                */

                NetworkStream stream = client.GetStream();

                //9-Console.WriteLine(stream.GetType());
                //****** 10-Reading Data *********
                //  10.1- Create an array of type byte with size 1024
                byte[] buffer = new byte[1024];

                /*
                10.2 - sotre the number of bytes read in the bytesRead
                Read(byte[] buffer, int offset, int count) Method take three parameters 
                buffer: An array of type Byte that is the location in memory to store data read from the NetworkStream
                offest:The location in buffer to begin storing the data to.
                count:The number of bytes to read from the NetworkStream.
                resource:https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.networkstream.read?view=net-7.0#system-net-sockets-networkstream-read(system-byte()-system-int32-system-int32)
                */
                int BytesReads = stream.Read(buffer, 0, buffer.Length);

                /*
                10.3- sotre the recived bytes in a string variable named message
                converts the received bytes into a string message using ASCII encoding.
                */
                string message = Encoding.ASCII.GetString(buffer, 0, BytesReads);
                // print the recived message from the client
                Console.WriteLine("Received: " + message);

                /*
                  sending back message to the client in the same way of reading 
                 but we used Write insted of read
                 */
                string responseMessage = "Thank you your message recived";
                byte[] responseBuffer = Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(responseBuffer, 0, responseBuffer.Length);
                // close the connection between the client and the server
                client.Close();
                }catch (SocketException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
    }
}
