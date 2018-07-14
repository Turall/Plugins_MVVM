using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Tools
{
    [Serializable]
    class Observable : INotifyPropertyChanged
    {
        [NonSerialized]
        private PropertyChangedEventHandler propertyChanged;

        public virtual event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value;  }
            remove { propertyChanged -= value;  }
        }

        protected void OnPropertyChanged ([CallerMemberName] string prop = "")
        {
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
