using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTreeView;

/// <summary>
/// The view model for the applications main Directory view
/// </summary>
public class DirectoryStructureViewModel : BaseViewModel
{
    #region Public Properites
    /// <summary>
    /// A list of all directories on the machine
    /// </summary>
    public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

    #endregion

    #region Constructor

    public DirectoryStructureViewModel()
    {
        // Get the logical drives
        var children = DirectoryStructure.GetLogicalDrives();

        // Create the view models from the data
        Items = new ObservableCollection<DirectoryItemViewModel>(
            children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
    }

    #endregion
}
