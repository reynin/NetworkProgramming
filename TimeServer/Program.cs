using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Starting up server...");
var listener = new TcpListener(IPAddress.Any, 44444);
listener.Start();
while (true)
{
    Console.WriteLine("Listening for incoming connections on " + listener.LocalEndpoint + "...");
    // accept a new incoming connection. Wait for it.
    using var client = listener.AcceptTcpClient(); // blocking
    Console.WriteLine("Client accepted: " + client.Client.RemoteEndPoint);
    // the text we want to send:
    Console.Write("Your message: ");
    string input = Console.ReadLine();
    using var writer = new StreamWriter(client.GetStream()); //Decorator pattern: StreamWriter decorates the stream by adding methods like 'Write(string text)'
    writer.Write(input);
    Console.WriteLine("Closed Connection.");
    //Thanks to "using" keyword, the compiler automatically calls Dispose first on 'writer', then on 'client'

}