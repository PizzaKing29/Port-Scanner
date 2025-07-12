#nullable disable
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static int MaxPortNumber = 0;
    static string IpAddress = "";
    static bool Scanning;
    static List<Task> ConnectionTasks = new List<Task>();

    static void Main()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.WriteLine("Port Scanner by PizzaKing29");
        Console.WriteLine("-------------------------------------------\n\n");
        Console.WriteLine("Enter a max port range to check through...");
        Console.WriteLine("Ports 1–1024 are well-known ports (HTTP, FTP, SSH, etc.)");
        Console.WriteLine("Ports 1025–49151 are registered ports (apps/services register them)");
        Console.WriteLine("Ports 49152–65535 are dynamic/private ports (often assigned temporarily)\n");
        Console.Write("Max Port #: ");
        MaxPortNumber = Convert.ToInt32(Console.ReadLine());
        ValidatePort(MaxPortNumber);
        Console.Write("Enter an IP Address to scan for open/avaliable ports to connect to: ");
        IpAddress = Console.ReadLine();
        ValidateIPAddress(IpAddress);
        Task.Run(ConnectToPort);
        Scanning = true;
        LoadingUI();
    }

    static void LoadingUI()
    {

        Console.CursorVisible = false; // Makes it so you cant type, for cleaner UI
        Console.Clear();
        Console.WriteLine("Please wait while this program checks for open ports...");
        

        while (Scanning) // Loading animation
        {
            Thread.Sleep(100);
            Console.Write("\\\r");
            Thread.Sleep(120);
            Console.Write("|\r");
            Thread.Sleep(100);
            Console.Write("/\r");
            Thread.Sleep(120);
            Console.Write("-\r");
        }
        Console.WriteLine("Finished ez");
    }

    static void ValidateIPAddress(string ipAddress)
    {
        if (!Regex.IsMatch(ipAddress, @"^(\d{1,3}\.){3}\d{1,3}$")) // checks if the IP address is NOT valid
        {
            Console.Clear();
            Console.Write("\nInvalid IP Address, press ENTER to try again... ");
            Console.Read();
            Main();
        }
    }

    static void ValidatePort(int port)
    {
        if (port > 65535)
        {
            Console.Clear();
            Console.Write($"\nPort '{port}' is too large for entry or is an invalid port entry, press ENTER to try again... ");
            Console.Read();
            Main();
        }
    }

    static async Task ConnectToPort()
    {
        for (int i = 1; i < MaxPortNumber; i++)
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                ConnectionTasks.Add(tcpClient.ConnectAsync(IpAddress, i));
                tcpClient.Close();
                tcpClient.Dispose();
            }
            catch
            {
                // Console.Write("Port Failed");
            }
        }
        await Task.WhenAll(ConnectionTasks);
    }
}