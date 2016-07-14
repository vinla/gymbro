using System;
using System.ComponentModel;

namespace GymBro.Core
{
    public class ViewModel : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnActivating()
        {

        }
    }    
}
