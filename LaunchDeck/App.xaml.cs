using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;



namespace LaunchDeck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Otwórz klucz rejestru
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            // Dodaj wartość do rejestru, aby aplikacja uruchamiała się przy starcie systemu
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rkApp.SetValue("LaunchDeck", System.IO.Path.GetDirectoryName(path)+"\\LaunchDeck.exe");
            
        }
    }

}
