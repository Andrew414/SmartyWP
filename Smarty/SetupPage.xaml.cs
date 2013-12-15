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
            helper = CommonHelper.GetInstanse();
            cbxCustomPort_Unchecked(null, null);

            DataContext = App.ViewModel;
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
            //if 
        }
    }
}