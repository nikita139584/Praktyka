using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

public interface IMessageHandler
{
    void HandleMessage(string message);
}

public interface IChatServer
{
    Task StartAsync();
}

public class UdpChatServer : IChatServer
{
    private readonly int port;
    private readonly IMessageHandler messageHandler;
    private UdpClient? server;
    private ConcurrentDictionary<IPEndPoint, bool> clients = new();
    private StringBuilder messageHistory = new();
    private int clientCounter = 0;

    public UdpChatServer(int port, IMessageHandler messageHandler)
    {
        this.port = port;
        this.messageHandler = messageHandler;
    }

    public async Task StartAsync()
    {
        InitializeServer();
        _ = Task.Run(ReceiveMessagesAsync);
        //_ = Task.Run(SendServerMessagesAsync);
        await Task.Delay(-1);
    }

    private void InitializeServer()
    {
        server = new UdpClient(port);
        messageHandler.HandleMessage($"сервер запущено на порту {port}.");
    }

    private async Task ReceiveMessagesAsync()
    {

        
        while (true)
        {
            var result = await server.ReceiveAsync();
            var message = Encoding.UTF8.GetString(result.Buffer);

            if (!clients.ContainsKey(result.RemoteEndPoint))
            {
                clients[result.RemoteEndPoint] = true;
                clientCounter++;
                await SendHistoryAsync(result.RemoteEndPoint);
                messageHandler.HandleMessage($"\nклієнт підключено: {result.RemoteEndPoint} (Клієнт #{clientCounter})");
            }

            if (message == "off")
            {
                clients.TryRemove(result.RemoteEndPoint, out _);
                messageHandler.HandleMessage($"\nклієнт відключено: {result.RemoteEndPoint}");
                continue;
            }

            var formattedMessage = $"{result.RemoteEndPoint}: {message}";
            messageHandler.HandleMessage(formattedMessage);
            messageHistory.AppendLine(formattedMessage);
            await BroadcastMessageAsync(formattedMessage, result.RemoteEndPoint);
        }
    }

    private async Task SendServerMessagesAsync()
    {
        while (true)
        {
            messageHandler.HandleMessage("надішліть повідомлення клієнтам: ");
            var serverMessage = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(serverMessage))
            {
                var formattedMessage = $"\n[Сервер]: {serverMessage}";
                // messageHandler.HandleMessage(formattedMessage);
                messageHistory.AppendLine(formattedMessage);
                await BroadcastMessageAsync(formattedMessage);
            }
        }
    }

    private async Task SendHistoryAsync(IPEndPoint client)
    {
        var history = Encoding.UTF8.GetBytes(messageHistory.ToString());
        await server.SendAsync(history, history.Length, client);
    }

    private async Task BroadcastMessageAsync(string message, IPEndPoint excludedClient = null)
    {
        var data = Encoding.UTF8.GetBytes(message);
        foreach (var client in clients.Keys)
        {
            if (!client.Equals(excludedClient))
                await server.SendAsync(data, data.Length, client);
        }
    }
}

public class ConsoleMessageHandler : IMessageHandler
{
    public void HandleMessage(string message)
    {
        Console.WriteLine(message);
    }
}

class ServerProgram
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "СЕРВЕРНА СТОРОНА";
        var handler = new ConsoleMessageHandler();
        var server = new UdpChatServer(9000, handler);
        await server.StartAsync();
    }
}