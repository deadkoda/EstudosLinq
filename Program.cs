using PocLinq;
using PocLinq.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        var service = new MusicaService();
        var menu = new Menu(service);
        await menu.ShowAsync();
    }
}