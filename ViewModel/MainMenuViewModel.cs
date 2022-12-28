using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        #region Commands
        public RelayCommand GenerateKeyCommand { get; set; }
        public RelayCommand ViewKeyCommand { get; set; }
        public RelayCommand SettingCommand { get; set; }
        #endregion


        public MainMenuViewModel()
        {

            GenerateKeyCommand = new RelayCommand(GenerateKeyClick);
            ViewKeyCommand = new RelayCommand(ViewKeyClick);
            SettingCommand = new RelayCommand(SettingClick);
            

        }

        private void ViewKeyClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new ViewKeysViewModel();
        }

        public MainMenuViewModel(string header)
        {

            GenerateKeyCommand = new RelayCommand(GenerateKeyClick);
            ViewKeyCommand = new RelayCommand(ViewKeyClick);
            SettingCommand = new RelayCommand(SettingClick);
            Header = header;
        }

        private void GenerateKeyClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new KeyGenerationViewModel();
        }

        private void SettingClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new SettingsViewModel();
        }
    }
}
