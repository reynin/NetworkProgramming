using System.Net;
using System.Net.Sockets;

// configuration
Console.Write("Please choose a user name: ");
var userName = Console.ReadLine();
ChooseProgram: Console.WriteLine("Do you want to [H]ost or [C]onnect? ");
var input = char.ToLowerInvariant(Console.ReadLine()!.FirstOrDefault()); // Error handling
switch(input) {
    case 'c': goto Client;
    case 'h': goto Server;
    default: goto ChooseProgram;
}

Client:
{
    // connect to server
    Console.Write("Please enter the host name, e.g. 192.168.1.24: ");
    var hostName = Console.ReadLine()!;
    using var client = new TcpClient(hostName, 44444);
    using var writer = new StreamWriter(client.GetStream());
    using var reader = new StreamReader(client.GetStream());
    writer.AutoFlush = true;
    
    // hand-shake
    writer.WriteLine("Is this Chat?");
    var response = reader.ReadLine();
    if (response != "Yes, this is Chat!") throw new Exception("Unexpected response: " + response);
    writer.WriteLine(userName);
    var otherUserName = reader.ReadLine()!;
    Console.WriteLine("Connected to " + otherUserName + ".\nSay something!");
    
    // chat loop
    while (true)
    {
        Console.Write("> ");
        writer.WriteLine(Console.ReadLine());
        Console.WriteLine(otherUserName + ": " + reader.ReadLine());
    }
}


Server:
{
    // listen for incoming connection
    var listener = new TcpListener(IPAddress.Any, 44444);
    listener.Start();
    Console.WriteLine("Waiting for incoming connection on " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0]);
    using var client = listener.AcceptTcpClient();
    using var writer = new StreamWriter(client.GetStream());
    using var reader = new StreamReader(client.GetStream());
    writer.AutoFlush = true;
    
    // hand-shake
    var request = reader.ReadLine();
    if (request != "Is this Chat?") throw new Exception("Unexpected request: " + request);
    writer.WriteLine("Yes, this is Chat!");
    var otherUserName = reader.ReadLine()!;
    Console.WriteLine(otherUserName + " connected!\nWaiting for their first message...");
    writer.WriteLine(userName);
    
    // chat loop
    while (true)
    {
        Console.WriteLine(otherUserName + ": " + reader.ReadLine());
        Console.Write("> ");
        writer.WriteLine(Console.ReadLine());
    }
}