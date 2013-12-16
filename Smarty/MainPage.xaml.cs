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

    public partial class MainPage : PhoneApplicationPage
    {
        public IServer server;

        public CommonHelper helper;
        public bool setupcomplete = false;

        public bool demomode = false;

        public Button[] mapbuttons;
        public MapButtonInfo[] mapbuttoninfos;
        

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

            }

            //setupcomplete = true;
        }

        private void MapButtons_Tap(object Sender, RoutedEventArgs Args)
        {
            int sender = -1;

            for (int i = 0; i < mapbuttons.Length; i++)
            {
                if (Sender == mapbuttons[i])
                {
                    sender = i;
                    break;
                }
            }

            rctMapLine.Visibility = System.Windows.Visibility.Visible;
            rctMapLinei.Visibility = System.Windows.Visibility.Visible;
            //rctMapLineh.Visibility = System.Windows.Visibility.Visible;
            rctMapLineih.Visibility = System.Windows.Visibility.Visible;

            double i1 = (int)(mapbuttoninfos[sender].X + (Sender as Button).Width / 2) - 1;
            double i2 = (int)(mapbuttoninfos[sender].Y + (Sender as Button).Height / 2);
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

            lblMapDesk.Text = (mapbuttoninfos[sender].name);
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BasicSetup.xaml", UriKind.RelativeOrAbsolute));
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
    }
}