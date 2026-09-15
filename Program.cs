using System;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Simple Port/Network Info Tool ===");

        Console.Write("Enter hostname or IP: ");
        string? host = Console.ReadLine();

        Console.Write("Enter port number: ");
        string? portInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(host) || !int.TryParse(portInput, out int port))
        {
            Console.WriteLine("Invalid host or port. Try again.");
            return;
        }

        await CheckPort(host, port);
    }

    static async Task CheckPort(string host, int port, int timeoutMs = 3000)
    {
        using var client = new TcpClient();

        try
        {
            var connectTask = client.ConnectAsync(host, port);
            var timeoutTask = Task.Delay(timeoutMs);

            var completedTask = await Task.WhenAny(connectTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                Console.WriteLine($"[TIMEOUT] {host}:{port} did not respond within {timeoutMs}ms.");
            }
            else if (client.Connected)
            {
                Console.WriteLine($"[OPEN] {host}:{port} is reachable.");
            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"[CLOSED] {host}:{port} refused connection or host not found. ({ex.SocketErrorCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }
}