using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Smarty
{
    public partial class MainPage : PhoneApplicationPage
    {
        public IServer server;

        public CommonHelper helper;
        public bool setupcomplete = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            helper = CommonHelper.GetInstanse();
            helper.mainpage = this;
        }

        public void ExitApplication()
        {
            throw new Exception("exit");
        }

        public bool StartLoadOrSetupServerInfoTask()
        {
            try
            {
                NavigationService.Navigate(new Uri("/SetupPage.xaml", UriKind.RelativeOrAbsolute));
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            //return MessageBox.Show("Simple question", "Is server known?", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }

        public Boolean MainPageActivated { get; set; }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            if (!setupcomplete)
            {
                StartLoadOrSetupServerInfoTask();
            }

            //setupcomplete = true;
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BasicSetup.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}