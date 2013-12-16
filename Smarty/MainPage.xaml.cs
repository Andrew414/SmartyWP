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
    public class MapButtonInfo
    {
        public int X;
        public int Y;
        public string name;
    }

    public class Constants
    {
        public static string THERM = "thermometer";
        public static string LIGHT = "light";

        public static Color OFF_COLOR = Colors.Red;
        public static Color ON_COLOR = Colors.Green;
        public static Color ONE_STATE = Colors.Blue;
    }

    public partial class MainPage : PhoneApplicationPage
    {
        public IServer server;

        public CommonHelper helper;
        public bool setupcomplete = false;

        public bool demomode = false;

        public Button[] mapbuttons;
        public MapButtonInfo[] mapbuttoninfos;

        public int activeItem = -1;
        public int mapItem = -1;
        

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
            /*
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }*/

            if (!setupcomplete)
            {
                StartLoadOrSetupServerInfoTask();
            }
            else
            {
                App.ViewModel.LoadData();
                if (!demomode)
                {
                    pbxMap.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(helper.model.housemap));
                    
                    //btnOne.Click += new RoutedEventHandler(Handler4x4);
                }

                mapbuttons = new Button[helper.model.DevCoords.Count];
                mapbuttoninfos = new MapButtonInfo[helper.model.DevCoords.Count];

                int index = 0;
                foreach (var i in helper.model.DevCoords)
                {
                    Button btnOne = new Button();
                    btnOne.FontSize = 20;
                    btnOne.Background = new SolidColorBrush(Colors.Black);
                    btnOne.Content = (index+1).ToString();
                    btnOne.Height = btnOne.Width = 65;

                    mapbuttons[index] = btnOne;
                    string name = "";
                    for (int j = 0; j < helper.model.Devices.Count; j++)
                    {
                        if (i.Key == helper.model.Devices[j].ID)
                        {
                            name = helper.model.Devices[j].Name;
                            break;
                        }
                    }
                    mapbuttoninfos[index] = new MapButtonInfo() { X = i.Value.X * 2 / 5, Y = (i.Value.Y * 2 / 5) + (int)pbxMap.Margin.Top, name=name };

                    btnOne.SetValue(Canvas.LeftProperty, (Double)mapbuttoninfos[index].X);
                    btnOne.SetValue(Canvas.TopProperty, (Double)mapbuttoninfos[index].Y);

                    btnOne.Tap += MapButtons_Tap;

                    cnvMap.Children.Add(btnOne);

                    index++;
                }

                rctMapLine.Visibility = System.Windows.Visibility.Collapsed;
                rctMapLinei.Visibility = System.Windows.Visibility.Collapsed;
                rctMapLineh.Visibility = System.Windows.Visibility.Collapsed;
                rctMapLineih.Visibility = System.Windows.Visibility.Collapsed;

                lblMapDesk.Visibility = System.Windows.Visibility.Collapsed;

            }

            RefreshMap();
        }

        private void RouletteChange(int dev)
        {
            if (helper.model.Devices[dev].Type == Constants.THERM)
            {
                return;
            }

            if (helper.model.Devices[dev].Type == Constants.LIGHT)
            {
                helper.mainpage.server.SetState(dev.ToString(), helper.model.Devices[dev].State == "1" ? "0" : "1");
                helper.model.LoadData();
            }
        }

        private void ConfigureButtons(int sender)
        {
            rctMapLine.Visibility = System.Windows.Visibility.Visible;
            rctMapLinei.Visibility = System.Windows.Visibility.Visible;
            //rctMapLineh.Visibility = System.Windows.Visibility.Visible;
            rctMapLineih.Visibility = System.Windows.Visibility.Visible;

            double i1 = (int)(mapbuttoninfos[sender].X + mapbuttons[sender].Width / 2) - 1;
            double i2 = (int)(mapbuttoninfos[sender].Y + mapbuttons[sender].Height / 2);
            double i3 = 364 - mapbuttoninfos[sender].Y;
            rctMapLine.SetValue(Canvas.LeftProperty, i1);
            rctMapLine.SetValue(Canvas.TopProperty, i2);
            rctMapLine.Height = i3;
            rctMapLinei.SetValue(Canvas.LeftProperty, i1 + 1);
            rctMapLinei.SetValue(Canvas.TopProperty, i2);
            rctMapLinei.Height = i3;

            int i4 = (int)i1 - 8;
            rctMapLineh.Width = i4;
            rctMapLineih.Width = i4;

            lblMapDesk.Text = (mapbuttoninfos[sender].name) + ", ";
            if (helper.model.Devices[sender].Type == Constants.LIGHT)
            {
                lblMapDesk.Text += helper.model.Devices[sender].LineMore.ToLower();
                //(helper.model.Devices[sender].State == "1") ? "включена" : "выключена";

                mapbuttons[sender].Background = new SolidColorBrush(
                    helper.model.Devices[sender].State == "1" ? Constants.ON_COLOR : Constants.OFF_COLOR
                    );
            }
            if (helper.model.Devices[sender].Type == Constants.THERM)
            {
                lblMapDesk.Text += helper.model.Devices[sender].LineMore;
                //helper.model.Devices[sender].State + " " ;
                mapbuttons[sender].Background = new SolidColorBrush(Constants.ONE_STATE);
            }
            lblMapDesk.Visibility = System.Windows.Visibility.Visible;
        }


        private void MapButtons_Tap(object Sender, RoutedEventArgs Args)
        {
            int sender = -1;

            foreach (var i in mapbuttons)
            {
                i.Background = new SolidColorBrush(Colors.Black);
            }

            bool rouletteChange = false;

            for (int i = 0; i < mapbuttons.Length; i++)
            {
                if (Sender == mapbuttons[i])
                {
                    if (i == mapItem)
                    {
                        rouletteChange = true;
                    }

                    sender = i;
                    mapItem = i;
                    break;
                }
            }

            if (rouletteChange)
            {
                RouletteChange(sender);
            }

            ConfigureButtons(sender);
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //this.activeItem = e
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            //try
            //{
              //  MessageBox.Show(e.GetPosition(sender as UIElement).X.ToString() + ", " + e.GetPosition(sender as UIElement).Y.ToString());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
             
        }

        public void RefreshMap()
        {
            if (mapItem != -1)
            {
                MapButtons_Tap(mapbuttons[mapItem], null);
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            helper.model.LoadData();

           // MessageBox.Show(activeItem.ToString());

            RefreshMap();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
            {
                return;
            }

            this.activeItem = int.Parse((e.AddedItems[0] as DeviceItem).ID);
            NavigationService.Navigate(new Uri("/BasicInfo.xaml", UriKind.RelativeOrAbsolute));
        }

        private void lblMapDesk_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}