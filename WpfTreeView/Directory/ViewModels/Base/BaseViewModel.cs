using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTreeView;

/// <summary>
/// A base view model that fires Property Changed events as needed
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// The event that is fired when any child property changes its value
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}