#nullable disable
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
        Console.Write("Enter an IP Address to scan for open/avaliable ports to connect to: ");
        string ipAddress = Console.ReadLine();
        LoadingUI();
    }

    static void LoadingUI()
    {
        Console.Clear();
        Console.CursorVisible = false; // Makes it so you cant type, for cleaner UI
        Console.WriteLine("Please wait while this program checks for open ports...");
        Console.WriteLine("Progress (6/1024) Ports checked");

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
}