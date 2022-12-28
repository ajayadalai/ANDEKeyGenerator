using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //basic ViewModelBase
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arguments"></param>
        public delegate void CommandHandler<T>(EventArguements<T> arguments);

        private bool displayBusyIndicator;
        public bool DisplayBusyIndicator
        {
            get { return displayBusyIndicator; }
            set
            {
                displayBusyIndicator = value;
                IsReady = !value;
                RaisePropertyChanged("DisplayBusyIndicator");
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get { return isReady; }
            private set
            {
                isReady = value;
                RaisePropertyChanged("IsReady");
            }
        }

        public virtual string Header
        {
            set
            {
                ViewModelMain.Instance.Header = value;
            }
        }

        private double viewHeight = ApplicationValues.ViewHeight;
        public virtual double ViewHeight
        {
            get
            {
                return viewHeight;
            }
            set
            {
                viewHeight = value;
                RaisePropertyChanged("ViewHeight");
            }
        }
    }
}
