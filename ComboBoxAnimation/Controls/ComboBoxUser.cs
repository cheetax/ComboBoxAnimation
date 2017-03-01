using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace ComboBoxAnimation.Controls
{
    public sealed class ComboBoxUser : Control , INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public ComboBoxUser()
        {
            this.DefaultStyleKey = typeof(ComboBoxUser);
        }

        ModelViewComboBox model;
        Popup _Popup;
        StackPanel _PopupPanel;
        public ListBox _ListBox;
        ContentPresenter _ContentPresenter;
        Border _Border;
        Grid _GridRoot;
        Border _ToolsPanel;
        FontIcon _AddButton;
        FontIcon _SettingsButton;

        public event EventHandler<RoutedEventArgs> PressedAddButton = delegate { };
        public event EventHandler<RoutedEventArgs> PressedSettingsButton = delegate { };


        protected override void OnApplyTemplate()
        {            
            _Popup = GetTemplateChild("Popup") as Popup;
            _PopupPanel = GetTemplateChild("PopupPanel") as StackPanel;
            _ListBox = GetTemplateChild("listBox") as ListBox;
            _ContentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;
            _Border = GetTemplateChild("Background") as Border;
            _GridRoot = GetTemplateChild("GridRoot") as Grid;
            _ContentPresenter.ContentTemplate = ItemTemplate;
            _ContentPresenter.Content = this.SelectedItem;
            _ToolsPanel = GetTemplateChild("ToolsPanel") as Border;

            _AddButton = GetTemplateChild("AddButton") as FontIcon;
            _AddButton.PointerPressed += (s, e) => PressedAddButton?.Invoke(s, e);

            _SettingsButton = GetTemplateChild("SettingsButton") as FontIcon;            
            _SettingsButton.PointerPressed += (s, e) => PressedSettingsButton?.Invoke(s, e);

            model = _GridRoot.DataContext as ModelViewComboBox;

            _ToolsPanel.SizeChanged += (s, e) =>
            {
                model.ToolsPanelHeigth = ((FrameworkElement)s).ActualHeight;
                model.ToolsPanelWidth = ((FrameworkElement)s).ActualWidth;
                ((FrameworkElement)s).Width = model.ToolsPanelWidth;
                model.GridRootWidth += ((FrameworkElement)_GridRoot).ActualWidth + model.ToolsPanelWidth;
                ((FrameworkElement)s).Visibility = Visibility.Collapsed;
            };
            _Popup.IsOpen = true;
            _PopupPanel.SizeChanged += (s, e) =>
            {                
                model.Lenght = ((FrameworkElement)s).ActualHeight;                         
                _ListBox.MaxHeight = ((FrameworkElement)(_ListBox.ContainerFromIndex(1))).ActualHeight * ((MaxCountItems == -1) ? double.PositiveInfinity : MaxCountItems);
                _Popup.IsOpen = model.IsChecked;
            };
            
            _ListBox.SelectionChanged += (s, e) =>
            {
                _ContentPresenter.Content = ((ListBox)s).SelectedItem;
                model.IsChecked = false;
                UpdateStates(true);
            };
            _Border.PointerEntered += (s, e) =>  // Есть фокус
            {
                VisualStateManager.GoToState(this, "PointerOver", true);
                VisualStateManager.GoToState(this, "OpenedToolsPanel", true);
            };

            _GridRoot.PointerExited += (s, e) =>  // Нет фокуса
            {
                VisualStateManager.GoToState(this, "Normal", true);
                if (!model.IsChecked ) VisualStateManager.GoToState(this, "ClosedToolsPanel", true);
            };
            _Border.PointerPressed += (s, e) =>
            {
                model.IsChecked = !model.IsChecked;
                _PopupPanel.Width = _Border.ActualWidth;
                UpdateStates(true); 
            };
            
            _AddButton.PointerEntered += (s, e) =>  // Есть фокус
            {
                VisualStateManager.GoToState(this, "PointerOverAddButton", true);
                
            };

            _AddButton.PointerExited += (s, e) =>  // Нет фокуса
            {
                VisualStateManager.GoToState(this, "Normal", true);
                
            };
            _SettingsButton.PointerEntered += (s, e) =>  // Есть фокус
            {
                VisualStateManager.GoToState(this, "PointerOverSettingsButton", true);
            };

            _SettingsButton.PointerExited += (s, e) =>  // Нет фокуса
            {
                VisualStateManager.GoToState(this, "Normal", true);
            };

        }

        void UpdateStates(bool useTransitions)
        {
            if (model.IsChecked)
            {
                VisualStateManager.GoToState(this, "Opened", useTransitions);
                VisualStateManager.GoToState(this, "OpenedToolsPanel", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Closed", useTransitions);
                VisualStateManager.GoToState(this, "ClosedToolsPanel", useTransitions);
            }
        }
        public string AddItem
        {
            get { return (string)GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); NotifyPropertyChanged(); }
        }
        public object ItemSource
        {
            get { return (object)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); NotifyPropertyChanged(); }
        }
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); NotifyPropertyChanged(); }
        }     

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); NotifyPropertyChanged(); }
        }

        public int MaxCountItems
        {
            get { return (int)GetValue(MaxCountItemsProperty); }
            set { SetValue(MaxCountItemsProperty, value); NotifyPropertyChanged(); }
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(nameof(ItemSource), typeof(object), typeof(ComboBoxUser), null);

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(ComboBoxUser), null);

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ComboBoxUser), null);

        public static readonly DependencyProperty AddItemProperty =
            DependencyProperty.Register(nameof(AddItem), typeof(string), typeof(ComboBoxUser), null);
        
        public static readonly DependencyProperty MaxCountItemsProperty =
            DependencyProperty.Register(nameof(MaxCountItems), typeof(int), typeof(ComboBoxUser), new PropertyMetadata(-1));
    }

    public class ModelViewComboBox : INotifyPropertyChanged

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

        public double ToolsPanelHeigth { get; set; }
        public double ToolsPanelWidth { get; set; }
        public double GridRootWidth { get; set; }

        int selItem;

        public int SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                IsChecked = false;
                NotifyPropertyChanged();
            }
        }
        bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged();
            }
        }

    }
    public class NegativeValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value.GetType().Name)
            {
                case "Double":
                    return -(double)value;
                case "Int32":                
                    return -(int)value;
                default:
                    return 0;
            }
                 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class DoubleToGridLength : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value.GetType().Name)
            {
                case "Double":
                    return new GridLength((double)value);                
                default:
                    return GridLength.Auto;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
