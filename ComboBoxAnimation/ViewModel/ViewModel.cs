using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ComboBoxAnimation.ViewModel
{
    public class User
    {
        public string Name { get; set; }
    }
    public class ModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>(); // = Account;
        User selectUser;
        string _AddUser;

        public Type SelectedPage { get; set; } = typeof(BlankPage1);

        public string AddUser
        {
            get { return _AddUser; }
            set
            {
                _AddUser = value;
                Users.Add(new User() { Name = value });
                NotifyPropertyChanged();
            }
        }
        public User SelectUser
        {
            get { return selectUser; }
            set { selectUser = value; NotifyPropertyChanged(); }
        }



        public ModelView()
        {
            
            Users.Add(new User() { Name = "Element 1" });
            Users.Add(new User() { Name = "Element 2" });
            Users.Add(new User() { Name = "Element 3" });
            Users.Add(new User() { Name = "Element 4" });
            SelectUser = Users[0];

        }

    }
}
