using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TickTacToe.ViewModels
{
	public class Observable : BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if(Equals(storage, value))
            {
                return;
            }

            storage = value;
            this.RaisePropertyChanged(propertyName);
        }
    }
}

