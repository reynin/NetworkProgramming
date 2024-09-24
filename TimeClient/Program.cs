using System.Net.Sockets;
using System.Text;

Console.WriteLine("Connecting to server...");
var client = new TcpClient();
client.Connect("127.0.0.1", 44444); // <-- Target IP address
Console.WriteLine("Server accepted: " + client.Client.RemoteEndPoint);

// Create an array in which we can store the received bytes
var buffer = new byte[1000];
// Read as many bytes as we can from the stream. Maximum 1000. The first byte of the array can be used.
var bytesReceived = client.GetStream().Read(buffer, 0, buffer.Length);
// Convert the bytes to readable text using ASCII format
var currentTime = Encoding.ASCII.GetString(buffer);
Console.WriteLine(currentTime);