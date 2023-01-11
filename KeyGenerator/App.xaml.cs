using System.Configuration;
using System.Windows;
using Domain;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigConstants.DBPath = ConfigurationSettings.AppSettings["SQLiteDBPath"];

        }
    }
}
