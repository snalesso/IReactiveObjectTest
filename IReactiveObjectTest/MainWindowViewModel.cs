using ReactiveUI;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace IReactiveObjectTest
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            this._hasMessageOAPH = this.WhenAnyValue(x => x.Message)
                .Select(x => !string.IsNullOrWhiteSpace(x))
                .Do(x => Debug.WriteLine($"Has Message changed: {x}"))
                .ToProperty(this, nameof(this.HasMessage));
        }

        private string? _message = null;
        public string? Message
        {
            get { return this._message; }
            set
            {
                if (this._message == value)
                    return;

                this.RaisePropertyChanging();
                this._message = value;
                this.RaisePropertyChanged();
            }
        }

        private readonly ObservableAsPropertyHelper<bool> _hasMessageOAPH;
        public bool HasMessage => this._hasMessageOAPH.Value;
    }
}
