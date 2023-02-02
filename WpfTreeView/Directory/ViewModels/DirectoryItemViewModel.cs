﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeView;

/// <summary>
/// A view model for each directory item
/// </summary>
public class DirectoryItemViewModel : BaseViewModel
{
    #region Public Properties

    /// <summary>
    /// The type of this item
    /// </summary>
    public DirectoryItemType Type { get; set; }

    /// <summary>
    /// The full path to the item
    /// </summary>
    public string FullPath { get; set; }

    /// <summary>
    /// The name of this directory item
    /// </summary>
    public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);

    /// <summary>
    ///  A list of all children contained inside this item
    /// </summary>
    public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

    /// <summary>
    /// Indicates if this item can be expanded
    /// </summary>
    public bool CanExpand => Type != DirectoryItemType.File;

    /// <summary>
    /// Indicate ifthe current item is expanded or not
    /// </summary>
    public bool IsExpanded
    {
        get
        {
            return Children?.Count(f => f != null) > 0;
        }
        set
        {
            // If the UI tells us to expand...
            if (value == true)
                // Fid all children
                Expand();
            // If  the UI tells us to close
            else
                ClearChildren();
        }
    }

    #endregion

    #region Public Commands

    /// <summary>
    /// The command to expand this item
    /// </summary>
    public ICommand ExpandCommand { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="fullPath">The full path of this item</param>
    /// <param name="type">The type of item</param>
    public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
    {
        // Create commands
        ExpandCommand = new RelayCommand(Expand);

        // Set path and type
        FullPath = fullPath;
        Type = type;

        // Setup the children as needed
        ClearChildren();
    }

    #endregion

    #region Helper Method

    /// <summary>
    /// Removes all children from the list, adding a dummy item to show the expand icon if required
    /// </summary>
    private void ClearChildren()
    {
        // Clear items
        Children = new ObservableCollection<DirectoryItemViewModel>();

        // Show the expand arrow if we are not a file
        if (Type != DirectoryItemType.File)
            Children.Add(null);
    }

    #endregion

    /// <summary>
    /// Expands this directory and finds all children
    /// </summary>
    private void Expand()
    {
        // We cannot expand a file
        if (Type == DirectoryItemType.File)
            return;

        // Find all children
        var children = DirectoryStructure.GetDirectoryContents(FullPath);
        Children = new ObservableCollection<DirectoryItemViewModel>(
            children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
    }
}