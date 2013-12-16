using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace Smarty
{
    public class Coords
    {
        public int Floor;
        public int X;
        public int Y;
    }


    public class MainViewModel : INotifyPropertyChanged
    {
        CommonHelper helper;

        public MainViewModel()
        {
            this.MainItems = new ObservableCollection<ItemViewModel>();
            this.SavedServers = new ObservableCollection<SavedServesItem>();
            this.Devices = new ObservableCollection<DeviceItem>();

            DevCoords = new Dictionary<string, Coords>();

            helper = CommonHelper.GetInstanse();
            helper.model = this;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> MainItems { get; private set; }
        public ObservableCollection<SavedServesItem> SavedServers { get; private set; }
        public ObservableCollection<DeviceItem> Devices { get; private set; }
        public string housemap;
        public Dictionary<string, Coords> DevCoords;

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.SavedServers.Add(new SavedServesItem() { LineMain = "Home", LineMore = "192.168.0.103" });
            this.SavedServers.Add(new SavedServesItem() { LineMain = "Stan house", LineMore = "192.168.1.27" });

            this.helper.mainpage.server.LoadData();

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}