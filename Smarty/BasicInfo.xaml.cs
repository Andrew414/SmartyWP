using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace Smarty
{
    public partial class BasicSetup : PhoneApplicationPage
    {
        public CommonHelper helper;
        public string type;
        public string state;

        public BasicSetup()
        {
            InitializeComponent();
            //cbxCustomPort_Unchecked(null, null);

            helper = CommonHelper.GetInstanse();
            helper.info = this;

            this.Loaded += BasicSetup_Loaded; ;
        }

        void BasicSetup_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            DeviceItem item = null;
            for (int i = 0; i < helper.model.Devices.Count; i++)
            {
                if (helper.model.Devices[i].ID == helper.mainpage.activeItem.ToString())
                {
                    item = helper.model.Devices[i];
                    break;
                }
            }

            this.lblDeviceName.Text = item.Name;
            this.type = item.Type;
            this.lblDeviceDesk.Text = item.LineMore;

            int x = this.helper.model.DevCoords[helper.mainpage.activeItem.ToString()].X;
            int y = this.helper.model.DevCoords[helper.mainpage.activeItem.ToString()].Y;
            //int x = 500;
            //int y = 500;

            int dx = -180, dy = 10;

            this.rct.SetValue(Canvas.LeftProperty, x * 400.0 / 1000 + dx);
            this.rct.SetValue(Canvas.TopProperty, y * 400.0 / 1000 + dy);

            this.rctLil.SetValue(Canvas.LeftProperty, x * 400.0 / 1000 + dx + 2);
            this.rctLil.SetValue(Canvas.TopProperty, y * 400.0 / 1000 + dy + 2);

            pbxMap.Source = helper.mainpage.pbxMap.Source;

            if (type == Constants.LIGHT)
            {
                btnChange.Visibility = System.Windows.Visibility.Visible;

                if (item.State == "1")
                {
                    this.rctLil.Fill = new SolidColorBrush(Constants.ON_COLOR);
                    btnChange.Content = "выключить";
                }
                else
                {
                    this.rctLil.Fill = new SolidColorBrush(Constants.OFF_COLOR);
                    btnChange.Content = "включить";
                }

                this.state = item.State;
            }

            if (type == Constants.THERM)
            {
                btnChange.Visibility = System.Windows.Visibility.Collapsed;

                this.rctLil.Fill = new SolidColorBrush(Constants.ONE_STATE);                

                this.state = item.State;
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (this.state == "1")
            {
                helper.mainpage.server.SetState(helper.mainpage.activeItem.ToString(), "0");
            }

            if (this.state == "0")
            {
                helper.mainpage.server.SetState(helper.mainpage.activeItem.ToString(), "1");
            }

            helper.model.LoadData();
            this.LoadData();
        }



        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            helper.model.LoadData();
            this.LoadData();
        }

        private void rct_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            btnChange_Click(sender, null);
        }
    }
}