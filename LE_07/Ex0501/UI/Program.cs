using System.Threading.Tasks;


namespace Ex0501.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new Menu();
            await menu.ShowMenuAsync();
        }
    }
}
