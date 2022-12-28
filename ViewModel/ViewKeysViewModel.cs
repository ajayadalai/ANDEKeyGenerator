using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace ViewModel
{
    public class ViewKeysViewModel : ViewModelBase
    {
        public RelayCommand BackCommand { get; set; }
        public RelayCommand GenerateCommand { get; set; }


        private List<KeyDetailsModel> _keyDetailsList;
        public List<KeyDetailsModel> KeyDetailsList
        {
            get
            {
                return _keyDetailsList;
            }

            set
            {
                _keyDetailsList = value;
            }
        }


        public ViewKeysViewModel()
        {
            ViewModelMain.Instance.Header = "Generated Keys";
            BindData();                     
            BackCommand = new RelayCommand(BackClick);
            GenerateCommand = new RelayCommand(GenerateClick);
        }

        private void GenerateClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new KeyGenerationViewModel();
        }

        private void BackClick(object obj)
        {          
            ViewModelMain.Instance.CurrentView = new MainMenuViewModel(string.Empty);
        }

        private void ViewClick(object obj)
        {
            
        }

        private void pagingCtrl__PageChanged()
        {

        }

       
        public void BindData()
        {
            try
            {
                // var repository = new KeyGenerationRepository();
                //  KeyDetails = new ObservableCollection<KeyDetailsModel>(repository.GetAllKeys());

                var repository = new KeyGenerationRepository();
                this.KeyDetailsList = repository.GetAllKeys();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
