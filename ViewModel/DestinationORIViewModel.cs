using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace ViewModel
{
    public class DestinationORIViewModel : ViewModelBase
    {
        public event CommandHandler<IViewEventArguments> OnSaveCompleted;

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        private string _value1;

        public string Value1
        {
            get
            {
                return _value1;
            }

            set
            {
                _value1 = value;
                RaisePropertyChanged("Value1");
            }
        }

        private string _value2;

        public string Value2
        {
            get
            {
                return _value2;
            }

            set
            {
                _value2 = value;
                RaisePropertyChanged("Value2");
            }
        }

        private string _value3;

        public string Value3
        {
            get
            {
                return _value3;
            }

            set
            {
                _value3 = value;
                RaisePropertyChanged("Value3");
            }
        }

        private string _value4;

        public string Value4
        {
            get
            {
                return _value4;
            }

            set
            {
                _value4 = value;
                RaisePropertyChanged("Value4");
            }
        }

        private string _value5;

        public string Value5
        {
            get
            {
                return _value5;
            }

            set
            {
                _value5 = value;
                RaisePropertyChanged("Value5");
            }
        }

        private string _value6;

        public string Value6
        {
            get
            {
                return _value6;
            }

            set
            {
                _value6 = value;
                RaisePropertyChanged("Value1");
            }
        }

        public DestinationORIViewModel()
        {
            ViewModelMain.Instance.Header = "Destination ORI";
            CancelCommand = new RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new SettingsViewModel(); });
            SaveCommand = new RelayCommand(SaveClick);
           BindData();
        }

        private void SaveClick(object obj)
        {
            try
            {
                var repository = new DestinationORIRepository();
                List<string> valueList = new List<string>();
                if (!string.IsNullOrEmpty(Value1))
                    valueList.Add(Value1);
                if (!string.IsNullOrEmpty(Value2))
                    valueList.Add(Value2);
                if (!string.IsNullOrEmpty(Value3))
                    valueList.Add(Value3);
                if (!string.IsNullOrEmpty(Value4))
                    valueList.Add(Value4);
                if (!string.IsNullOrEmpty(Value5))
                    valueList.Add(Value5);
                if (!string.IsNullOrEmpty(Value6))
                    valueList.Add(Value6);

                var retVal = repository.AddConfigValues(valueList);
                if (retVal == 1)
                {
                    OnSaveCompleted(new EventArguements<IViewEventArguments>(null));
                }
                else
                {
                    OnSaveCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = "There is some issue processing your request. Please try again later!" }));
                }

                ViewModelMain.Instance.CurrentView = new SettingsViewModel();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindData()
        {
            try
            {
                var repository = new DestinationORIRepository();
                var sourceLab = repository.GetConfigValues();
                Value1 = sourceLab != null && sourceLab.Count > 0 ? sourceLab.ElementAt(0) : string.Empty;
                Value2 = sourceLab != null && sourceLab.Count > 1 ? sourceLab.ElementAt(1) : string.Empty;
                Value3 = sourceLab != null && sourceLab.Count > 2 ? sourceLab.ElementAt(2) : string.Empty;
                Value4 = sourceLab != null && sourceLab.Count > 3 ? sourceLab.ElementAt(3) : string.Empty;
                Value5 = sourceLab != null && sourceLab.Count > 4 ? sourceLab.ElementAt(4) : string.Empty;
                Value6 = sourceLab != null && sourceLab.Count > 5 ? sourceLab.ElementAt(5) : string.Empty;
            }
            catch(Exception ex)
            {
            }
        }
    }
}
