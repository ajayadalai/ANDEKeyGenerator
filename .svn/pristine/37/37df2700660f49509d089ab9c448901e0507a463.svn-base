using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using Domain;
using TestApplication.View.Common;

namespace TestApplication.View
{
    /// <summary>
    /// Interaction logic for DestinationORI.xaml
    /// </summary>
    public partial class DestinationORI : UserControl
    {
        DestinationORIViewModel vm = null;
        public DestinationORI()
        {
            InitializeComponent();
            vm = ViewModelMain.Instance.CurrentView as DestinationORIViewModel;
            vm.OnSaveCompleted += Vm_OnSaveCompleted;
        }

        private void Vm_OnSaveCompleted(Domain.EventArguements<Domain.IViewEventArguments> arguments)
        {
            if (!arguments.IsMessage)
            {
                Message.Display("Saved Successfully!!", string.Format("{0} saved successfully!!", ViewModelMain.Instance.Header));
            }
            else
            {
                if ((arguments.EventData as ArguementMessage).Type == MessageType.Message)
                    Message.Display("Error", arguments.EventData.Content);
            }
        }
    }
}
