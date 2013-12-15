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
    public partial class SetupPage : PhoneApplicationPage
    {
        public CommonHelper helper;

        public SetupPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            
            helper = CommonHelper.GetInstanse();
            helper.setuppage = this;

            PrepareUI();

            
        }

        private void PrepareUI()
        {
            lblDemoDescription.Text = "Демо-режим позволяет вам понять возможности " + 
                "приложения даже не имея настроенного smarty-сервера.";
            cbxCustomPort_Unchecked(null, null);
        }

        private void cbxCustomPort_Unchecked(object sender, RoutedEventArgs e)
        {
            tbxPort.IsEnabled = false;
            lblPort.Foreground = new System.Windows.Media.SolidColorBrush(Colors.Gray);
        }

        private void cbxCustomPort_Checked(object sender, RoutedEventArgs e)
        {
            tbxPort.IsEnabled = true;
            lblPort.Foreground = new System.Windows.Media.SolidColorBrush(
                (lblIP.Foreground as SolidColorBrush).Color);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            helper.mainpage.server = new HouseServer();
            Dictionary<string, string> parameters = new Dictionary<string,string>();
            parameters.Add("ip", tbxIP.Text);
            parameters.Add("port", tbxPort.Text);
            helper.mainpage.server.SetupServer(parameters);

            FinishSetup();
        }

        private void lbxSaved_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private void btnDemo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            helper.mainpage.server = new DummyServer();
            helper.mainpage.server.SetupServer(null);

            FinishSetup();
        }

        private void FinishSetup()
        {
            if (helper.mainpage.server.IsServerValid())
            {
                helper.mainpage.setupcomplete = true;
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Сервер не отвечает или не настроен как smarty-сервер.\n" +
                    "Нажмите \"OK\", чтобы настроить другой сервер, и попытайтесь еще раз.",
                    "smarty", MessageBoxButton.OK);
            }
        }
    }
}