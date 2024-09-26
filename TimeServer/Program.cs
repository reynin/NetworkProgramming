using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Starting up server...");
var listener = new TcpListener(IPAddress.Any, 44444);
listener.Start();
while (true)
{
    Console.WriteLine("Listening for incoming connections on " + listener.LocalEndpoint + "...");
    using var client = listener.AcceptTcpClient(); // TcpClient implements IDisposable, we should either invoke Dispose manually, or use the `using` keyword
    Console.WriteLine("Client accepted: " + client.Client.RemoteEndPoint);
    Console.Write("Your Message: ");
    string input = Console.ReadLine();
    using var writer = new StreamWriter(client.GetStream()); // Decorator pattern: StreamWriter decorates the stream by adding methods like `Write(string text)`
    writer.Write(input);
    Console.WriteLine("Closed connection.");
    // thanks to "using" keyword, the compiler automatically calls Dispose first on `writer`, then on `client`
}