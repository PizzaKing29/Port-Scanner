#nullable disable
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{
    static ushort MinPortNumber = 0;
    static ushort MaxPortNumber = 0;
    static int PortsChecked = 0;
    static string IpAddress = "";
    static bool Scanning;
    static List<int> OpenPorts = new List<int>();

    static void Main()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.WriteLine("Port Scanner by PizzaKing29");
        Console.WriteLine("-------------------------------------------\n\n");
        do
        {
            Console.WriteLine("Enter a max port range to check through...");
            Console.WriteLine("Ports 1–1024 are well-known ports (HTTP, FTP, SSH, etc.)");
            Console.WriteLine("Ports 1025–49151 are registered ports (apps/services register them)");
            Console.WriteLine("Ports 49152–65535 are dynamic/private ports (often assigned temporarily)\n");
            Console.Write("Min Port #: ");
            MinPortNumber = Convert.ToUInt16(Console.ReadLine());
            Console.Write("Max Port #: ");
            MaxPortNumber = Convert.ToUInt16(Console.ReadLine());
        }
        while (!ValidatePort(MinPortNumber, MaxPortNumber));
        do
        {
            Console.Write("Enter an IP Address to scan for open/avaliable ports to connect to: ");
            IpAddress = Console.ReadLine();
        }
        while (!ValidateIPAddress(IpAddress));
        Task.Run(ConnectToPort);
        Scanning = true;
        LoadingUI();
    }

    static void LoadingUI()
    {
        Console.CursorVisible = false; // Makes it so you cant type, for cleaner UI
        while (Scanning) // Loading animation
        {
            Console.Clear();
            Console.WriteLine("Please wait while this program checks for open ports...");
            Console.WriteLine($"Ports Checked ({MinPortNumber + PortsChecked}/{MaxPortNumber})");
            Thread.Sleep(100);
            Console.Write("\\\r");
            Thread.Sleep(120);
            Console.Write("|\r");
            Thread.Sleep(100);
            Console.Write("/\r");
            Thread.Sleep(120);
            Console.Write("-\r");
        }
    }

    static bool ValidateIPAddress(string ipAddress)
    {
        if (!IPAddress.TryParse(ipAddress, out IPAddress address)) // checks if the IP address is NOT valid
        {
            Console.Clear();
            Console.Write("\nInvalid IP Address, press ENTER to try again... ");
            Console.Read();
            Console.Clear();
            return false;
        }
        else
        {
            return true;
        }
    }

    static bool ValidatePort(int minPort, int maxPort)
    {
        if (minPort > 0 && maxPort > 65535)
        {
            Console.Clear();
            Console.Write($"\nMinPort and MaxPort is too large or is an invalid entry, press 'ENTER' to try again...");
            Console.Read();
            Console.Clear();
            return false;
        }
        else
        {
            return true;
        }
    }

    static async Task ConnectToPort()
    {
        // MaxDegreeOfParallelism = 250 sets the max amount of ports that can be scanned at once
        await Parallel.ForAsync(MinPortNumber, MaxPortNumber, new ParallelOptions { MaxDegreeOfParallelism = 250 }, async (i, CancellationToken) =>
        {
            try
            {
                using var tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(IpAddress, i);
                if (tcpClient.Connected)
                    OpenPorts.Add(i);
                PortsChecked++;
            }
            catch
            {
                PortsChecked++;
            }
        });

        Console.Clear();
        Console.WriteLine("List of open ports:");
        foreach (var port in OpenPorts)
        {
            Console.WriteLine($"Port {port} is avaliable");
        }
        Environment.Exit(exitCode: 0);
    }
}