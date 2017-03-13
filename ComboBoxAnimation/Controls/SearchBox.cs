using System;
using Windows.System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace ComboBoxAnimation.Controls
{
    public sealed class SearchBox : Control, INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public SearchBox()
        {
            this.DefaultStyleKey = typeof(SearchBox);
        }

        ModelViewSearchBox model;
        Grid _GridRoot;
        Border _Background;
        TextBox _TextBoxSearch;
        Button _ButtonSearch;
        public event EventHandler<RoutedEventArgs> PressedSearchButton = delegate { };

        protected override void OnApplyTemplate()
        {            
            _GridRoot = GetTemplateChild<Grid>("GridRoot");
            _Background = GetTemplateChild<Border>("Background");
            _TextBoxSearch = GetTemplateChild<TextBox>("TextBoxSearch");
            _ButtonSearch = GetTemplateChild<Button>("ButtonSearch");

            _ButtonSearch.Tapped += (s, e) =>
            {
                _TextBoxSearch.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                PressedSearchButton?.Invoke(s, e);
            };
            _TextBoxSearch.KeyDown += (s, e) =>
            {
                if (e.Key == VirtualKey.Enter)
                {
                    _TextBoxSearch.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    PressedSearchButton?.Invoke(s, e);
                }
            };

            model = _GridRoot.DataContext as ModelViewSearchBox;

            _GridRoot.Loaded += (s, e) =>
            {
                model.Lenght = _Background.ActualWidth;
                _TextBoxSearch.Visibility = Visibility.Collapsed;
            };            
            _GridRoot.PointerEntered += (s, e) =>
             {
                 VisualStateManager.GoToState(this, "OpenedSearchBox", true);
                 if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
                     _TextBoxSearch.Focus(FocusState.Pointer);
             };
            _GridRoot.PointerExited += (s, e) =>
            {
                if (_TextBoxSearch.Text == string.Empty && _TextBoxSearch.FocusState == FocusState.Unfocused)
                {
                    VisualStateManager.GoToState(this, "ClosedSearchBox", true);
                }
            };
            _GridRoot.LostFocus += (s, e) =>
            {
                if (_TextBoxSearch.Text == string.Empty && _TextBoxSearch.FocusState == FocusState.Unfocused)
                {
                    VisualStateManager.GoToState(this, "ClosedSearchBox", true);
                    _TextBoxSearch.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    PressedSearchButton?.Invoke(s, e);
                }
            };
        }

       

        T GetTemplateChild<T>(string name) where T: DependencyObject
        {
            T child = GetTemplateChild(name) as T;
            if (child == null)
                throw new NullReferenceException(name);            
            return child;
        }

        public Thickness BorderThicknessOpened
        {
            get { return (Thickness)GetValue(BorderThicknessOpenedProperty); }
            set { SetValue(BorderThicknessOpenedProperty, value); NotifyPropertyChanged(); }
        }

        public static readonly DependencyProperty BorderThicknessOpenedProperty =
            DependencyProperty.Register(nameof(BorderThicknessOpened), typeof(Thickness), typeof(SearchBox), new PropertyMetadata(new Thickness(0)));

        public string SearchValue
        {
            get { return (string)GetValue(SearchValueProperty); }
            set { SetValue(SearchValueProperty, value); NotifyPropertyChanged(); }
        }

        public static readonly DependencyProperty SearchValueProperty =
            DependencyProperty.Register(nameof(SearchValue), typeof(string), typeof(SearchBox), null);

    }
    public class ModelViewSearchBox : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        double lenght;
        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }        

    }

    public class ObjectToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value!=null)
            {
                switch (value.GetType().Name)
                {
                    case "Thinckness":
                        return (string)value;
                    case "Int32":
                        return (string)value;
                    default:
                        return "";
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
