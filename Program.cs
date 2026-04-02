using System.Net;
using System.Net.Sockets;
using System.Text;
using System;


public interface IMessageHandler
{
    void HandleMessage(string message);
}

public interface IChatClient
{
    Task ConnectAsync();
}

public class UdpChatClient : IChatClient
{
    private readonly int serverPort;
    private readonly string? serverIp;
    private readonly IMessageHandler? messageHandler;
    private UdpClient? client;
    private IPEndPoint? serverEndPoint;

    public UdpChatClient(string? serverIp, int serverPort, IMessageHandler? messageHandler)
    {
        this.serverIp = serverIp;
        this.serverPort = serverPort;
        this.messageHandler = messageHandler;
    }

    public async Task ConnectAsync()
    {
        serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
        client = new UdpClient(0);
        client.Connect(serverEndPoint);
        await SendInitialMessageAsync();

        AppDomain.CurrentDomain.ProcessExit += async (s, e) => await SendDisconnectMessageAsync();

        _ = Task.Run(ReceiveMessagesAsync);
        _ = Task.Run(SendMessagesAsync);
        await Task.Delay(-1);
    }

    private async Task SendInitialMessageAsync()
    {
        var initialMessage = "Клієнт підключився";
        var data = Encoding.UTF8.GetBytes(initialMessage);
        await client.SendAsync(data, data.Length);
    }

    private async Task SendDisconnectMessageAsync()
    {
        var message = "off";
        var data = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(data, data.Length);
    }

    private async Task ReceiveMessagesAsync()
    {
        while (true)
        {
            var result = await client.ReceiveAsync();
            var message = Encoding.UTF8.GetString(result.Buffer);
            messageHandler?.HandleMessage(message);
        }
    }

    private async Task SendMessagesAsync()
    {
        Console.WriteLine("Введіть ім'я");
        string name = Console.ReadLine();
        Console.WriteLine("Введіть номер коліра 1-15");
        int color = int.Parse(Console.ReadLine());
        DateTime now = DateTime.Now;
        while (true)
        {

            if(color == 1)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
            else if (color == 2)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
          else if (color == 3)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if(color == 4)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
           else if (color == 5)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 6)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 7)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 8)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 9)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 10)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 11)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 12)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.White;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
            else if (color == 13)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }

            else if (color == 14)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
            else if (color == 15)
            {

                Console.Write($"{now} {name}: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                var message = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(message);
                Console.ForegroundColor = ConsoleColor.White;
                await client.SendAsync(data, data.Length);

                if (message == "off")
                    break;
            }
            else
            {
                Console.WriteLine("Ви ввели не вірне число введіть номер коліра 1-15");
                return;
            }
        }
                    client.Close();
                    Console.WriteLine("Відключено від сервера.");
                }
           

        }
    


public class ConsoleMessageHandler : IMessageHandler
{
    public void HandleMessage(string message)
    {
        Console.WriteLine(message);
    }
}

class ClientProgram
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "CLIENT SIDE";
        var handler = new ConsoleMessageHandler();
        var client = new UdpChatClient("127.0.0.1", 9000, handler);
        await client.ConnectAsync();
    }
}