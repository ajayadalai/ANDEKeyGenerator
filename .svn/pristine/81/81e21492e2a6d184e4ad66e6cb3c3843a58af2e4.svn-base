using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        #region Local resource declarations
        /// <summary>
        /// 
        /// </summary>
        private static ViewModelMain _instance;
        #endregion

        public static ViewModelMain Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewModelMain();
                }
                return ViewModelMain._instance;
            }
        }


        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        private ViewModelMain()
        {
            // CurrentView = new KeyGenerationViewModel();
            CurrentView = new MainMenuViewModel();
        }



        private string _header;
        /// <summary>
        /// 
        /// </summary>
        public new string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                RaisePropertyChanged("Header");
            }
        }

    }
}
