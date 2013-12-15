using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Smarty
{
    public class SavedServersItem : INotifyPropertyChanged
    {
        private string _ip;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                if (value != _ip)
                {
                    _ip = value;
                    NotifyPropertyChanged("IP");
                }
            }
        }

        private string _port;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (value != _port)
                {
                    _port = value;
                    NotifyPropertyChanged("Port");
                }
            }
        }

        private string _lineMain;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineMain
        {
            get
            {
                return _lineMain;
            }
            set
            {
                if (value != _lineMain)
                {
                    _lineMain = value;
                    NotifyPropertyChanged("LineMain");
                }
            }
        }

        private string _lineMore;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineMore
        {
            get
            {
                return _lineMore;
            }
            set
            {
                if (value != _lineMore)
                {
                    _lineMore = value;
                    NotifyPropertyChanged("LineMore");
                }
            }
        }

        private string _desc;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                if (value != _desc)
                {
                    _desc = value;
                    NotifyPropertyChanged("Desription");
                }
            }
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