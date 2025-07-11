#nullable disable
using System.Text.RegularExpressions;

class Program
{
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
        int maxPortNumber = Convert.ToInt32(Console.ReadLine());
        ValidatePort(maxPortNumber);
        Console.Write("Enter an IP Address to scan for open/avaliable ports to connect to: ");
        string ipAddress = Console.ReadLine();
        ValidateIPAddress(ipAddress);
        LoadingUI();
    }

    static void LoadingUI()
    {
        Console.Clear();
        Console.CursorVisible = false; // Makes it so you cant type, for cleaner UI
        Console.WriteLine("Please wait while this program checks for open ports...");
        Console.WriteLine("Progress (1/xxxx) Ports checked");

        while (true)
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
}