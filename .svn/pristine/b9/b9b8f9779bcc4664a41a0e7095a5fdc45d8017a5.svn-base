using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace TestApplication.View
{
    /// <summary>
    /// Interaction logic for KeyList.xaml
    /// </summary>
    public partial class KeyList : UserControl
    {
        ViewKeysViewModel vm = null;
        public KeyList()
        {
            InitializeComponent();
            vm = new ViewKeysViewModel();

            pagingCtrl_.PageSize = SetGridPageSize(ConfigurationManager.AppSettings["AdvanceSearchPageSize"]);
            pagingCtrl_.CurrentPageIndex = 0;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > 450)
            {
                var no_of_items_canbeshown = Convert.ToInt32((e.NewSize.Height - 475) / 22);
                var newPagesize = Convert.ToInt32(ConfigurationManager.AppSettings["KeyPageSize"]) + no_of_items_canbeshown;
                newPagesize = newPagesize > 15 ? 15 : newPagesize;
                if (pagingCtrl_.PageSize != newPagesize)
                {
                    pagingCtrl_.CurrentPageIndex = 0;
                    pagingCtrl_.PageSize = newPagesize;
                    BindData();
                }
            }
            else
            {
                var defaultPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["AdvanceSearchPageSize"]);
                if (pagingCtrl_.PageSize != defaultPageSize)
                {
                    pagingCtrl_.CurrentPageIndex = 0;
                    pagingCtrl_.PageSize = defaultPageSize;
                    BindData();
                }
            }
        }

        private void pagingCtrl__PageChanged()
        {

        }

        private void SetPagingControl(int totalCount)
        {
            pagingCtrl_.TotalItems = totalCount;
            pagingCtrl_.currentPage.Text = (pagingCtrl_.CurrentPage).ToString();
            pagingCtrl_.totalPage.Text = pagingCtrl_.TotalPages.ToString();
            pagingCtrl_.ConfigureButtons();
        }



        private void _runIdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (null != pagingCtrl_)
            {
                pagingCtrl_.PageChanged += () =>
                {
                    BindData();
                };
            }
          
        }


        private void BindData()
        {                     
            int skipCount = pagingCtrl_.PageSize * pagingCtrl_.CurrentPageIndex;
            int takeCount = pagingCtrl_.PageSize;

            if (vm.KeyDetailsList != null)
            {
                var KeyList = vm.KeyDetailsList;                
                if (!string.IsNullOrEmpty(SearchText))
                {
                    // _KeyList.ItemsSource = vm.KeyDetailsList.Where(i => string.Format("{0}{1}", i.ApplicationName.ToLower(), i.OrganaisationName.ToLower()).Contains(SearchText.ToLower())).Skip(skipCount).Take(takeCount);
                    KeyList = KeyList.Where(i => string.Format("{0}{1}", i.ApplicationName.ToLower(), i.OrganaisationName.ToLower()).Contains(SearchText.ToLower())).ToList();
                    skipCount = 0;
                }
                else
                {
                    // _KeyList.ItemsSource = vm.KeyDetailsList.Skip(skipCount).Take(takeCount);
                    KeyList = KeyList.ToList();
                }

                SetPagingControl(KeyList.Count);
                _KeyList.ItemsSource = KeyList.Skip(skipCount).Take(takeCount);

            }
            else
            {
                SetPagingControl(0);
            }
        }


        private int SetGridPageSize(string configPageSize)
        {
            var retVal = 10; //ConfigurationConstant.DefaultPageSize;
            if (int.TryParse(configPageSize, out retVal))
            {
                return retVal;
            }           
            return retVal;
        }

        private void searchKeywords__OnSearch(string obj)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
            }
        }

        private void searchKeywords__OnClear()
        {
            try
            {
                if (null != vm.KeyDetailsList)
                {
                    BindData();
                    pagingCtrl_.CurrentPageIndex = 0;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private string SearchText
        {
            get
            {
                return null == searchKeywords_ || searchKeywords_.SearchInstructions != searchKeywords_.SearchText ? searchKeywords_.SearchText : string.Empty;
            }
        }
    }
}
