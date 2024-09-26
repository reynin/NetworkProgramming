using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.Write("Enter PortKey: ");
int portKey = int.Parse(Console.ReadLine());

using UdpClient server = new UdpClient(portKey);

var ServerMessage = string.Empty;

Console.WriteLine("Awaiting message...");

while (true)
{
    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 44446);
    var bytes = server.Receive(ref remoteEP); // Incoming Bytes
    var incommingMessage = Encoding.ASCII.GetString(bytes); // Incoming Message

    if (incommingMessage == "Quit")
    {
        server.Close();
    }
    // Validation: Check if message is one word and less than 20 characters
    else if (incommingMessage.Length > 0 && incommingMessage.Length <= 20 && incommingMessage.Contains(" "))
    {
        ServerMessage += " " + incommingMessage.Trim();
        bytes = Encoding.ASCII.GetBytes(ServerMessage);
        server.Send(bytes, bytes.Length, remoteEP);
    }
    else
    {
        string message = "Invalid Message; Please send less then 20 characters and with no space";
        byte[] sendBytes = Encoding.ASCII.GetBytes(message);
        server.Send(sendBytes, sendBytes.Length, remoteEP);
    }
    server.Close();
}