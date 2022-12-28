using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Commands
        public RelayCommand DestinationORIConfigCommand { get; set; }
        public RelayCommand SourceLabConfigCommand { get; set; }
        public RelayCommand SubmitByUserIdConfigCommand { get; set; }
        public RelayCommand SpecimenCategoryConfigCommand { get; set; }
        public RelayCommand ReadingByUserConfigCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        #endregion

        public SettingsViewModel()
        {
            ViewModelMain.Instance.Header = "Settings";
            DestinationORIConfigCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new DestinationORIViewModel(); });
            //SourceLabConfigCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new SourceLabViewModel(); });
            //SubmitByUserIdConfigCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new SubmitByUserIdViewModel(); });
            //SpecimenCategoryConfigCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new SpecimenCategoryViewModel(); });
            //ReadingByUserConfigCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new ReadingByUserViewModel(); });
            CancelCommand = new ViewModel.RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new MainMenuViewModel(string.Empty); });
        }
    }
}
