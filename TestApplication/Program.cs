using Eto.Forms;

namespace TestApplication;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        new Application().Run(new FormMain());
    }
}