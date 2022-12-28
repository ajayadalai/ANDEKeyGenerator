using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CMFFilesForRunIDViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand CancelCommand { get; set; }

        #endregion
        public CMFFilesForRunIDViewModel()
        {
            ViewModelMain.Instance.Header = "CMF Files for ANDE RUN ID";
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnCancel(object obj)
        {
            ViewModelMain.Instance.CurrentView = new MainMenuViewModel(string.Empty);
        }
    }
}
