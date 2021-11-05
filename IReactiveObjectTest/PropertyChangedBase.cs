using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Threading;

namespace IReactiveObjectTest
{
    public class PropertyChangedBase : IReactiveObject
    {
        private readonly Lazy<Unit> _propertyChangingEventsSubscribed;
        private readonly Lazy<Unit> _propertyChangedEventsSubscribed;
        public PropertyChangedBase()
        {
            _propertyChangingEventsSubscribed = new Lazy<Unit>(
                                                        () =>
                                                        {
                                                            this.SubscribePropertyChangingEvents();
                                                            return Unit.Default;
                                                        },
                                                        LazyThreadSafetyMode.PublicationOnly);
            _propertyChangedEventsSubscribed = new Lazy<Unit>(
                                                        () =>
                                                        {
                                                            this.SubscribePropertyChangedEvents();
                                                            return Unit.Default;
                                                        },
                                                        LazyThreadSafetyMode.PublicationOnly);
        }

        #region INotifyPropertyChanged

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged
        {
            add
            {
                _ = _propertyChangedEventsSubscribed.Value;
                PropertyChangedHandler += value;
            }
            remove => PropertyChangedHandler -= value;
        }

        private event PropertyChangedEventHandler? PropertyChangedHandler;

        #endregion

        #region INotifyPropertyChanging

        private event PropertyChangingEventHandler? PropertyChangingHandler;

        /// <inheritdoc/>
        public event PropertyChangingEventHandler? PropertyChanging
        {
            add
            {
                _ = _propertyChangingEventsSubscribed.Value;
                PropertyChangingHandler += value;
            }
            remove => PropertyChangingHandler -= value;
        }

        #endregion

        #region IReactiveObject

        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChangedHandler?.Invoke(this, args);
        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanging(PropertyChangingEventArgs args) =>
            PropertyChangingHandler?.Invoke(this, args);

        #endregion
    }
}
