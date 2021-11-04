using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IReactiveObjectTest
{
    public class PropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging, IReactiveObject
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null) => this.RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));

        #endregion

        #region INotifyPropertyChanging

        public event PropertyChangingEventHandler? PropertyChanging;
        public void RaisePropertyChanging([CallerMemberName] string? propertyName = null) => this.RaisePropertyChanging(new PropertyChangingEventArgs(propertyName));

        #endregion

        #region IReactiveObject

        public void RaisePropertyChanged(PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);
        public void RaisePropertyChanging(PropertyChangingEventArgs args) => PropertyChanging?.Invoke(this, args);

        #endregion
    }
}
