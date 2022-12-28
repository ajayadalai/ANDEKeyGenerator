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
    public class KeyGenerationViewModel : ViewModelBase
    {
        public event CommandHandler<IViewEventArguments> OnIssueCompleted;

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand GenerateCommand { get; set; }
        public RelayCommand ViewCommand { get; set; }
        public RelayCommand IssueCommand { get; set; }

        public Dictionary<int, List<char>> ProbableAlphabets { get; set; }
        public Dictionary<int, List<int>> ProbableDigits { get; set; }
      

        private string _organaisationName;
        public string OrganaisationName
        {
            get
            {
                return _organaisationName;
            }

            set
            {
                _organaisationName = value;
                RaisePropertyChanged("OrganaisationName");
            }
        }

        private string _applicationName;

        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }

            set
            {
                _applicationName = value;
                RaisePropertyChanged("ApplicationName");
            }
        }

        private string _key;

        public string Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
                RaisePropertyChanged("Key");
            }
        }

        private string _keyIssueDateTime;

        public string KeyIssueDateTime
        {
            get
            {
                return _keyIssueDateTime;
            }

            set
            {
                _keyIssueDateTime = value;
                RaisePropertyChanged("KeyIssueDateTime");
            }
        }

        private ApplicationModel _selectedApplication;

        public ApplicationModel SelectedApplication
        {
            get
            {
                return _selectedApplication;
            }

            set
            {
                _selectedApplication = value;
                RaisePropertyChanged("SelectedApplication");
            }
        }


        private ObservableCollection<ApplicationModel> _applications;
        public ObservableCollection<ApplicationModel> Applications
        {
            get { return _applications; }
            set { _applications = value; }
        }


        public KeyGenerationViewModel()
        {
            ViewModelMain.Instance.Header = "Key Generation";
         //   CancelCommand = new RelayCommand((obj) => { ViewModelMain.Instance.CurrentView = new KeyGenerationViewModel(); });
            IssueCommand = new RelayCommand(IssueClick);
            GenerateCommand = new RelayCommand(GenerateClick);
            CancelCommand = new RelayCommand(BackClick);
            ViewCommand = new RelayCommand(ViewClick);
            BindApplication();

        }

        private void BackClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new MainMenuViewModel(string.Empty);
        }

        private void ViewClick(object obj)
        {
            ViewModelMain.Instance.CurrentView = new ViewKeysViewModel();
        }

        private void BindApplication()
        {
            Applications = new ObservableCollection<ApplicationModel>()
                {
                  new ApplicationModel(){id=1,Application="FAIRS"}
                     , new ApplicationModel() {id=2,Application="CMF Editor" }
                     ,new ApplicationModel(){id=3,Application="ANDE UAW" }
                };

            this.SelectedApplication = Applications[0];
        }

        private void GenerateClick(object obj)
        {
            string EnteredOrgName = this.OrganaisationName;


            if (!string.IsNullOrEmpty(EnteredOrgName))
            {
                string orgName = new String(this.OrganaisationName.Where(Char.IsLetter).ToArray()).ToString().ToUpper();

                if (string.IsNullOrEmpty(orgName))
                {
                    OnIssueCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = "Please Enter Valid Organisation Name." }));

                }
                else
                {
                    this.Key = new Licensor(orgName, SelectedApplication).CreateLicenseKey();
                }

                
                //#region test code
                //var test = new Licensor(OrganaisationName, SelectedApplication).ValidateLicenseKey("2P4C-M5I2-438I-3PL6-M222");
                //#endregion
            }
            else
            {
                OnIssueCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = "Please Enter Organisation Name." }));
            }
        }

        

        private void IssueClick(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Key))
                {
                    var repository = new KeyGenerationRepository();
                    KeyDetailsModel keyDetails = new KeyDetailsModel();

                    keyDetails.ApplicationName = this.SelectedApplication.Application;
                    keyDetails.OrganaisationName = this.OrganaisationName;
                    keyDetails.Key = this.Key;
                    keyDetails.KeyIssueDateTime = DateTime.Now.ToUniversalTime().Ticks.ToString(); //DateTime.UtcNow.ToString();

                        var retVal = repository.AddKeyDetailsValues(keyDetails);
                        if (retVal == 1)
                        {
                            OnIssueCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = string.Format("Key {0} issued Successfully.", this.Key) }));
                        }
                        else
                        {
                            OnIssueCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = "There is some issue processing your request. Please try again later!" }));
                        }                                    
                }
                else
                {
                    OnIssueCompleted(new EventArguements<IViewEventArguments>(new ArguementMessage { Content = "Please Generate Key." }));
                }
                

                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
